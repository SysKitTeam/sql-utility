﻿using SQLQuickUtilityTool.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SQLQuickUtilityTool
{
    public partial class MainForm : Form
    {
        public enum State
        {
            Ready,
            Running,
            Done,
            Canceled,
            Error
        }
        private State _state;
        private bool _isTimerRunning;
        private Exception _exc;
        private DateTime _startTime;
        public MainForm(string connectionString)
        {
            InitializeComponent();
            tbConnectionStrnig.Text = connectionString;
            _state = State.Ready;
            _isTimerRunning = false;
            _exc = null;

            updateState();
            queryExecutorBackgroundWorker.WorkerSupportsCancellation = true;
            timerQuery.Start();
        }

        #region State Management
        private void updateState()
        {
            switch(_state)
            {
                case State.Ready:
                    setStateReady();
                    break;
                case State.Done:
                    setStateDone();
                    break;
                case State.Running:
                    setStateRunning();
                    break;
                case State.Canceled:
                    setStateCanceled();
                    break;
                case State.Error:
                    setStateError();
                    break;
            }
        }

        private void setStateReady()
        {
            // buttons:
            btnExecute.Enabled = true;
            btnCancel.Enabled = false;

            // text boxes:
            tbConnectionStrnig.Enabled = true;
            tbQuery.Enabled = true;

            // status text:
            tslStatus.Text = "Status: Ready";

            // tool strip color:
            statusStrip.BackColor = Color.White;

            // rows affected text:
            tslRowsAffected.Text = string.Empty;

            // time text:
            tslTime.Text = "Time: 00:00:00.000";
        }

        private void setStateRunning()
        {
            // buttons:
            btnExecute.Enabled = false;
            btnCancel.Enabled = true;

            // text boxes:
            tbConnectionStrnig.Enabled = false;
            tbQuery.Enabled = false;

            // status text:
            tslStatus.Text = "Status: Running";

            // tool strip color:
            statusStrip.BackColor = Color.Yellow;

            // rows affected text:
            tslRowsAffected.Text = string.Empty;
        }

        private void setStateDone()
        {
            // buttons:
            btnExecute.Enabled = true;
            btnCancel.Enabled = false;

            // text boxes:
            tbConnectionStrnig.Enabled = true;
            tbQuery.Enabled = true;

            // status text:
            tslStatus.Text = "Status: Done";

            // tool strip color:
            statusStrip.BackColor = Color.Green;
        }

        private void setStateCanceled()
        {
            // buttons:
            btnExecute.Enabled = true;
            btnCancel.Enabled = false;

            // text boxes:
            tbConnectionStrnig.Enabled = true;
            tbQuery.Enabled = true;

            // status text:
            tslStatus.Text = "Status: Canceled";

            // tool strip color:
            statusStrip.BackColor = Color.Orange;
        }

        private void setStateError()
        {
            // buttons:
            btnExecute.Enabled = true;
            btnCancel.Enabled = false;

            // text boxes:
            tbConnectionStrnig.Enabled = true;
            tbQuery.Enabled = true;

            // status text:
            tslStatus.Text = "Status: Error";

            // tool strip color:
            statusStrip.BackColor = Color.Red;
        }

        #endregion

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbConnectionStrnig.Text))
            {
                MessageBox.Show("Connection string must be defined!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(tbQuery.Text))
            {
                MessageBox.Show("Query text must be given!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QueryDTO args = new QueryDTO
            {
                ConnectionString = tbConnectionStrnig.Text,
                QueryText = tbQuery.Text
            };

            _startTime = DateTime.Now;
            _isTimerRunning = true;
            _state = State.Running;

            tslTime.Text = "Time: 00:00:00";
            
            updateState();

            queryExecutorBackgroundWorker.RunWorkerAsync(args);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            queryExecutorBackgroundWorker.CancelAsync();
            _isTimerRunning = false;
            _state = State.Canceled;
            updateState();
            QueryExecutor.CancelExecution();
        }

        private void queryExecutorBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _exc = null;
            QueryDTO args = e.Argument as QueryDTO;
            try
            {
                object result = QueryExecutor.ExecuteQuery(args);
                if ((sender as BackgroundWorker).CancellationPending)
                {
                    e.Cancel = true;
                } else
                {
                    e.Result = result;
                }
            } catch (Exception exc)
            {
                _exc = exc;
            }
        }

        private void queryExecutorBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) return;
            _isTimerRunning = false;
            TimeSpan totalTime = DateTime.Now.Subtract(_startTime);
            tslTime.Text = string.Format("Time: {0:00}:{1:00}:{2:00}.{3:00}", totalTime.Hours, totalTime.Minutes, totalTime.Seconds, totalTime.Milliseconds);

            try
            {

                dgvResults.DataSource = null;
                dgvResults.Columns.Clear();
                if (_exc != null)
                {
                    _state = State.Error;
                    updateState();
                    MessageBox.Show(_exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _state = State.Done;
                updateState();
                // SELECT cases:
                if (e.Result.GetType() == typeof(DataSet))
                {
                    dgvResults.ReadOnly = true;
                    DataTable table = new DataTable();

                    dgvResults.DataSource = turnBinariesToText((e.Result as DataSet)).Tables[0];

                    tslRowsAffected.Text = "Rows affected: " + (e.Result as DataSet).Tables[0].Rows.Count;
                }
                // INSERT, UPDATE, DELETE cases:
                if (e.Result.GetType() == typeof(int))
                {
                    dgvResults.Columns.Add("result", "result");
                    dgvResults.Rows.Add("Query executed successfully. Rows affected: " + e.Result);
                    tslRowsAffected.Text = "Rows affected: " + e.Result;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Display Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timerQuery_Tick(object sender, EventArgs e)
        {
            if (_isTimerRunning)
            {
                if (_startTime != null)
                {
                    TimeSpan current = DateTime.Now.Subtract(_startTime);
                    tslTime.Text = string.Format("Time: {0:00}:{1:00}:{2:00}", current.Hours, current.Minutes, current.Seconds);
                }
            }
        }

        /// <summary>
        /// This method is used to filter out data types that cannot be displayed (eg. binary types).
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DataSet turnBinariesToText(DataSet data)
        {
            bool changeNeeded = false;
            foreach (DataColumn col in data.Tables[0].Columns)
            {
                if (col.DataType == typeof(Byte[]))
                {
                    changeNeeded = true;
                    break;
                }
            }

            if (!changeNeeded) return data;
            else
            {
                DataTable newTable = new DataTable();
                DataTable oldTable = data.Tables[0];
                List<string> byteColumns = new List<string>();

                foreach(DataColumn col in oldTable.Columns)
                {
                    if (col.DataType == typeof(Byte[]))
                    {
                        newTable.Columns.Add(col.ColumnName, typeof(string));
                        byteColumns.Add(col.ColumnName);
                    }
                    else
                    {
                        newTable.Columns.Add(col.ColumnName, col.DataType);
                    }
                }
                foreach(DataRow row in oldTable.Rows)
                {
                    DataRow newRow = newTable.NewRow();
                    foreach (DataColumn col in oldTable.Columns)
                    {
                        if (byteColumns.Contains(col.ColumnName))
                        {
                            newRow[col.ColumnName] = "<binary_data>";
                        } else
                        {
                            newRow[col.ColumnName] = row[col.ColumnName];
                        }
                    }
                    newTable.Rows.Add(newRow);
                }
                DataSet newSet = new DataSet();
                newSet.Tables.Add(newTable);
                return newSet;
            }
            
        }
    }
}
