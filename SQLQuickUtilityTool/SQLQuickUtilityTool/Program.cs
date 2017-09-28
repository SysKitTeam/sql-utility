using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SQLQuickUtilityTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string connectionString;
            if (args.Length < 1)
            {
                while(true)
                {
                    ConnectionStringForm connStringForm = new ConnectionStringForm();
                    DialogResult res = connStringForm.ShowDialog();
                    if (res == DialogResult.Cancel)
                    {
                        return;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(connStringForm.ConnectionString))
                        {
                            connectionString = connStringForm.ConnectionString;
                            break;
                        }
                    }
                }
            } else
            {
                connectionString = args[0];
            }
            Application.Run(new MainForm(connectionString));
        }
    }
}
