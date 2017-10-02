using SQLQuickUtilityTool.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SQLQuickUtilityTool
{
    public class QueryExecutor
    {
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
                    SqlCommand sqlCmd = new SqlCommand(singleQuery, sqlConn);
                    if (singleQuery.Trim().ToLower().StartsWith("select "))
                    {
                        try
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                            DataSet dataSet = new DataSet();
                            adapter.Fill(dataSet);
                            result = dataSet;
                        }
                        catch (Exception exc)
                        {
                            result = exc;
                        }
                    } else
                    {
                        sqlCmd.Connection.Open();
                        try
                        {
                            result = sqlCmd.ExecuteNonQuery();
                        } catch (Exception exc)
                        {
                            result = exc;
                        }
                    }
                }
                // Thread.Sleep(5000);
                return result;
            }
            
        }
    }
}
