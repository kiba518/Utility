 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Utility
{
    public class DbHelperSQL : IDisposable
    {
        public DbHelperSQL()
        {

        }
        public DbHelperSQL(string connectionString)
        {
            connectionSQL = new SqlConnection(connectionString);
            connectionSQL.Close();
        }
        SqlConnection connectionSQL;
        public DataSet Query(string SQLString)
        {
            DataSet ds = new DataSet();
            try
            {
                if (connectionSQL.State != ConnectionState.Open)
                {
                    connectionSQL.Open();
                }
                if (connectionSQL.State == ConnectionState.Open)
                {
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connectionSQL);
                    //command.SelectCommand.CommandTimeout = Times;
                    command.SelectCommand.CommandTimeout = 60000;
                    command.Fill(ds, "ds");
                }
                else
                {
                    return null;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return null;
            }
            return ds;
        }
        public static DataSet Query(string connectionString, string SQLString, string strWhere)
        {
            if (!string.IsNullOrEmpty(strWhere))
            {
                SQLString = SQLString + strWhere;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                        //command.SelectCommand.CommandTimeout = Times;
                        command.SelectCommand.CommandTimeout = 60000;
                        command.Fill(ds, "ds");
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    connection.Close();
                }
                return ds;
            }
        }
        public static DataSet Query(string connectionString, string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                        //command.SelectCommand.CommandTimeout = Times;
                        command.SelectCommand.CommandTimeout = 60000;
                        command.Fill(ds, "ds");
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    connection.Close();
                }
                return ds;
            }
        }

        public static string CheckConnection(string strConnectionString)
        {
            string strMessage = "";
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                sqlConnection.ConnectionString = strConnectionString;
                sqlConnection.Open();
                if (sqlConnection.State == ConnectionState.Open)
                {
                    strMessage = "连接成功";
                }
                else
                {
                    strMessage = "连接失败";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                if (string.IsNullOrEmpty(ex.Message))
                {
                    strMessage = "连接失败";
                }
                else
                {
                    strMessage = ex.Message;
                }
            }
            finally
            {
                sqlConnection.Close();
            }
            return strMessage;
        }

        /// <summary>         
        /// 获取泛型集合         
        /// /// </summary>         
        /// /// <typeparam name="T">类型</typeparam>         
        /// /// <param name="connStr">数据库连接字符串</param>         
        /// <param name="sqlStr">要查询的T-SQL</param>         
        /// <returns></returns>         
        public static List<T> GetList<T>(string connStr, string sqlStr)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(sqlStr, conn))
                    {
                        DataSet ds = new DataSet();
                        sda.Fill(ds);
                        conn.Close();
                        return DataSetToList<T>(ds, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }
        /// <summary>         
        /// 获取datatable    
        /// /// </summary>         
        /// /// <param name="connStr">数据库连接字符串</param>         
        /// <param name="sqlStr">要查询的T-SQL</param>         
        /// <returns></returns>         
        public static DataTable GetDataTable(string connStr, string sqlStr)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(sqlStr, conn))
                    { 
                        DataTable table = new DataTable();
                        sda.Fill(table);
                        conn.Close();
                        Logger.Debug("row count"+table.Rows.Count.ToString());
                        return table;
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }

        /// <summary>         
        /// DataSetToList         
        /// </summary>          
        /// <typeparam name="T">转换类型</typeparam>         
        /// <param name="dataSet">数据源</param>         
        /// <param name="tableIndex">需要转换表的索引</param>        
        /// /// <returns>泛型集合</returns>
        public static List<T> DataSetToList<T>(DataSet dataset, int tableIndex)
        {
            //确认参数有效
            if (dataset == null || dataset.Tables.Count <= 0 || tableIndex < 0)
            {
                return null;
            }
            DataTable dt = dataset.Tables[tableIndex];
            Logger.Debug("row count" + dt.Rows.Count.ToString());
            List<T> list = new List<T>();
            string[] typrArray = new[] { "string", "int", "datetime", "decimal" };
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Type _T = typeof(T);
                    if (typrArray.Contains(_T.Name.ToLower()))
                    {
                        object str = dt.Rows[i][0];
                        if (str != null)
                        {
                            T _t = (T)Convert.ChangeType(str, typeof(T));
                            list.Add(_t);
                        }
                    }
                    else
                    {
                        #region
                        //创建泛型对象
                        T _t = Activator.CreateInstance<T>();
                        //获取对象所有属性
                        System.Reflection.PropertyInfo[] propertyInfo = _t.GetType().GetProperties();
                        //属性和名称相同时则赋值
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            foreach (System.Reflection.PropertyInfo info in propertyInfo)
                            {
                                if (dt.Columns[j].ColumnName.ToUpper().Equals(info.Name.ToUpper()))
                                {
                                    if (dt.Rows[i][j] != DBNull.Value)
                                    {
                                        info.SetValue(_t, dt.Rows[i][j], null);
                                    }
                                    else
                                    {
                                        info.SetValue(_t, null, null);
                                    }

                                    break;
                                }
                            }
                        }
                        list.Add(_t);
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }

            return list;
        }

        public static List<T> DataTableToList<T>(DataTable datatable)
        {
            //确认参数有效
            if (datatable == null || datatable.Rows.Count <= 0)
            {
                return null;
            }
            DataTable dt = datatable;

            List<T> list = new List<T>();
            string[] typrArray = new[] { "string", "int", "datetime", "decimal" };
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Type _T = typeof(T);
                    if (typrArray.Contains(_T.Name.ToLower()))
                    {
                        object str = dt.Rows[i][0];
                        if (str != null)
                        {
                            T _t = (T)Convert.ChangeType(str, typeof(T));
                            list.Add(_t);
                        }
                    }
                    else
                    {
                        #region
                        //创建泛型对象
                        T _t = Activator.CreateInstance<T>();
                        //获取对象所有属性
                        System.Reflection.PropertyInfo[] propertyInfo = _t.GetType().GetProperties();
                        //属性和名称相同时则赋值
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            foreach (System.Reflection.PropertyInfo info in propertyInfo)
                            {
                                if (dt.Columns[j].ColumnName.ToUpper().Equals(info.Name.ToUpper()))
                                {
                                    if (dt.Rows[i][j] != DBNull.Value)
                                    {
                                        info.SetValue(_t, dt.Rows[i][j], null);
                                    }
                                    else
                                    {
                                        info.SetValue(_t, null, null);
                                    }

                                    break;
                                }
                            }
                        }
                        list.Add(_t);
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return list;
        }



        #region 批量处理数据

        #endregion

        public static int ExecuteSql(string connectionString, string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = 60000;
                        int rows = cmd.ExecuteNonQuery();

                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        public static int ExecuteSql(SqlConnection connection, string SQLString)
        {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
                try
                {
                    cmd.CommandTimeout = 60000;
                    int rows = cmd.ExecuteNonQuery();

                    return rows;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    throw e;
                }
            }
        }
        public static bool ExecuteSql(string connectionString, string sqlStr, List<SqlParameter> parameter)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStr, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = 60000;
                        parameter.ForEach(p =>
                        {
                            cmd.Parameters.Add(p);
                        });
                        int r = cmd.ExecuteNonQuery();
                        if (r > 0)
                            return true;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sqlStr"></param>
        /// <param name="parameter"> 
        ///           List<SqlParameter> paras = new List<SqlParameter>();
        ///           paras.Add(new SqlParameter("@memberId", SqlDbType.Int) { Value = memberId });
        ///           paras.Add(new SqlParameter("@Score", SqlDbType.Decimal) { Value = para.Score });
        ///           paras.Add(new SqlParameter("@MSG", SqlDbType.VarChar,100) { Direction = ParameterDirection.Output }); </param>
        /// <returns></returns>
        public static Dictionary<string, string> ExecuteProcedure(string connectionString, string sqlStr, List<SqlParameter> parameter)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStr, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 60000;
                        parameter.ForEach(p =>
                        {
                            cmd.Parameters.Add(p);
                        });
                        cmd.ExecuteNonQuery();
                        foreach (SqlParameter sp in cmd.Parameters)
                        {
                            if (sp.Direction == ParameterDirection.Output)
                            {
                                dic.Add(sp.ParameterName, sp.Value.ToString());
                            }
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return dic;
        }

        public void Dispose()
        {
            connectionSQL.Close();
        }


        public static bool ExecuteSqlTransaction(string connectionString, List<string> sqlList )
        {
            bool resultBool = true;
         
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("SampleTransaction");

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    if (sqlList.Count > 0)
                    {
                        foreach (var item in sqlList)
                        {
                            command.CommandText = item;
                            command.ExecuteNonQuery();
                        }
                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();
                    Console.WriteLine("Both records are written to database.");
                }
                catch (Exception ex)
                {
                    resultBool = false;
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);
                    Logger.Error(ex);
                     
                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                       
                    }
                }
            }
            return resultBool;
        }

        public static bool ExecuteSqlTransaction(string connectionString, List<string> sqlList, out string message)
        {
            bool resultBool = true;
            message = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("SampleTransaction");

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    if (sqlList.Count > 0)
                    {
                        foreach (var item in sqlList)
                        {
                            command.CommandText = item;
                            command.ExecuteNonQuery();
                        }
                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();
                    Console.WriteLine("Both records are written to database.");
                }
                catch (Exception ex)
                {
                    resultBool = false;
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);
                    Logger.Error(ex);
                    message = ex.Message;
                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                        message = ex2.Message;
                    }
                }
            }
            return resultBool;
        }
    }
}
