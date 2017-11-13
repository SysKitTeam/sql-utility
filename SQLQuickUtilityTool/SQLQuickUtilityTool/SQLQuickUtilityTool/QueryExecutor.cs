using SQLQuickUtilityTool.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;

namespace SQLQuickUtilityTool
{
    public class QueryExecutor
    {
        private static SqlCommand _sqlCmd;
        /// <summary>
        /// Executes the given query using given connection string.
        /// </summary>
        /// <param name="args">Query parameters.</param>
        /// <returns>Selected rows if a SELECT statement was given in form of a <seealso cref="DataSet"/> object, 
        /// number of rows affected if other statements are given, or exception if any was thrown.</returns>
        public static object ExecuteQuery(QueryDTO args)
        {
            using (var sqlConn = new SqlConnection(args.ConnectionString))
            {
                object result = null;
                string[] queries = args.QueryText.Split(';');
                foreach(string singleQuery in queries)
                {
                    _sqlCmd = new SqlCommand(singleQuery, sqlConn);
                    if (singleQuery.Trim().ToLower().StartsWith("select "))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(_sqlCmd);
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        result = dataSet;
                    } else
                    {
                        _sqlCmd.Connection.Open();
                        
                        try
                        {
                            result = _sqlCmd.ExecuteNonQuery();
                        } catch (Exception exc)
                        {
                            _sqlCmd.Connection.Close();
                            throw exc;
                        }
                        _sqlCmd.Connection.Close();
                    }
                }
                _sqlCmd = null;
                return result;
            }
        }

        public static void CancelExecution()
        {
            if (_sqlCmd != null)
            {
                _sqlCmd.Cancel();
            }
        }
    }
}
