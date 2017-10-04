namespace SQLQuickUtilityTool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.tbConnectionStrnig = new System.Windows.Forms.TextBox();
            this.lblQuery = new System.Windows.Forms.Label();
            this.tbQuery = new System.Windows.Forms.TextBox();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.lblResult = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslRowsAffected = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.queryExecutorBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.timerQuery = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(15, 287);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(227, 41);
            this.btnExecute.TabIndex = 0;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(259, 287);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(227, 41);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Location = new System.Drawing.Point(9, 19);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(124, 17);
            this.lblConnectionString.TabIndex = 3;
            this.lblConnectionString.Text = "Connection String:";
            // 
            // tbConnectionStrnig
            // 
            this.tbConnectionStrnig.Location = new System.Drawing.Point(12, 39);
            this.tbConnectionStrnig.Name = "tbConnectionStrnig";
            this.tbConnectionStrnig.Size = new System.Drawing.Size(474, 22);
            this.tbConnectionStrnig.TabIndex = 4;
            // 
            // lblQuery
            // 
            this.lblQuery.AutoSize = true;
            this.lblQuery.Location = new System.Drawing.Point(12, 76);
            this.lblQuery.Name = "lblQuery";
            this.lblQuery.Size = new System.Drawing.Size(51, 17);
            this.lblQuery.TabIndex = 5;
            this.lblQuery.Text = "Query:";
            // 
            // tbQuery
            // 
            this.tbQuery.Location = new System.Drawing.Point(15, 96);
            this.tbQuery.Multiline = true;
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Size = new System.Drawing.Size(471, 170);
            this.tbQuery.TabIndex = 0;
            // 
            // dgvResults
            // 
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(12, 360);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.RowTemplate.Height = 24;
            this.dgvResults.Size = new System.Drawing.Size(471, 255);
            this.dgvResults.TabIndex = 7;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(12, 340);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(52, 17);
            this.lblResult.TabIndex = 8;
            this.lblResult.Text = "Result:";
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus,
            this.tslRowsAffected,
            this.tslTime});
            this.statusStrip.Location = new System.Drawing.Point(0, 632);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(501, 25);
            this.statusStrip.TabIndex = 9;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tslStatus
            // 
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(90, 20);
            this.tslStatus.Text = "Status: Build";
            // 
            // tslRowsAffected
            // 
            this.tslRowsAffected.Name = "tslRowsAffected";
            this.tslRowsAffected.Size = new System.Drawing.Size(126, 20);
            this.tslRowsAffected.Text = "Rows affected: 10";
            // 
            // tslTime
            // 
            this.tslTime.Name = "tslTime";
            this.tslTime.Size = new System.Drawing.Size(137, 20);
            this.tslTime.Text = "Time: HH:mm:ss.ms";
            // 
            // queryExecutorBackgroundWorker
            // 
            this.queryExecutorBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.queryExecutorBackgroundWorker_DoWork);
            this.queryExecutorBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.queryExecutorBackgroundWorker_RunWorkerCompleted);
            // 
            // timerQuery
            // 
            this.timerQuery.Enabled = true;
            this.timerQuery.Interval = 1000;
            this.timerQuery.Tick += new System.EventHandler(this.timerQuery_Tick);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnExecute;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(501, 657);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.tbQuery);
            this.Controls.Add(this.lblQuery);
            this.Controls.Add(this.tbConnectionStrnig);
            this.Controls.Add(this.lblConnectionString);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExecute);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "SQL Quick Utility Tool";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.TextBox tbConnectionStrnig;
        private System.Windows.Forms.Label lblQuery;
        private System.Windows.Forms.TextBox tbQuery;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.ComponentModel.BackgroundWorker queryExecutorBackgroundWorker;
        private System.Windows.Forms.ToolStripStatusLabel tslStatus;
        private System.Windows.Forms.ToolStripStatusLabel tslRowsAffected;
        private System.Windows.Forms.ToolStripStatusLabel tslTime;
        private System.Windows.Forms.Timer timerQuery;
    }
}

