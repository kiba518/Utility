using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Utility
{

    /// <summary>
    /// SQLite帮助类  具体配置和需要引用的dll 需要自行百度，搜索SQLiteConnection的用法
    /// </summary>
    public class SQLiteHelper
    {
        private SQLiteConnection _SQLiteConn = null;     //连接对象
        private SQLiteTransaction _SQLiteTrans = null;   //事务对象
        public string connectionStr = "";
        SQLiteConnectionStringBuilder connectionString;
        public SQLiteHelper()
        {
        }

        public void Open()
        { 
            _SQLiteConn.Open(); 
        }
        public void Close()
        {
            try
            {
                _SQLiteConn.Close();
            }
            catch { }
        }
        /// <summary>
        /// 绑定数据库
        /// </summary>
        /// <param name="fileName"></param>
        public void Bind(string fileName)
        { 
            try
            {
                CreateDataBaseFile(fileName);
                connectionString = new SQLiteConnectionStringBuilder
                {
                    Version = 3,
                    Pooling = true,
                    FailIfMissing = false,
                    DataSource = fileName
                };
                connectionStr = connectionString.ConnectionString;

                _SQLiteConn = new SQLiteConnection(connectionStr);
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }    
        }

        /// <summary>
        /// 创建数据库文件
        /// </summary>
        /// <param name="fileName"></param>
        private void CreateDataBaseFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                SQLiteConnection.CreateFile(fileName);
            }
        }
         
        public SQLiteConnection GetConn() { return _SQLiteConn; }
          
        public int CreateTable(string tableStr)
        {
            // 首先获取数据库连接
            SQLiteConnection conn = _SQLiteConn; if (conn == null) return -1;

            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = conn;
            cmd.CommandText = tableStr;
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <param name="cmdText"> 需要执行的命令文本 </param>
        /// <returns> 一个数据表集合 </returns>
        public DataTable GetDataTable(string cmdText)
        {
            // 首先获取数据库连接
            SQLiteConnection conn = _SQLiteConn; if (conn == null) return null;

            var dt = new DataTable();
            try
            {
                var cmd = new SQLiteCommand(conn) { CommandText = cmdText };
                using (var reader = cmd.ExecuteReader()) {
                    dt.Load(reader);
                }
            } catch (Exception ex) {
                Logger.Error(ex);
                dt = null;
            }

            return dt;
        }

        /// <summary>
        /// 预备命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        private void PrepareCommand(SQLiteConnection conn, SQLiteCommand cmd, string cmdText, SQLiteParameter[] commandParameters)
        {
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 30;

            if (commandParameters != null) {
                foreach (object parm in commandParameters) {
                    cmd.Parameters.Add(parm);
                }
            }
        }

        /// <summary>
        /// 返回受影响的行数
        /// </summary>
        /// <param name="cmdText">执行语句</param>
        /// <param name="commandParameters">传入的参数</param>
        /// <returns>返回受影响行数</returns>
        public int ExecuteNonQuery(string cmdText, SQLiteParameter[] commandParameters)
        {
            // 首先获取数据库连接
            SQLiteConnection conn = _SQLiteConn; if (conn == null) return -1;

            SQLiteCommand command = new SQLiteCommand();
            PrepareCommand(conn, command, cmdText, commandParameters);
            return command.ExecuteNonQuery();
        }
        public ConnectionState GetState()
        {
            if (_SQLiteConn != null)
            {
                return _SQLiteConn.State;
            }
            else
            {
                return ConnectionState.Closed;
            } 
        }
        /// <summary>
        /// 执行非查询命令
        /// </summary>
        /// <param name="cmdText"> 需要执行的命令文本 </param>
        /// <returns> 返回更新的行数 </returns>
        public int ExecuteNonQuery(string cmdText)
        {
            // 首先获取数据库连接
            SQLiteConnection conn = _SQLiteConn; if (conn == null) return -1;

            var cmd = new SQLiteCommand(conn) { CommandText = cmdText };
            var rowsUpdated = cmd.ExecuteNonQuery();

            return rowsUpdated;
        }

        /// <summary>
        /// 返回表集合
        /// </summary>
        /// <param name="cmdText">执行语句</param>
        /// <param name="commandParameters">传入的参数</param>
        /// <returns>返回DataSet</returns>
        public DataSet ExecuteDataset(string cmdText, SQLiteParameter[] commandParameters)
        {
            // 首先获取数据库连接
            SQLiteConnection conn = _SQLiteConn;    if (conn == null) return null;

            DataSet ds = new DataSet();
            SQLiteCommand command = new SQLiteCommand();
            PrepareCommand(conn, command, cmdText, commandParameters);
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            da.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 返回SqlDataReader对象
        /// </summary>
        /// <param name="cmdText">执行语句</param>
        /// <param name="commandParameters">传入的参数</param>
        /// <returns>返回SQLiteDataReader</returns>
        public SQLiteDataReader ExecuteReader(string cmdText, SQLiteParameter[] commandParameters)
        {
            // 首先获取数据库连接
            SQLiteConnection conn = _SQLiteConn; if (conn == null) return null;

            SQLiteCommand command = new SQLiteCommand();
            PrepareCommand(conn, command, cmdText, commandParameters);
            SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        /// <summary>
        /// 返回表第一行
        /// </summary>
        /// <param name="cmdText">执行语句</param>
        /// <param name="commandParameters">传入的参数</param>
        /// <returns>返回：第一行</returns>
        public DataRow ExecuteDataRow(string cmdText, SQLiteParameter[] commandParameters)
        {
            DataSet ds = ExecuteDataset(cmdText, commandParameters);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                return ds.Tables[0].Rows[0];
            }else {
                return null;
            }
        }

        /// <summary>
        /// 返回结果集中的第一行第一列，忽略其他行或列
        /// </summary>
        /// <param name="cmdText">执行语句</param>
        /// <param name="commandParameters">传入的参数</param>
        /// <returns>返回：第一行第一列</returns>
        public object ExecuteScalar(string cmdText, SQLiteParameter[] commandParameters)
        {
            // 首先获取数据库连接
            SQLiteConnection conn = _SQLiteConn; if (conn == null) return null;

            SQLiteCommand cmd = new SQLiteCommand();
            PrepareCommand(conn, cmd, cmdText, commandParameters);
            return cmd.ExecuteScalar();

        }

        /// <summary>
        /// 执行检索单项命令
        /// </summary>
        /// <param name="cmdText"> 需要执行的命令文本 </param>
        /// <returns> 一个字符串 </returns>
        public object ExecuteScalar(string cmdText)
        {
            // 首先获取数据库连接
            SQLiteConnection conn = _SQLiteConn; if (conn == null) return null;

            var cmd = new SQLiteCommand(conn) { CommandText = cmdText };
            return cmd.ExecuteScalar();
        }

        public List<T> FetchTable<T>(string tabName) {
            var dt = this.GetDataTable($"SELECT * FROM {tabName}");
            List<T> retList = SQLiteHelper.DataTableToList<T>(dt);
            return retList;
        }



        #region 高级方法

        /// <summary>
        /// 插入表数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="tableName">表名</param>
        /// <param name="dataList">数据集合</param>
        /// <returns>true或false</returns>
        public bool BatchInsert<T>(string tableName, List<T> dataList)
        {
            try
            {
                if (dataList != null && dataList.Count > 0)
                {
                    var temp = dataList[0];
                    PropertyInfo[] propertyInfos = temp.GetType().GetProperties();
                    List<string> propertyStrs = new List<string>();
                    string propertyStr = "";
                    foreach (var propertyInfo in propertyInfos)
                    {
                        propertyStrs.Add(propertyInfo.Name);
                        propertyStr = propertyStr + "@" + propertyInfo.Name + ",";
                    }
                    propertyStr = propertyStr.Remove(propertyStr.Length - 1);

                    using (var conn = (_SQLiteConn == null) ? (new SQLiteConnection(connectionStr)) : _SQLiteConn)
                    {
                        using (SQLiteCommand command = new SQLiteCommand(conn))
                        {
                            command.Connection.Open();
                            using (SQLiteTransaction transaction = conn.BeginTransaction())
                            {
                                command.Transaction = transaction;
                                command.CommandText = "insert into " + tableName + " values(" + propertyStr + ")";
                                foreach (var needInsertData in dataList)
                                {
                                    command.Parameters.Clear();
                                    for (int i = 0; i < propertyStrs.Count; i++)
                                    {
                                        command.Parameters.AddWithValue("@" + propertyStrs[i], propertyInfos[i].GetValue(needInsertData, null));
                                    }
                                    command.ExecuteNonQuery();
                                }
                                transaction.Commit();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 删除表数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>true或false</returns>
        public bool DeleteTableData(string tableName)
        {
            try
            {
                using (var conn = (_SQLiteConn == null) ? (new SQLiteConnection(connectionStr)) : _SQLiteConn)
                {
                    using (SQLiteCommand command = new SQLiteCommand(conn))
                    {
                        command.Connection.Open();
                        command.CommandText = "delete from " + tableName;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 开始数据库事务
        /// </summary>
        public void BeginTransaction()
        {
            _SQLiteTrans = this._SQLiteConn.BeginTransaction();
        }

        /// <summary>
        /// 开始数据库事务
        /// </summary>
        /// <param name="isoLevel">事务锁级别</param>
        public void BeginTransaction(IsolationLevel isoLevel)
        {
            _SQLiteTrans = this._SQLiteConn.BeginTransaction(isoLevel);
        }

        /// <summary>
        /// 提交当前挂起的事务
        /// </summary>
        public void Commit()
        {
            if (_SQLiteTrans != null)
                this._SQLiteTrans.Commit();
        }
        #endregion

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
                return new List<T>();
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

        /// <summary>
        /// 检查指定名称的 表Table 是否存在
        /// </summary>
        /// <param name="_db"></param>
        /// <param name="tabName"></param>
        /// <returns></returns>
        public bool ExistTable(string tabName) {
            string sql = $"SELECT COUNT(*) FROM Sqlite_master WHERE TYPE ='table' AND NAME = '{tabName}'";
            int count = Convert.ToInt32(ExecuteScalar(sql));
            return (count > 0);
        }

    }
}