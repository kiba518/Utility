using Aspose.Cells;
 
 
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Utility
{
    public class UFunction
    {
        public static int lcm(int a, int b)
        {
            int gcd = gcd1(a, b);
            return (a * b) / gcd;
        }
        public static int gcd1(int a, int b)
        {
            return b == 0 ? a : gcd1(b, a % b);
        }      
        #region Int

        public static int ToInt(object source, int def)
        {
            if (source != null && source != System.DBNull.Value)
            {
                string txt = source.ToString().ToLower();

                if (txt == "false")
                {
                    return 0;
                }
                else if (txt == "true")
                {
                    return 1;
                }
                else
                {
                    if (txt != "")
                    {
                        try
                        {
                            int iTemp = System.Convert.ToInt32(source.ToString());
                            return iTemp;
                        }
                        catch (System.OverflowException e)
                        {
                            return 0;
                        } 
                    }
                }
            }
            return def;
        }
        public static int DecimalToInt(object source)
        {
            int d = 0;
            if (source != null && source.ToString() != "")
            {
                string str = source.ToString();
                if (str.IndexOf(".") > 0)
                {
                    str = str.TrimEnd('0').TrimEnd('.');
                    string[] sarray = str.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                    if (sarray.Length > 1)
                    {
                        str = sarray[0];
                    }
                }
                int.TryParse(str, out d);
            }
            return d;
        }
        public static int ToSpecificInt(object source)
        {
            if (source != null && source != System.DBNull.Value)
            {
                string txt = source.ToString().ToLower().Trim();
                if (txt == "是")
                {
                    source = 1;
                }
                if (txt == "否")
                {
                    source = 0;
                }
                if (txt.ToLower() == "true")
                {
                    source = 1;
                }
                if (txt.ToLower() == "false")
                {
                    source = 1;
                }
            }

            return ToInt(source, -1);
        }
        public static int ToInt(object source)
        {
            return ToInt(source, -1);
        }



        #endregion


        #region double
        public static double ToDouble(object source)
        {
            return ToDouble(source, -1);
        }
        public static double ToDouble(object source, double def)
        {
            if (source != null && source != System.DBNull.Value && source.ToString() != "")
            {
                try
                {
                    def = double.Parse(source.ToString());
                }
                catch (Exception ex)
                {
                }
            }

            return def;
        }
        #endregion

        #region float
        public static double ToFloat(object source)
        {
            return ToFloat(source, -1);
        }
        public static double ToFloat(object source, double def)
        {
            if (source != null && source != System.DBNull.Value && source.ToString() != "")
            {
                try
                {
                    def = float.Parse(source.ToString());
                }
                catch (Exception ex)
                {
                }
            }

            return def;
        }
        #endregion

        public static bool IsNumeric(string value)
        {
            //return Regex.IsMatch(value, @"^[0-9]+([.][0-9]+){0,1}$");
            return Regex.IsMatch(value, @"^(\-)?\d+([.][0-9]+){0,1}$");
        }
        public static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[0-9]*$");
        }
        public static bool IsUnsign(string value)
        {
            return Regex.IsMatch(value, @"^/d*[.]?/d*$");
        }
        #region Deciaml
        /// <summary>
        /// 转换成金钱
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal ToMoney(object source)
        {
            //decimal d = 0;
            //if (source != null && source != System.DBNull.Value && source.ToString() != "")
            //{
            //    string str = source.ToString();
            //    if (str.IndexOf(".") > 0)
            //    {
            //        str = str.TrimEnd('0').TrimEnd('.');
            //        string[] sarray = str.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            //        if (sarray.Length > 1)
            //        {
            //            string s2 = sarray[1];
            //            if (s2.Length > 2)
            //            {
            //                s2 = s2.Substring(0, 2);
            //            }
            //            str = sarray[0] + "." + s2;
            //        }
            //    }
            //    decimal.TryParse(str, out d);
            //}
            //return d;
            return ToMoney(source, 6);
        }
        public static decimal ToMoney(object source, int ii)
        {
            decimal d = 0;
            if (source != null && source != System.DBNull.Value && source.ToString() != "")
            {
                string str = source.ToString();
                if (str.IndexOf(".") > 0)
                {
                    str = str.TrimEnd('0').TrimEnd('.');
                    string[] sarray = str.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                    if (sarray.Length > 1)
                    {
                        string s2 = sarray[1];
                        if (s2.Length > ii)
                        {
                            s2 = s2.Substring(0, ii);
                        }
                        str = sarray[0] + "." + s2;
                    }
                }
                decimal.TryParse(str, out d);
            }
            return d;
        }
        /// <summary>
        /// 转换成数量
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal ToNumber(object source, int ii)
        {
            decimal d = 0;
            if (source != null && source != System.DBNull.Value && source.ToString() != "")
            {
                string str = source.ToString();
                if (str.IndexOf(".") > 0)
                {
                    str = str.TrimEnd('0').TrimEnd('.');
                    string[] sarray = str.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                    if (sarray.Length > 1)
                    {
                        string s2 = sarray[1];
                        if (ii > 0 && s2.Length > ii)
                        {
                            s2 = s2.Substring(0, ii);
                        }
                        str = sarray[0] + "." + s2;
                    }
                }
                decimal.TryParse(str, out d);
            }
            return d;
        }
        public static decimal ToNumber(object source)
        {
            //decimal d = 0;
            //if (source != null && source != System.DBNull.Value && source.ToString() != "")
            //{
            //    string str = source.ToString();
            //    if (str.IndexOf(".") > 0)
            //    {
            //        string[] sarray = str.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            //        if (sarray.Length > 0)
            //        {
            //            str = sarray[0];
            //        }
            //    }
            //    decimal.TryParse(str, out d);
            //}
            return ToNumber(source, 6);
        }
        public static decimal ToDecimal(object source, decimal def)
        {
            try
            {
                if (source != null && source != System.DBNull.Value && source.ToString() != "")
                {
                    if (IsNumeric(source.ToString()))
                    {
                        decimal.TryParse(source.ToString(), out def);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return def;
        }

        public static decimal ToDec(object source)
        {
            if (source.ToString().Length > 0 && source.ToString().Substring(source.ToString().Length - 1) == ".")
            {
                source = source.ToString() + "0";
            }
            return ToDecimal(source, -1);
        }

        public static bool IsDecimal(object source)
        {
            bool re = false;

            if (source != null && source != System.DBNull.Value && source.ToString() != "")
            {
                try
                {
                    decimal def = decimal.Parse(source.ToString());

                    re = true;
                }
                catch (Exception ex)
                {
                    re = false;
                }
            }

            return re;
        }



        public static string FormatDecimal(object source)
        {
            return FormatDecimal(source, 2);
        }

        public static string FormatDecimal(object source, int num)
        {
            if (source == null || source == System.DBNull.Value)
                return null;

            string re = source.ToString();

            int pos = re.IndexOf('.');

            if (pos == -1)
                return re;

            re = Regex.Replace(re, @"[0]*$", "");

            re = Regex.Replace(re, @"\.$", "");

            pos = re.IndexOf('.');

            if (pos == -1)
            {
                if (num > 0)
                    re = re + "." + new string('0', num);
            }
            else
            {
                pos = re.Length - pos - 1;

                if (num > pos)
                    re = re + new string('0', num - pos);

            }

            return re;
        }

        public static string FormatNDecimal(object source)
        {
            if (ToDec(source) == 0)
                return "";

            return FormatDecimal(source);
        }

        public static decimal Round(decimal dec)
        {
            return Round(dec, 0);
        }

        public static decimal Round(decimal dec, string st)
        {
            return Round(dec, ToInt(st, 2));
        }

        public static decimal Floor(decimal dec, int st)
        {
            return (decimal)(Math.Floor((double)dec * Math.Pow(10, st)) / Math.Pow(10, st));
        }

        public static decimal Ceiling(decimal dec, int st)
        {
            return (decimal)(Math.Ceiling((double)dec * Math.Pow(10, st)) / Math.Pow(10, st));
        }

        public static decimal Round(decimal dec, int st)
        {
            if (st == 0)
                st = 1;
            else
                st = (int)Math.Pow(10, st);

            double num = (double)dec * st;
            double numL = Math.Floor(num);
            double numR = Math.Floor(num * 10) % 10;

            if (numR >= 5)
                numL += 1;

            return (decimal)numL / st;
        }

        public static double Round(double dec)
        {
            return Round(dec, 0);
        }

        public static double Round(double dec, int st)
        {
            if (st == 0)
                st = 1;
            else
                st = (int)Math.Pow(10, st);

            double num = (double)dec * st;
            double numL = Math.Floor(num);
            double numR = Math.Floor(num * 10) % 10;

            if (numR >= 5)
                numL += 1;

            return numL / st;
        }

        #endregion

        #region Boolean

        public static bool ToBoolean(object source, bool def)
        {
            if (source != null && source != System.DBNull.Value && source.ToString() != "")
            {
                string val = source.ToString();

                if (val == "0")
                    return false;

                if (val == "1")
                    return true;

                if (Regex.IsMatch(val, @"^[0-9\.]+$"))
                    return ToDecimal(val, 0) > 0;

                try
                {
                    def = bool.Parse(val);
                }
                catch (Exception ex)
                {
                }
            }

            return def;
        }

        public static bool ToBoolean(object source)
        {
            return ToBoolean(source, false);
        }

        public static bool IsBoolean(object source)
        {
            if (source == null || source == System.DBNull.Value || source.ToString() == "")
                return false;

            if (source.ToString() == "0" || source.ToString() == "1")
                return true;

            if (source.ToString().ToLower() == "true" || source.ToString().ToLower() == "false")
                return true;

            return false;
        }

        #endregion

        #region DataTable

        public static DataTable ToDataTable(object source, DataTable def)
        {
            if (source != null)
            {
                try
                {
                    def = (DataTable)source;
                }
                catch (Exception ex)
                {
                }
            }

            return def;
        }

        public static DataTable ToDataTable(object source)
        {
            return ToDataTable(source, null);
        }

        public static DataTable ToDataTable(DataView dv)
        {
            DataTable dt = dv.Table.Clone();

            dt.TableName = "dt";

            //if (!dt.Columns.Contains("RowState"))
            //    dt.Columns.Add("RowState", typeof(System.String));

            for (int i = 0; i < dv.Count; i++)
            {
                DataRow row = dv[i].Row;

                if (row.RowState == DataRowState.Deleted)
                {
                    row.RejectChanges();

                    row = dt.Rows.Add(row.ItemArray);

                    row.AcceptChanges();

                    //row["RowState"] = dv[i].Row.RowState.ToString();

                    row.Delete();
                }
                else if (row.RowState == DataRowState.Added)
                {
                    row = dt.Rows.Add(row.ItemArray);

                    //row["RowState"] = dv[i].Row.RowState.ToString();
                }
                else if (row.RowState == DataRowState.Modified)
                {
                    row = dt.Rows.Add(row.ItemArray);

                    //row["RowState"] = dv[i].Row.RowState.ToString();

                    row.AcceptChanges();

                    row.SetModified();
                }
                else if (row.RowState == DataRowState.Unchanged)
                {
                    row = dt.Rows.Add(row.ItemArray);

                    //row["RowState"] = dv[i].Row.RowState.ToString();

                    row.AcceptChanges();
                }
                else
                {
                    int d = 0;
                }
            }

            return dt;
        }

        public static DataTable ToDataTable(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0) return null;
            DataTable tmp = rows[0].Table.Clone();
            foreach (DataRow row in rows)
            {
                tmp.ImportRow(row);
            }
            return tmp;
        }
        public static DataSet ToDataSet(object source, DataSet def)
        {
            if (source != null)
            {
                try
                {
                    def = (DataSet)source;
                }
                catch (Exception ex)
                {
                }
            }

            return def;
        }

        public static DataSet ToDataSet(object source)
        {
            return ToDataSet(source, null);
        }

        public static string[][] HashTableToArray(Hashtable ht)
        {
            string[][] re = new string[ht.Count][];

            int i = 0;

            foreach (string key in ht.Keys)
            {
                re[i] = new string[2];

                re[i][0] = key;
                re[i][1] = ht[key].ToString();

                i++;
            }

            return re;
        }

        public static bool HasValue(object value)
        {
            if (value == null)
                return false;

            if (value == System.DBNull.Value)
                return false;

            return true;
        }

        public static bool HasValue(DataRow row, string columnName)
        {
            if (row[columnName] is System.DBNull)
                return false;

            if (row[columnName] == null)
                return false;

            if (row[columnName].ToString() == string.Empty)
                return false;

            return true;
        }

        public static bool HasValue(DataRowView row, string columnName)
        {
            if (row[columnName] is System.DBNull)
                return false;

            if (row[columnName] == null)
                return false;

            if (row[columnName].ToString() == string.Empty)
                return false;

            return true;
        }

        public static bool DataTableDeleteNotChangeRow(DataTable dt)
        {
            return DataTableDeleteNotChangeRow(dt, true, true, true);
        }

        public static bool DataTableDeleteNotChangeRow(DataTable dt, bool add, bool modify, bool delete)
        {
            if (dt == null) return true;
            if (dt.Rows.Count == 0) return true;

            try
            {
                if (add && modify && delete)
                {
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dt.Rows[i].RowState == DataRowState.Unchanged)
                        {
                            dt.Rows[i].Delete();
                            dt.Rows[i].AcceptChanges();
                        }
                    }
                }
                else
                {
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dt.Rows[i].RowState == DataRowState.Unchanged)
                        {
                            dt.Rows[i].Delete();
                            dt.Rows[i].AcceptChanges();
                        }
                        else if (!add && dt.Rows[i].RowState == DataRowState.Added)
                        {
                            dt.Rows[i].Delete();
                        }
                        else if (!delete && dt.Rows[i].RowState == DataRowState.Deleted)
                        {
                            dt.Rows[i].AcceptChanges();
                        }
                        else if (!modify && dt.Rows[i].RowState == DataRowState.Modified)
                        {
                            dt.Rows[i].Delete();
                            dt.Rows[i].AcceptChanges();
                        }
                    }
                }
            }
            catch { throw new Exception("删除datatable中没有发生改变的行,并提交...；发生错误！"); };

            return true;
        }

        public static bool DataTableDeleteColumns(DataTable dtDome, string[] saves, string[] dels)
        {
            if (dtDome == null) return true;
            if (dtDome.Rows.Count == 0) return true;

            if (saves != null)
            {
                for (int k = dtDome.Columns.Count - 1; k >= 0; k--)
                {
                    bool finded = false;

                    for (int i = 0; i < saves.Length; i++)
                    {
                        finded = (saves[i] == dtDome.Columns[k].ColumnName);

                        if (finded)
                            break;
                    }

                    if (!finded)
                        dtDome.Columns.RemoveAt(k);
                }
            }

            if (dels != null)
            {
                for (int i = 0; i < dels.Length; i++)
                {
                    if (dtDome.Columns.Contains(dels[i]))
                        dtDome.Columns.Remove(dels[i]);
                }
            }

            return true;
        }

        public static bool DataTableEnColumnNames(DataTable dtDome, string[] cols)
        {
            int amt = (int)'A';

            for (int i = 0; i < cols.Length; i++)
            {
                string name = "";

                if (i < 26)
                    name = ((char)(amt + i)).ToString();
                else
                    name = ((char)(amt + 25)).ToString() + (i - 25).ToString();


                if (dtDome.Columns.Contains(cols[i]))
                    dtDome.Columns[cols[i]].ColumnName = name;
            }

            return true;
        }

        public static bool DataTableUnColumnNames(DataTable dtDome, string[] cols)
        {
            int amt = (int)'A';

            for (int i = 0; i < cols.Length; i++)
            {
                string name = "";

                if (i < 26)
                    name = ((char)(amt + i)).ToString();
                else
                    name = ((char)(amt + 25)).ToString() + (i - 25).ToString();

                if (dtDome.Columns.Contains(name))
                    dtDome.Columns[name].ColumnName = cols[i];
            }

            return true;
        }

        public static bool HasChangeRow(DataTable dtDome)
        {
            if (dtDome == null) return false;
            if (dtDome.Rows.Count == 0) return false;

            for (int i = 0; i < dtDome.Rows.Count; i++)
            {
                if (dtDome.Rows[i].RowState != DataRowState.Unchanged)
                {
                    return true;
                }
            }

            return false;
        }

        public static object ConvertDBData(Type type, string value)
        {
            if (type == typeof(System.Int32))
            {
                if (IsInt(value))
                    return ToInt(value);
            }
            else if (type == typeof(System.Decimal))
            {
                if (IsDecimal(value))
                    return ToDec(value);
            }
            else if (type == typeof(System.Boolean))
            {
                if (IsBoolean(value))
                    return ToBoolean(value);
            }
            else if (type == typeof(System.DateTime))
            {
                if (IsDateTime(value) && ToDateTime(value, "1900-1-1") > DateTime.Parse("1900-1-1"))
                    return ToDateTime(value, "1900-1-1");
            }
            else if (type == typeof(System.String))
            {
                if (value != null)
                    return value;
            }

            return System.DBNull.Value;
        }

        public static DataTable EnSelectData(DataTable dt, string tableName, string columnName)
        {
            DataView dvSP = new DataView(dt);
            dvSP.RowStateFilter = DataViewRowState.CurrentRows;

            if (dt.Columns[columnName].DataType == typeof(System.Int32))
                dvSP.RowFilter = string.Format("not ({0} is null) || {0}=-1", columnName);
            else
                dvSP.RowFilter = string.Format("Trim({0})<>''", columnName);

            DataTable dt1 = dvSP.ToTable(tableName, true, columnName);
            DataTable dt2 = new DataTable();
            dt2.TableName = tableName;
            dt2.Columns.Add("编号", typeof(System.String));
            dt2.Columns.Add("名称", typeof(System.String));
            dt2.Columns.Add("助记码", typeof(System.String));

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (!dt1.Rows[i].IsNull(columnName))
                {
                    string key = dt1.Rows[i][columnName].ToString().Trim();

                    if (key != "")
                    {
                        if (dt2.Select(string.Format("名称='{0}'", fSQL(key))).Length == 0)
                        {
                            EnSelectData_Add(dt2, key);
                        }
                    }
                }
            }

            dt2.AcceptChanges();

            return dt2;
        }

        public static void EnSelectData_Add(DataTable dt, string text)
        {
            DataRow row = dt.NewRow();
            dt.Rows.Add(row);

            if (dt.Columns.Contains("编号"))
                row["编号"] = dt.Rows.Count;

            if (dt.Columns.Contains("助记码"))
                row["助记码"] = PY(text, true);

            row["名称"] = text;
        }

        public static void EnSelectData_Add(DataTable dt, string value, string text)
        {
            DataRow row = dt.NewRow();
            dt.Rows.Add(row);

            if (dt.Columns.Contains("编号"))
                row["编号"] = value;

            if (dt.Columns.Contains("助记码"))
                row["助记码"] = PY(text, true);

            row["名称"] = text;
        }

        public static DataTable EnSelectData_NewDataTable(string name)
        {
            DataTable dt = new DataTable();

            dt.TableName = name;

            dt.Columns.Add("编号", typeof(System.String));
            dt.Columns.Add("名称", typeof(System.String));
            dt.Columns.Add("助记码", typeof(System.String));

            return dt;
        }

        public static void DataTableMerge(DataTable dtTarget, DataTable dtMerge)
        {
            ArrayList alColumns = new ArrayList();

            for (int i = 0; i < dtTarget.Columns.Count; i++)
            {
                string name = dtTarget.Columns[i].ColumnName;

                if (!dtMerge.Columns.Contains(name))
                    continue;

                alColumns.Add(name);
            }

            for (int i = 0; i < dtMerge.Rows.Count; i++)
            {
                DataRow row = dtMerge.Rows[i];

                if (row.RowState == DataRowState.Deleted)
                    continue;

                DataRow nRow = dtTarget.NewRow();

                for (int k = 0; k < alColumns.Count; k++)
                {
                    string name = alColumns[k].ToString();

                    nRow[name] = row[name];
                }

                dtTarget.Rows.Add(nRow);
            }
        }

        #endregion

        #region String
        public static string ToString(object str)
        {
            if (str == null)
            {
                return "";
            }
            return str.ToString();
        }
        public static string ToJoin<T>(string separator, List<T> _list, bool isQuotes)
        {
            string returnStr = "";
            try
            {
                StringBuilder sb = new StringBuilder();
                var list = _list.Distinct().ToList();
                int _i = 0;
                foreach (var item in list)
                {
                    string str = "-9999";
                    if (item != null)
                    {
                        str = item.ToString().Trim();
                    }
                    else
                    {
                        continue;
                    }
                    if (_i > 0)
                    {
                        sb.Append(separator);
                    }
                    if (!isQuotes)
                    {
                        sb.Append(str);
                        _i++;
                    }
                    else
                    {
                        sb.Append("'" + str + "'");
                        _i++;
                    }
                }
                returnStr=sb.ToString();              
            }
            catch(Exception ex)
            {
                Logger.Error("OLFunc.ToJoin<T>(string separator, List<T> list, bool isQuotes)=>", ex);
                returnStr = "";
            }
            if (string.IsNullOrWhiteSpace(returnStr))
            {
                if (!isQuotes)
                {
                    return "-9999";
                }
                else
                {
                    return "'NULL'";
                }
            }
            return returnStr;
        }
        public static string Clear(string str)
        {
            if (str == null)
            {
                return "";
            }
            str = str.Replace("&nbsp;", "").Trim();
            return str;
        }
        public static string DeCode(string str)
        {
            string deStr = "";
            deStr = HttpUtility.HtmlDecode(str);
            return deStr;
        }

        public static string EncodingGetString(string str, Encoding encodingS, Encoding encodingT)
        {
            string encode = "";
            var byteStr = Encoding.Default.GetBytes(str);
            var byteStr1 = Encoding.ASCII.GetBytes(str);
            var byteStr2 = Encoding.UTF8.GetBytes(str);
            var byteStr3 = Encoding.Unicode.GetBytes(str);
            if (encodingT == null)
            {
                encodingT = Encoding.Default;
                encode = encodingT.GetString(byteStr);
            }
            else
            {
                encode = encodingT.GetString(byteStr);
            }
            return encode;
        }

        public static string EnCode(string str, Encoding encoding)
        {
            string encode = "";
            if (encoding == null)
            {
                encode = HttpUtility.UrlEncode(str, Encoding.UTF8);
            }
            else
            {
                encode = HttpUtility.UrlEncode(str, encoding);
            }
            return encode;
        }

        private static string _CRLF = "";
        public static string CRLF
        {
            get
            {
                if (_CRLF == "")
                    _CRLF = ((char)13).ToString() + ((char)10).ToString();

                return _CRLF;
            }
        }

        public static string ToNotNullString(object source, string def)
        {
            if (source != null && source != System.DBNull.Value)
            {
                try
                {
                    def = source.ToString();
                }
                catch (Exception ex)
                {
                }
            }

            return def;
        }

        public static string ToNotNullString(object source)
        {
            return ToNotNullString(source, string.Empty);
        }

        public static bool IsNullValue(object source)
        {
            return source == null || source == System.DBNull.Value || source.ToString() == "";
        }

        public static string fSQL(string source)
        {
            return Regex.Replace(source, "'", "''");
        }

        public static string PadLeft(string pad, int len, string source)
        {
            int len_Pad = GetLength(pad);
            int len_source = GetLength(source);

            if (len_source >= len)
                return source;

            for (int i = len_source + len_Pad; i <= len; i = i + len_Pad)
                source = pad + source;

            if (len - GetLength(source) == 1)
                source = "&nbsp;" + source;

            return source;
        }

        public static int GetLength(object arg)
        {
            if (arg == null)
                return 0;

            return GetLength(arg.ToString());
        }

        public static int GetLength(string arg)
        {
            return GetLength(arg, true);
        }

        public static int GetLength(string arg, bool clear)
        {
            if (arg == null)
                return 0;

            if (clear)
                arg = arg.Trim(' ');

            if (arg.Length == 0)
                return 0;

            char[] cs = arg.ToCharArray();
            int Amount = 0;

            for (int i = 0; i < arg.Length; i++)
            {
                Amount += Encoding.GetEncoding("gb2312").GetByteCount(cs[i].ToString());

                //if ((int)cs[i] > 255)
                //    Amount += 2;
                //else
                //    Amount += 1;
            }

            return Amount;
        }

        public static string CutLength(string txt, int length)
        {
            StringBuilder sb = new StringBuilder();

            int L = 0;

            for (int i = 0; i < txt.Length; i++)
            {
                string str = txt.Substring(i, 1);
                L += Encoding.Default.GetBytes(str).Length;

                if (L <= length)
                    sb.Append(str);
                else
                    break;
            }

            return sb.ToString();
        }

        public static string PY(string value, bool changeBrackets)
        {
            string re = string.Empty;
            char[] objarrey = value.ToCharArray();
            if (changeBrackets)
            {
                foreach (char obj in objarrey)
                {
                    switch (obj)
                    {
                        case '（':
                            re += "(";
                            break;
                        case '）':
                            re += ")";
                            break;
                        case '参':
                            re += "S";
                            break;
                        case '唑':
                            re += "Z";
                            break;
                        case '肟':
                            re += "W";
                            break;
                        default:
                            string str = PY2(obj.ToString());
                            if (str == "*")
                            {
                                if (HasChinese(obj.ToString()))
                                {
                                    str = GetCharSpellCode(obj.ToString());
                                }
                                else
                                {
                                    str = obj.ToString();
                                }
                            }
                            re += str;
                            break;
                    }

                }
            }
            else
            {
                foreach (char obj in objarrey)
                {
                    switch (obj)
                    {
                        case '参':
                            re += "S";
                            break;
                        case '唑':
                            re += "Z";
                            break;
                        case '肟':
                            re += "W";
                            break;

                        default:
                            string str = PY2(obj.ToString());
                            if (str == "*")
                            {
                                if (HasChinese(obj.ToString()))
                                {
                                    str = GetCharSpellCode(obj.ToString());
                                }
                                else
                                {
                                    str = obj.ToString();
                                }
                            }
                            re += str;
                            break;
                    }

                }
            }

            return re;
        }
        private static string GetCharSpellCode(string CnChar)
        {
            if (CnChar.CompareTo("驁") <= 0) return "A";
            if (CnChar.CompareTo("簿") <= 0) return "B";
            if (CnChar.CompareTo("錯") <= 0) return "C";
            if (CnChar.CompareTo("鵽") <= 0) return "D";
            if (CnChar.CompareTo("樲") <= 0) return "E";
            if (CnChar.CompareTo("鰒") <= 0) return "F";
            if (CnChar.CompareTo("腂") <= 0) return "G";
            if (CnChar.CompareTo("夻") <= 0) return "H";
            if (CnChar.CompareTo("攈") <= 0) return "J";
            if (CnChar.CompareTo("穒") <= 0) return "K";
            if (CnChar.CompareTo("鱳") <= 0) return "L";
            if (CnChar.CompareTo("旀") <= 0) return "M";
            if (CnChar.CompareTo("桛") <= 0) return "N";
            if (CnChar.CompareTo("漚") <= 0) return "O";
            if (CnChar.CompareTo("曝") <= 0) return "P";
            if (CnChar.CompareTo("囕") <= 0) return "Q";
            if (CnChar.CompareTo("鶸") <= 0) return "R";
            if (CnChar.CompareTo("蜶") <= 0) return "S";
            if (CnChar.CompareTo("籜") <= 0) return "T";
            if (CnChar.CompareTo("鶩") <= 0) return "W";
            if (CnChar.CompareTo("鑂") <= 0) return "X";
            if (CnChar.CompareTo("韻") <= 0) return "Y";
            if (CnChar.CompareTo("咗") <= 0) return "Z";
            return "*";

        }
        //private static string _PY(string value)
        //{
        //    try
        //    {
        //        if (value.CompareTo("吖") < 0) return value;


        //        //if (value.CompareTo("嚓") < 0) return "B";
        //        //if (value.CompareTo("咑") < 0) return "C";
        //        //if (value.CompareTo("妸") < 0) return "D";
        //        //if (value.CompareTo("发") < 0) return "E";
        //        //if (value.CompareTo("旮") < 0) return "F";
        //        //if (value.CompareTo("铪") < 0) return "G";
        //        //if (value.CompareTo("讥") < 0) return "H";
        //        //if (value.CompareTo("咔") < 0) return "J";
        //        //if (value.CompareTo("垃") < 0) return "K";
        //        //if (value.CompareTo("旀") < 0) return "L";
        //        //if (value.CompareTo("拏") < 0) return "M";
        //        //if (value.CompareTo("噢") < 0) return "N";
        //        //if (value.CompareTo("妑") < 0) return "O";
        //        //if (value.CompareTo("七") < 0) return "P";
        //        //if (value.CompareTo("亽") < 0) return "Q";
        //        //if (value.CompareTo("仨") < 0) return "R";
        //        //if (value.CompareTo("他") < 0) return "S";
        //        //if (value.CompareTo("哇") < 0) return "T";
        //        //if (value.CompareTo("夕") < 0) return "W";
        //        //if (value.CompareTo("丫") < 0) return "X";
        //        //if (value.CompareTo("帀") < 0) return "Y";
        //        //if (value.CompareTo("咗") <= 0) return "Z";

        //        if (value.CompareTo("驁") <= 0) return "A";
        //        if (value.CompareTo("簿") <= 0) return "B";
        //        if (value.CompareTo("錯") <= 0) return "C";
        //        if (value.CompareTo("鵽") <= 0) return "D";
        //        if (value.CompareTo("樲") <= 0) return "E";
        //        if (value.CompareTo("鰒") <= 0) return "F";
        //        if (value.CompareTo("腂") <= 0) return "G";
        //        if (value.CompareTo("夻") <= 0) return "H";
        //        if (value.CompareTo("攈") <= 0) return "J";
        //        if (value.CompareTo("穒") <= 0) return "K";
        //        if (value.CompareTo("鱳") <= 0) return "L";
        //        if (value.CompareTo("旀") <= 0) return "M";
        //        if (value.CompareTo("桛") <= 0) return "N";
        //        if (value.CompareTo("漚") <= 0) return "O";
        //        if (value.CompareTo("曝") <= 0) return "P";
        //        if (value.CompareTo("囕") <= 0) return "Q";
        //        if (value.CompareTo("鶸") <= 0) return "R";
        //        if (value.CompareTo("蜶") <= 0) return "S";
        //        if (value.CompareTo("籜") <= 0) return "T";
        //        if (value.CompareTo("鶩") <= 0) return "W";
        //        if (value.CompareTo("鑂") <= 0) return "X";
        //        if (value.CompareTo("韻") <= 0) return "Y";
        //        if (value.CompareTo("咗") <= 0) return "Z";

        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //    return "";
        //    //switch (value)
        //    //{
        //    //    case "唑":
        //    //        return "Z";
        //    //}

        //    //try
        //    //{
        //    //    byte[] ZW = new byte[2];
        //    //    long HZ_INT;
        //    //    ZW = System.Text.Encoding.Default.GetBytes(value);
        //    //    if (ZW.Length <= 1)
        //    //    {
        //    //        return value;
        //    //    }
        //    //    int i1 = (short)(ZW[0]);
        //    //    int i2 = (short)(ZW[1]);
        //    //    HZ_INT = i1 * 256 + i2; // expresstion 
        //    //    //table of the constant list 
        //    //    // 'A'; //45217..45252   // 'B'; //45253..45760                
        //    //    // 'C'; //45761..46317   // 'D'; //46318..46825                 
        //    //    // 'E'; //46826..47009   // 'F'; //47010..47296                
        //    //    // 'G'; //47297..47613   // 'H'; //47614..48118                 
        //    //    // 'J'; //48119..49061   // 'K'; //49062..49323                
        //    //    // 'L'; //49324..49895   // 'M'; //49896..50370                
        //    //    // 'N'; //50371..50613   // 'O'; //50614..50621                 
        //    //    // 'P'; //50622..50905   // 'Q'; //50906..51386 
        //    //    // 'R'; //51387..51445   // 'S'; //51446..52217                
        //    //    // 'T'; //52218..52697   //没有U,V                
        //    //    // 'W'; //52698..52979   // 'X'; //52980..53640                 
        //    //    // 'Y'; //53689..54480   // 'Z'; //54481..55289

        //    //    // HZ_INT match the constant 
        //    //    if ((HZ_INT >= 45217) && (HZ_INT <= 45252))
        //    //    {
        //    //        return "A";
        //    //    }
        //    //    if ((HZ_INT >= 45253) && (HZ_INT <= 45760))
        //    //    {
        //    //        return "B";
        //    //    }
        //    //    if ((HZ_INT >= 45761) && (HZ_INT <= 46317))
        //    //    {
        //    //        return "C";

        //    //    }
        //    //    if ((HZ_INT >= 46318) && (HZ_INT <= 46825))
        //    //    {
        //    //        return "D";
        //    //    }
        //    //    if ((HZ_INT >= 46826) && (HZ_INT <= 47009))
        //    //    {
        //    //        return "E";
        //    //    }
        //    //    if ((HZ_INT >= 47010) && (HZ_INT <= 47296))
        //    //    {
        //    //        return "F";
        //    //    }
        //    //    if ((HZ_INT >= 47297) && (HZ_INT <= 47613))
        //    //    {
        //    //        return "G";
        //    //    }
        //    //    if ((HZ_INT >= 47614) && (HZ_INT <= 48118))
        //    //    {
        //    //        return "H";
        //    //    }
        //    //    if ((HZ_INT >= 48119) && (HZ_INT <= 49061))
        //    //    {
        //    //        return "J";
        //    //    }
        //    //    if ((HZ_INT >= 49062) && (HZ_INT <= 49323))
        //    //    {
        //    //        return "K";
        //    //    }
        //    //    if ((HZ_INT >= 49324) && (HZ_INT <= 49895))
        //    //    {
        //    //        return "L";
        //    //    }
        //    //    if ((HZ_INT >= 49896) && (HZ_INT <= 50370))
        //    //    {
        //    //        return "M";
        //    //    }

        //    //    if ((HZ_INT >= 50371) && (HZ_INT <= 50613))
        //    //    {
        //    //        return "N";

        //    //    }
        //    //    if ((HZ_INT >= 50614) && (HZ_INT <= 50621))
        //    //    {
        //    //        return "O";
        //    //    }
        //    //    if ((HZ_INT >= 50622) && (HZ_INT <= 50905))
        //    //    {
        //    //        return "P";

        //    //    }
        //    //    if ((HZ_INT >= 50906) && (HZ_INT <= 51386))
        //    //    {
        //    //        return "Q";

        //    //    }
        //    //    if ((HZ_INT >= 51387) && (HZ_INT <= 51445))
        //    //    {
        //    //        return "R";
        //    //    }
        //    //    if ((HZ_INT >= 51446) && (HZ_INT <= 52217))
        //    //    {
        //    //        return "S";
        //    //    }
        //    //    if ((HZ_INT >= 52218) && (HZ_INT <= 52697))
        //    //    {
        //    //        return "T";
        //    //    }
        //    //    if ((HZ_INT >= 52698) && (HZ_INT <= 52979))
        //    //    {
        //    //        return "W";
        //    //    }
        //    //    if ((HZ_INT >= 52980) && (HZ_INT <= 53640))
        //    //    {
        //    //        return "X";
        //    //    }
        //    //    if ((HZ_INT >= 53689) && (HZ_INT <= 54480))
        //    //    {
        //    //        return "Y";
        //    //    }
        //    //    if ((HZ_INT >= 54481) && (HZ_INT <= 55289))
        //    //    {
        //    //        return "Z";
        //    //    }
        //    //}
        //    //catch
        //    //{
        //    //}

        //    //return ("");
        //}

        private static string PY2(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += _PY2(strText.Substring(i, 1));
            }
            return myStr;
        }

        private static string _PY2(string myChar)
        {
            byte[] arrCN = System.Text.Encoding.Default.GetBytes(myChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return System.Text.Encoding.Default.GetString(new byte[] { (byte)(65 + i) }, 0, 1).ToUpper();
                    }
                }
                return "*";
            }
            else return myChar;
        }

        #endregion

        #region DateTime

        public static string EnTimeString(string txt)
        {
            return "";
        }

        public static string EnDateTimeString(string txt)
        {
            string[] paras = txt.Split(' ');

            if (paras[0].Length < 6 || paras[0].Length > 8)
                return "";

            int year = UFunction.ToInt(paras[0].Substring(0, 4), -1);

            if (year < 1900)
                return "";

            int month = 0;
            int day = 0;

            if (paras[0].Length == 8)
            {
                month = UFunction.ToInt(paras[0].Substring(4, 2), -1);
                day = UFunction.ToInt(paras[0].Substring(6, 2), -1);
            }
            else if (paras[0].Length == 7)
            {
                month = UFunction.ToInt(paras[0].Substring(4, 2), -1);
                day = UFunction.ToInt(paras[0].Substring(6, 1), -1);

                if (month > 12)
                {
                    month = UFunction.ToInt(paras[0].Substring(4, 1), -1);
                    day = UFunction.ToInt(paras[0].Substring(5, 2), -1);
                }
            }
            else
            {
                month = UFunction.ToInt(paras[0].Substring(4, 1), -1);
                day = UFunction.ToInt(paras[0].Substring(5, 1), -1);
            }

            if (month < 1 || month > 12)
                return "";

            if (day < 1 || day > 31)
                return "";

            DateTime d = new DateTime(year, month, 1);

            d = d.AddDays(day - 1);

            if (d.Month != month)
                return "";

            if (paras.Length == 1 || paras[1] == "")
                return d.ToString("yyyy-MM-dd");

            int hour = 0;
            int minute = 0;

            if (paras[1].Length < 2 || paras[1].Length > 4)
                return "";

            if (paras[1].Length == 4)
            {
                hour = UFunction.ToInt(paras[1].Substring(0, 2), -1);
                minute = UFunction.ToInt(paras[1].Substring(2, 2), -1);
            }
            else if (paras[0].Length == 3)
            {
                hour = UFunction.ToInt(paras[1].Substring(0, 2), -1);
                minute = UFunction.ToInt(paras[1].Substring(2, 1), -1);

                if (hour > 23)
                {
                    hour = UFunction.ToInt(paras[0].Substring(0, 1), -1);
                    minute = UFunction.ToInt(paras[0].Substring(1, 2), -1);
                }
            }
            else
            {
                hour = UFunction.ToInt(paras[0].Substring(0, 1), -1);
                minute = UFunction.ToInt(paras[0].Substring(1, 1), -1);
            }

            if (hour < 0 || hour > 23)
                return "";

            if (minute < 0 || minute > 59)
                return "";

            d = d.AddHours(hour);
            d = d.AddMinutes(minute);

            return d.ToString("yyyy-MM-dd  HH:mm:ss");

        }

        public static DateTime ToXQ(object source, string def)
        {
            return ToDateTime(source, def);
        }

        private static DateTime _RealDateTiem = new DateTime(1900, 1, 1);

        public static bool IsRealDateTime(object source)
        {
            if (!IsDateTime(source))
                return false;

            DateTime d = source is DateTime ? (DateTime)source : DateTime.Parse(source.ToString());

            return d > _RealDateTiem;
        }

        public static DateTime ToDateTime(object source)
        {
            return ToDateTime(source, "1900-1-1");
        }

        public static DateTime ToDateTime(object source, string def)
        {
            if (source != System.DBNull.Value && source != null && source.ToString() != "")
            {
                if (source is DateTime)
                    return (DateTime)source;

                try
                {
                    DateTime de = DateTime.Parse(source.ToString().Trim());

                    def = source.ToString();
                }
                catch
                {
                }
            }

            return DateTime.Parse(def);
        }

        public static DateTime ToDateTime(object source, DateTime def)
        {
            if (source != System.DBNull.Value && source != null && source.ToString() != "")
            {
                if (source is DateTime)
                    return (DateTime)source;

                try
                {
                    def = DateTime.Parse(source.ToString());
                }
                catch
                {
                }
            }

            return def;
        }

        public static bool IsDateTime(object source)
        {
            if (source == null || source == System.DBNull.Value || source.ToString() == "")
                return false;

            if (source is DateTime)
                return true;

            bool back = true;

            try
            {
                DateTime d = DateTime.Parse(source.ToString());
            }
            catch
            {
                back = false;
            }

            return back;
        }
        public static bool IsDateTimeType(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return false;
            }
            DateTime d = DateTime.Now;
            return DateTime.TryParse(source, out d);

        }

        public static string FormatTime(DateTime d)
        {
            return string.Format("{0}:{1}:{2}", d.Hour.ToString().PadLeft(2, '0'), d.Minute.ToString().PadLeft(2, '0'), d.Second.ToString().PadLeft(2, '0'));
        }

        public static string FormatDate(DateTime d)
        {
            return string.Format("{0}-{1}-{2}", d.Year.ToString(), d.Month.ToString().PadLeft(2, '0'), d.Day.ToString().PadLeft(2, '0'));
        }

        public static string FormatIntervalDate(object source)
        {
            if (source == null || source == System.DBNull.Value)
                return string.Empty;

            string val = source.ToString();

            if (val.Length <= 4)
                return val;

            string re = val.Substring(0, 4) + "-";

            for (int i = 4; i < val.Length; i++)
            {
                re += val.Substring(i, 1);

                if (i % 2 == 1 && i != val.Length - 1)
                    re += "-";
            }

            return re;
        }

        public static string FormatIntervalTime(object source)
        {
            if (source == null || source == System.DBNull.Value)
                return string.Empty;

            string val = source.ToString();

            if (val.Length <= 2)
                return val;

            string re = val.Substring(0, 2) + ":";

            for (int i = 2; i < val.Length; i++)
            {
                re += val.Substring(i, 1);

                if (i % 2 == 1 && i != val.Length - 1)
                    re += ":";
            }

            return re;
        }

        #endregion

        #region enum
        public static T StringToEnum<T>(string name,out bool isSuccess)
        {
            isSuccess = true;
            try
            {
                T t = (T)Enum.Parse(typeof(T), name);
                return t;
            }
            catch
            {
                isSuccess = false;
                return default(T);
            }
        }
        #endregion

        //国药准字J20140154(<a style="color:#0083ce" href="http://app1.sfda.gov.cn/datasearch/face3/dir.html" target="_blank">国家食药局查询</a>)
        public static string ApprovalNumber(string p)
        {
            Regex regpic = new Regex(@"国药准字\S*(?<=\()");
            var apm = regpic.Match(p);
            string srcStr = apm.Value.Replace("(", "");
            return srcStr;
        }



        /// <summary>
        /// 获取字符串相似度
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static float StringSemblance(string str1, string str2)
        {
            if (string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(str2))
            {
                return 1;
            }
            else if (!string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2))
            {
                //计算两个字符串的长度。 
                int len1 = str1.Length;
                int len2 = str2.Length;
                //建立上面说的数组，比字符长度大一个空间 
                int[,] dif = new int[len1 + 1, len2 + 1];
                //赋初值，步骤B。 
                for (int a = 0; a <= len1; a++)
                {
                    dif[a, 0] = a;
                }
                for (int a = 0; a <= len2; a++)
                {
                    dif[0, a] = a;
                }
                //计算两个字符是否一样，计算左上的值 
                int temp;
                for (int i = 1; i <= len1; i++)
                {
                    for (int j = 1; j <= len2; j++)
                    {
                        if (str1[i - 1] == str2[j - 1])
                        {
                            temp = 0;
                        }
                        else
                        {
                            temp = 1;
                        }
                        //取三个值中最小的 
                        dif[i, j] = Math.Min(Math.Min(dif[i - 1, j - 1] + temp, dif[i, j - 1] + 1), dif[i - 1, j] + 1);
                    }
                }
                //Console.WriteLine("字符串\"" + str1 + "\"与\"" + str2 + "\"的比较");

                //取数组右下角的值，同样不同位置代表不同字符串的比较 
                //Console.WriteLine("差异步骤：" + dif[len1, len2]);
                //计算相似度 
                float similarity = 1 - (float)dif[len1, len2] / Math.Max(str1.Length, str2.Length);
                //Console.WriteLine("相似度：" + similarity);
                return similarity;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 删除字符串中的中文
        /// </summary>
        public static string DeleteChinese(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            string retValue = str;
            if (System.Text.RegularExpressions.Regex.IsMatch(str, @"[\u4e00-\u9fa5]"))
            {
                retValue = string.Empty;
                var strsStrings = str.ToCharArray();
                for (int index = 0; index < strsStrings.Length; index++)
                {
                    if (strsStrings[index] >= 0x4e00 && strsStrings[index] <= 0x9fa5)
                    {
                        continue;
                    }
                    retValue += strsStrings[index];
                }
            }
            return retValue;
        }

        /// <summary>
        /// 组合字符串(取数字和指定字符)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CombinationString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";

            string strCombination = "";

            //string[] strList = {"粒","丸","升","毫升","克","毫克","袋","片","支"};

            //获取数字
            string strNumber = Regex.Replace(str, "[0-9]", "", RegexOptions.IgnoreCase);

            str = str.Replace(strNumber, "");

            //获取字母
            string strLetter = Regex.Replace(str, "[a-z]", "", RegexOptions.IgnoreCase);

            if (strLetter.Length > 0)
            {
                strCombination = strNumber + strLetter.ToUpper();
            }
            else
            {
                if (str.Contains("升"))
                {
                    strLetter = "L";
                }
                else if (str.Contains("毫升"))
                {
                    strLetter = "ML";
                }
                else if (str.Contains("克"))
                {
                    strLetter = "G";
                }
                else if (str.Contains("毫克"))
                {
                    strLetter = "MG";
                }
                else
                {
                    strLetter = "";
                }

                strCombination = strNumber + strLetter.ToUpper();
            }

            return strCombination;
        }

        /// <summary>
        /// 字符串比较
        /// </summary>
        /// <param name="source">字符串一</param>
        /// <param name="target">字符串2</param>
        /// <param name="similarity">比率</param>
        /// <param name="isCaseSensitive">大小写</param>
        /// <returns>字距</returns>
        public static Int32 LevenshteinDistance(String source, String target, out Double similarity, Boolean isCaseSensitive = false)
        {
            if (String.IsNullOrEmpty(source))
            {
                if (String.IsNullOrEmpty(target))
                {
                    similarity = 1;
                    return 0;
                }
                else
                {
                    similarity = 0;
                    return target.Length;
                }
            }
            else if (String.IsNullOrEmpty(target))
            {
                similarity = 0;
                return source.Length;
            }

            String From, To;
            if (isCaseSensitive)
            {   // 大小写敏感  
                From = source;
                To = target;
            }
            else
            {   // 大小写无关  
                From = source.ToLower();
                To = target.ToLower();
            }

            // 初始化  
            Int32 m = From.Length;
            Int32 n = To.Length;
            Int32[,] H = new Int32[m + 1, n + 1];
            for (Int32 i = 0; i <= m; i++) H[i, 0] = i;  // 注意：初始化[0,0]  
            for (Int32 j = 1; j <= n; j++) H[0, j] = j;

            // 迭代  
            for (Int32 i = 1; i <= m; i++)
            {
                Char SI = From[i - 1];
                for (Int32 j = 1; j <= n; j++)
                {   // 删除（deletion） 插入（insertion） 替换（substitution）  
                    if (SI == To[j - 1])
                        H[i, j] = H[i - 1, j - 1];
                    else
                        H[i, j] = Math.Min(H[i - 1, j - 1], Math.Min(H[i - 1, j], H[i, j - 1])) + 1;
                }
            }

            // 计算相似度  
            Int32 MaxLength = Math.Max(m, n);   // 两字符串的最大长度  
            similarity = ((Double)(MaxLength - H[m, n])) / MaxLength;

            return H[m, n];    // 编辑距离  
        }
        /// <summary>
        /// 判断字符串中是否包含中文
        /// </summary>
        /// <param name="str">需要判断的字符串</param>
        /// <returns>判断结果</returns>
        public static bool HasChinese(string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }

        public static T DeSerializerFromFile<T>(string path)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader mem = new StreamReader(fs);
            var obj = (T)ser.Deserialize(mem);
            mem.Close();
            return obj;
        }

        public static void SerializerToFile<T>(T obj, string savePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            FileStream fs = new FileStream(savePath, FileMode.Create);
            ser.Serialize(fs, obj);
            fs.Close();
        }
        public static T DeSerializerFromString<T>(string xmlStr)
        {
            using (StringReader sr = new StringReader(xmlStr))
            {
                XmlSerializer xmldes = new XmlSerializer(typeof(T));
                return (T)xmldes.Deserialize(sr);
            }
        }

        public static string SerializerToString<T>(T obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            XmlSerializer xml = new XmlSerializer(typeof(T));
            //序列化对象  
            xml.Serialize(Stream, obj, namespaces);
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();
            sr.Dispose();
            Stream.Dispose();
            string replace = "<?xml version=\"1.0\"?>";
            str = str.Replace(replace, "").Trim();
            return str;
        }

        public static string XMLSerializerToString(object obj, Encoding encoding)
        {
            try
            {
                if (obj != null)
                {
                    XmlSerializer ser = new XmlSerializer(obj.GetType());
                    MemoryStream mem = new MemoryStream();
                    ser.Serialize(mem, obj);
                    string resXMLstr = encoding.GetString(mem.ToArray());
                    return resXMLstr;
                }
                else
                {
                    return "序列化对象为空";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return "序列化异常";
            }
        }
        public static string DataContractSerializerToString(object obj, Encoding encoding)
        {
            try
            {
                if (obj != null)
                {
                    DataContractSerializer ser = new DataContractSerializer(obj.GetType());
                    MemoryStream mem = new MemoryStream();
                    ser.WriteObject(mem, obj);
                    string resXMLstr = encoding.GetString(mem.ToArray());
                    return resXMLstr;
                }
                else
                {
                    return "序列化对象为空";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return "序列化异常";
            }
        }
        public static DataTable DeserializeDataTable(string pXml)
        {
            StringReader strReader = new StringReader(pXml);
            XmlReader xmlReader = XmlReader.Create(strReader);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            DataTable dt = serializer.Deserialize(xmlReader) as DataTable;
            return dt;
        }
        public static string ToHexString(byte[] bytes) // 0xae00cf => "AE00CF "
        {
            string hexString = string.Empty;

            if (bytes != null)
            {

                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {

                    strB.Append(bytes[i].ToString("X2"));

                }

                hexString = strB.ToString();

            } return hexString;

        }
        /// <summary>
        /// 给现有实体赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pasteModel">需要赋值的实体</param>
        /// <param name="copyModel">数据源实体</param>
        public static void CopyPropertyValue<T>(T pasteModel, T copyModel)
        {
            List<System.Reflection.PropertyInfo> p1 = pasteModel.GetType().GetProperties().ToList();
            List<System.Reflection.PropertyInfo> p2 = pasteModel.GetType().GetProperties().ToList();
            foreach (var item in p1)
            {
                var v = p2.Where(q => q.Name == item.Name && q.PropertyType == item.PropertyType).ToList();
                object[] objAttrs = item.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), true);
                if (v.Count > 0 && v[0].GetValue(copyModel, null) != item.GetValue(pasteModel, null) && objAttrs.Length < 1)
                {
                    item.SetValue(pasteModel, v[0].GetValue(copyModel, null), null);
                }
            }
        }
        public static void CopyPropertyValueNotList<T>(T source, T target)
        {
            List<System.Reflection.PropertyInfo> p1 = source.GetType().GetProperties().ToList();
            List<System.Reflection.PropertyInfo> p2 = source.GetType().GetProperties().ToList();
            foreach (var item in p1)
            {
                if (!item.PropertyType.Name.Contains("List") && !item.PropertyType.Name.Contains("ICollection"))
                {
                    var v = p2.Where(q => q.Name == item.Name && q.PropertyType == item.PropertyType).ToList();
                    object[] objAttrs = item.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), true);
                    if (v.Count > 0 && v[0].GetValue(target, null) != item.GetValue(source, null) && objAttrs.Length < 1)
                    {
                        item.SetValue(source, v[0].GetValue(target, null), null);
                    }
                }
            }
        }

        public static int ExceptStringInt(string str)
        {
            int result = 0;
            if (str != null && str != string.Empty)
            {
                // 正则表达式剔除非数字字符（不包含小数点.）
                str = Regex.Replace(str, @"[^\d.\d]", "");
                // 如果是数字，则转换为decimal类型
                if (Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$"))
                {
                    result = int.Parse(str);
                }
            }
            return result;
        }
        public static decimal ExceptStringDecimal(string str)
        {
            decimal result = 0;
            if (str != null && str != string.Empty)
            {
                // 正则表达式剔除非数字字符（不包含小数点.） 
                str = Regex.Replace(str, @"[^\d.\d]", "");
                // 如果是数字，则转换为decimal类型
                if (Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$"))
                {
                    result = decimal.Parse(str);
                }
            }
            return result;
        }
        public static string GetFirstNumber(string str)
        {
            string result = "";
            if (str != null && str != string.Empty)
            {
                var res = Regex.Matches(str, @"(\d+(\.\d+)?)");
                // 如果是数字，则转换为decimal类型
                if (res.Count > 0)
                {
                    result = res[0].Value;
                }
            }
            return result;
        }
        public static List<MacthColumn> GetMacthColumn<T>(T entity)
        {
            string tStr = string.Empty;
            if (entity == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] properties = entity.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return null;
            }

            List<MacthColumn> list = new List<MacthColumn>();

            foreach (System.Reflection.PropertyInfo item in properties)
            {

                MacthColumn mc = new MacthColumn();

                mc.ColumnName = item.Name; //名称   
                string des = "";
                try
                {
                    des = ((DescriptionAttribute)Attribute.GetCustomAttribute(item, typeof(DescriptionAttribute))).Description;// 属性值  
                }
                catch (Exception e)
                {
                    des = "";
                }
                mc.ColumnNameCn = des;
                mc.ColumnType = item.PropertyType.ToString();
                if (!string.IsNullOrWhiteSpace(des))
                {
                    list.Add(mc);
                }
            }
            return list;
        }

        #region ExportExcel
        public static void ExportExcel<T>(string name, List<T> list, Action<string> callback)
        {
            if (list != null && list.Count > 0)
            {
                System.Windows.Forms.SaveFileDialog frm = new System.Windows.Forms.SaveFileDialog();
                frm.Filter = "Excel文件(*.xls,xlsx)|*.xls;*.xlsx";
                frm.FileName = name + ".xlsx";
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    new Action<string, List<T>, Action<string>>(Export).BeginInvoke(frm.FileName, list, callback, null, null);
                }
                else
                {
                    callback("您取消了保存");
                }
            }
            else
            {
                callback("导出数据不可为空");
            }
        }
        public static void ExportExcelByName<T>(string fileName, List<T> list, Action<string> callback)
        {
            if (list != null && list.Count > 0)
            {               
                new Action<string, List<T>, Action<string>>(Export).BeginInvoke(fileName, list, callback, null, null);
            }
            else
            {
                callback("导出数据不可为空");
            }
        }
       

        public static void ExportExcel<T>(List<string> fileNameList, List<T> list, Action<string> callback)
        {
            if (list != null && list.Count > 0)
            {
                System.Windows.Forms.FolderBrowserDialog frm = new System.Windows.Forms.FolderBrowserDialog();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    string Path = frm.SelectedPath;
                    if (fileNameList.Count > 0)
                    {
                        new Action<List<string>, List<T>, string, Action<string>>(Export).BeginInvoke(fileNameList, list, Path, callback, null, null);
                    }
                    else
                    {
                        callback("不存在导出的供应商");
                    }
                }
                else
                {
                    callback("您取消了保存");
                }
            }
            else
            {
                callback("导出数据不可为空");
            }
        }

        public static void ExportExcel(DataTable dataTable, Action<string> callback)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                System.Windows.Forms.SaveFileDialog frm = new System.Windows.Forms.SaveFileDialog();
                frm.Filter = "Excel文件(*.xls,xlsx)|*.xls;*.xlsx";
                frm.FileName = "result.xlsx";
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    new Action<string, DataTable, Action<string>>(Export).BeginInvoke(frm.FileName, dataTable, callback, null, null);
                }
                else
                {
                    callback("您取消了保存");
                }
            }
            else
            {
                callback("导出数据不可为空");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="dataTableCn">传入表结构与dataTable列相同 匹配上为不导入的数据  匹配不上为导入的数据</param>
        /// <param name="callback"></param>
        public static void ExportExcel(DataTable dataTable, List<MacthColumn> MacthColumnList, Action<string> callback)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                System.Windows.Forms.SaveFileDialog frm = new System.Windows.Forms.SaveFileDialog();
                frm.Filter = "Excel文件(*.xls,xlsx)|*.xls;*.xlsx";
                frm.FileName = "result.xlsx";
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    #region 匹配选中项
                    List<string> RemoveList = new List<string>();
                    for (int cellColumn = 0; cellColumn < dataTable.Columns.Count; cellColumn++)
                    {
                        bool isBool = false;
                        foreach (MacthColumn propertyInfo in MacthColumnList)
                        {
                            if (dataTable.Columns[cellColumn].ColumnName == propertyInfo.ColumnName)
                            {
                                dataTable.Columns[cellColumn].ColumnName = propertyInfo.ColumnNameCn;
                                isBool = true;
                            }
                        }
                        if (isBool == false)
                        {
                            RemoveList.Add(dataTable.Columns[cellColumn].ColumnName);
                        }
                    }
                    foreach (string item in RemoveList)
                    {
                        dataTable.Columns.Remove(item);
                    }
                    #endregion
                    new Action<string, DataTable, Action<string>>(Export).BeginInvoke(frm.FileName, dataTable, callback, null, null);
                }
                else
                {
                    callback("您取消了保存");
                }
            }
            else
            {
                callback("导出数据不可为空");
            }
        }
        private static void Export(string fileName, DataTable dataTable, Action<string> callback)
        {
            string resultMsg = "";
            try
            {
                string strpath = fileName;
                if (FileHelper.IsExistFile(strpath))
                {
                    FileHelper.DeleteFile(strpath);
                }
                FileHelper.ClearFile(strpath);
                Workbook workbook = null;
                workbook = new Workbook(strpath);
                Worksheet sheet = workbook.Worksheets[0]; //工作表
                Cells cells = sheet.Cells;//单元格 
                Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];//新增样式
                style = SettingCellStyle();
                ExportToExcel(dataTable, cells);
                workbook.Save(strpath, SaveFormat.Xlsx);
                resultMsg = "保存成功";

            }
            catch (Exception ex)
            {
                resultMsg = "保存失败：" + ex.Message;
                Logger.Error(ex);
            }
            finally
            {
                callback(resultMsg);
            }

        }
        private static void Export<T>(string fileName, List<T> list, Action<string> callback)
        {
            string resultMsg = "";
            try
            {
                string strpath = fileName;
                if (FileHelper.IsExistFile(strpath))
                {
                    FileHelper.DeleteFile(strpath);
                }
                FileHelper.ClearFile(strpath);
                Workbook workbook = null;
                workbook = new Workbook(strpath);
                Worksheet sheet = workbook.Worksheets[0]; //工作表
                Cells cells = sheet.Cells;//单元格 
                Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];//新增样式
                style = SettingCellStyle();
                ExportToExcel<T>(list, cells);
                workbook.Save(strpath, SaveFormat.Xlsx);
                resultMsg = "保存成功";

            }
            catch (Exception ex)
            {
                resultMsg = "保存失败：" + ex.Message;
                Logger.Error(ex);
            }
            finally
            {
                callback(resultMsg);
            }

        }
 
        /// <summary>
        /// 文件夹
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">路径</param>
        /// <param name="list">数据</param>
        /// <param name="callback"></param>
        private static void Export<T>(List<string> fileNameList, List<T> list, string path, Action<string> callback)
        {
            string resultMsg = "";
            try
            {
                int i = 0;
                foreach (var item in fileNameList)
                {
                    i = i + 1;
                    string strpath = path + "\\" + item + "(" + i + ").xlsx"; ;
                    if (FileHelper.IsExistFile(strpath))
                    {
                        FileHelper.DeleteFile(strpath);
                    }
                    FileHelper.ClearFile(strpath);
                    Workbook workbook = null;
                    workbook = new Workbook(strpath);
                    Worksheet sheet = workbook.Worksheets[0]; //工作表
                    Cells cells = sheet.Cells;//单元格 
                    Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];//新增样式
                    style = SettingCellStyle();

                    Type type = typeof(T);


                    ExportToExcel<T>(list, cells);

                    workbook.Save(strpath, SaveFormat.Xlsx);
                    Thread.Sleep(500);
                }
                resultMsg = "导出成功";

            }
            catch (Exception ex)
            {
                resultMsg = "导出失败：" + ex.Message;
                Logger.Error(ex);
            }
            finally
            {
                callback(resultMsg);
            }

        }

        private static void ExportToExcel(DataTable dataTable, Cells cells)
        {

            for (int i = 0; i <= dataTable.Rows.Count; i++)
            {
                if (i == 0)
                {
                    for (int cellColumn = 0; cellColumn < dataTable.Columns.Count; cellColumn++)
                    {
                        var mc = dataTable.Columns[cellColumn];
                        Aspose.Cells.Style style = SettingCellStyle();
                        cells[i, cellColumn].PutValue(mc.ColumnName);//填写内容 
                        style.Font.IsBold = true;
                        cells[i, cellColumn].SetStyle(style);//给单元格关联样式  
                    }

                }
                else
                {
                    var itemT = dataTable.Rows[i - 1];
                    for (int cellColumn = 0; cellColumn < dataTable.Columns.Count; cellColumn++)
                    {
                        Aspose.Cells.Style style = new Style();

                        DataColumn dColumn = dataTable.Columns[cellColumn] as DataColumn;
                        if (dColumn.DataType.ToString().Contains("System.DateTime"))
                        {
                            style = SettingCellStyleTime();
                        }
                        else
                        {
                            style = SettingCellStyle();
                        }

                        //bool resultbool =  IsDateTimeType(itemT[cellColumn].ToString());
                        //if (resultbool)
                        //{
                        //    style = SettingCellStyleTime();
                        //}
                        //else
                        //{
                        //    style = SettingCellStyle();
                        //}
                        cells[i, cellColumn].PutValue(itemT[cellColumn]);//填写内容
                        cells[i, cellColumn].SetStyle(style);//给单元格关联样式  
                        int columnWidth = cells.GetColumnWidthPixel(cellColumn) + 10;
                        if (columnWidth >= 255)
                        {
                            columnWidth = 254;
                        }
                        else
                        {

                        }
                        cells.SetColumnWidthPixel(cellColumn, columnWidth);
                    }
                }
            }

        }
        private static void ExportToExcel<T>(List<T> list, Cells cells)
        {
            var first = list[0];
            PropertyInfo[] properties = first.GetType().GetProperties();
            List<MacthColumn> columnList = UFunction.GetMacthColumn<T>(first);


            for (int i = 0; i <= list.Count; i++)
            {
                if (i == 0)
                {
                    int Count = 0;
                    for (int cellColumn = 0; cellColumn < columnList.Count; cellColumn++)
                    {
                        MacthColumn mc = columnList[cellColumn];

                        Aspose.Cells.Style style = SettingCellStyle();
                        cells[i, cellColumn + Count].PutValue(mc.ColumnNameCn);//填写内容 
                        style.Font.IsBold = true;
                        cells[i, cellColumn + Count].SetStyle(style);//给单元格关联样式  }

                    }

                }
                else
                {
                    int Count = 0;
                    var itemT = list[i - 1];
                    var color = UFunction.GetPropertyValue(itemT, "ColumnBackgroundColor");
                    string colorStr = "";
                    if (color != null)
                    {
                        colorStr = color.ToString();
                    }
                    for (int cellColumn = 0; cellColumn < columnList.Count; cellColumn++)
                    {
                        MacthColumn mc = columnList[cellColumn];
                        foreach (System.Reflection.PropertyInfo propertyInfo in properties)
                        {
                            if (propertyInfo.Name == mc.ColumnName)
                            {
                                object putValue = propertyInfo.GetValue(itemT, null);


                                Aspose.Cells.Style style = SettingCellStyle(colorStr);

                                if (propertyInfo.PropertyType.FullName.Contains("System.DateTime"))
                                {
                                    style = SettingCellStyleTime(colorStr);
                                }
                                else
                                {
                                    style = SettingCellStyle(colorStr);
                                }
                                cells[i, cellColumn + Count].PutValue(putValue);//填写内容
                                cells[i, cellColumn + Count].SetStyle(style);//给单元格关联样式 
                                int columnWidth = cells.GetColumnWidthPixel(cellColumn + Count) + 10;
                                if (columnWidth >= 255)
                                {
                                    columnWidth = 254;
                                }
                                else
                                {

                                }
                                cells.SetColumnWidthPixel(cellColumn + Count, columnWidth);
                                continue;

                            }

                        }

                    }
                }
            }

        }
        private static void ExportToExcel<T>(T temp, Cells cells, int rowI, int colI, out int columnI)
        {
            string ExportColumn = ",ManuFactorName,DrugGenericName,Specification,".ToUpper();

            columnI = 0;
            PropertyInfo[] properties = temp.GetType().GetProperties();
            List<MacthColumn> columnList = UFunction.GetMacthColumn<T>(temp);
            for (int cellColumn = 0; cellColumn < columnList.Count; cellColumn++)
            {
                MacthColumn mc = columnList[cellColumn];
                foreach (System.Reflection.PropertyInfo propertyInfo in properties)
                {
                    if (propertyInfo.Name == mc.ColumnName && ExportColumn.Contains("," + mc.ColumnName.ToUpper() + ","))
                    {
                        object putValue = propertyInfo.GetValue(temp, null);

                        Aspose.Cells.Style style = SettingCellStyle();

                        if (propertyInfo.PropertyType.FullName.Contains("System.DateTime"))
                        {
                            style = SettingCellStyleTime();
                        }
                        else
                        {
                            style = SettingCellStyle();
                        }
                        cells[rowI, colI + columnI].PutValue(putValue);//填写内容
                        cells[rowI, colI + columnI].SetStyle(style);//给单元格关联样式  
                        int columnWidth = cells.GetColumnWidthPixel(colI + columnI) + 10;
                        if (columnWidth >= 255)
                        {
                            columnWidth = 254;
                        }
                        else
                        {

                        }
                        cells.SetColumnWidthPixel(colI + columnI, columnWidth);
                        columnI++;
                        continue;
                    }
                }
            }
            if (columnI > 0)
            {
                columnI = columnI - 1;
            }

        }
        private static void ExportToExcelHeader<T>(T temp, Cells cells, int rowI, int colI, out int columnI)
        {
            string ExportColumn = ",ManuFactorName,DrugGenericName,Specification,".ToUpper();

            columnI = 0;
            PropertyInfo[] properties = temp.GetType().GetProperties();
            List<MacthColumn> columnList = UFunction.GetMacthColumn<T>(temp);
            for (int cellColumn = 0; cellColumn < columnList.Count; cellColumn++)
            {
                MacthColumn mc = columnList[cellColumn];
                foreach (System.Reflection.PropertyInfo propertyInfo in properties)
                {
                    if (propertyInfo.Name == mc.ColumnName && ExportColumn.Contains("," + mc.ColumnName.ToUpper() + ","))
                    {
                        Aspose.Cells.Style style = SettingCellStyle();
                        cells[rowI, colI + columnI].PutValue(mc.ColumnNameCn);//填写内容 
                        style.Font.IsBold = true;
                        cells[rowI, colI + columnI].SetStyle(style);//给单元格关联样式  
                        columnI++;
                    }
                }
            }
            if (columnI > 0)
            {
                columnI = columnI - 1;
            }

        }

        private static Aspose.Cells.Style SettingCellStyle(string color = "")
        {
            Aspose.Cells.Style style = new Aspose.Cells.Style();
            style.HorizontalAlignment = TextAlignmentType.Center;//文字居中
            style.Font.Name = "宋体";//文字字体
            style.Font.Size = 10;//文字大小
            style.IsLocked = false;//单元格解锁
            style.Font.IsBold = false;//粗体
            if (string.IsNullOrWhiteSpace(color))
            {
                style.ForegroundColor = Color.FromArgb(255, 255, 255);//设置背景色
            }
            else
            {
                if (color.Length > 6)
                {
                    style.ForegroundColor = System.Drawing.Color.FromArgb(
                    System.Int32.Parse(color.Substring(1, 2), System.Globalization.NumberStyles.AllowHexSpecifier),
                    System.Int32.Parse(color.Substring(3, 2), System.Globalization.NumberStyles.AllowHexSpecifier),
                    System.Int32.Parse(color.Substring(5, 2), System.Globalization.NumberStyles.AllowHexSpecifier)
                    ); //设置背景色
                }
                else
                {
                    style.ForegroundColor = Color.FromArgb(255, 255, 255);//设置背景色
                }
            }
            style.Pattern = BackgroundType.Solid; //设置背景样式
            style.IsTextWrapped = true;//单元格内容自动换行            
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin; //应用边界线 左边界线
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin; //应用边界线 右边界线
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin; //应用边界线 上边界线
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin; //应用边界线 下边界线
            return style;
        }
        private static Aspose.Cells.Style SettingCellStyleTime(string color = "")
        {
            Aspose.Cells.Style style = new Aspose.Cells.Style();
            style.HorizontalAlignment = TextAlignmentType.Center;//文字居中
            style.Font.Name = "宋体";//文字字体
            style.Font.Size = 10;//文字大小
            style.IsLocked = false;//单元格解锁
            style.Font.IsBold = false;//粗体
            if (string.IsNullOrWhiteSpace(color))
            {
                style.ForegroundColor = Color.FromArgb(255, 255, 255);//设置背景色
            }
            else
            {
                if (color.Length > 6)
                {
                    style.ForegroundColor = System.Drawing.Color.FromArgb(
                    System.Int32.Parse(color.Substring(1, 2), System.Globalization.NumberStyles.AllowHexSpecifier),
                    System.Int32.Parse(color.Substring(3, 2), System.Globalization.NumberStyles.AllowHexSpecifier),
                    System.Int32.Parse(color.Substring(5, 2), System.Globalization.NumberStyles.AllowHexSpecifier)
                    ); //设置背景色
                }
                else
                {
                    style.ForegroundColor = Color.FromArgb(255, 255, 255);//设置背景色
                }
            }
            //style.Number = 22;
            style.Custom = "yyyy-MM-dd";
            style.Pattern = BackgroundType.Solid; //设置背景样式
            style.IsTextWrapped = true;//单元格内容自动换行  
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin; //应用边界线 左边界线
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin; //应用边界线 右边界线
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin; //应用边界线 上边界线
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin; //应用边界线 下边界线
            return style;
        }
        #endregion


       
        public static void ForEachParallel<T>(List<T> source, Action<T> method)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            ParallelOptions pOption = new ParallelOptions() { CancellationToken = cts.Token };
            pOption.MaxDegreeOfParallelism = 20;
            Parallel.ForEach(source, pOption, item =>
            {
                method(item);
            });

        }
        public static bool ForParallelWaiteResult(int fromInclusive, int toExclusive, Action<int> method)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            ParallelOptions pOption = new ParallelOptions() { CancellationToken = cts.Token };
            pOption.MaxDegreeOfParallelism = 20;
            BlockingCollection<int> excuteCount = new BlockingCollection<int>();
            ParallelLoopResult result = Parallel.For(fromInclusive, toExclusive, pOption, item =>
            {
                method(item);
                excuteCount.Add(1);
            });
            while (true)
            {
                if (toExclusive == excuteCount.Count())
                {
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
            return true;
        }
        public static bool ForEachParallelWaiteResult<T>(List<T> source, Action<T> method)
        {
            //object objlock = new object();
            CancellationTokenSource cts = new CancellationTokenSource();
            ParallelOptions pOption = new ParallelOptions() { CancellationToken = cts.Token };
            pOption.MaxDegreeOfParallelism = 20;
            BlockingCollection<int> excuteCount = new BlockingCollection<int>();
            ParallelLoopResult result = Parallel.ForEach(source, pOption, item =>
            {
                //lock (objlock)
                //{
                method(item);
                excuteCount.Add(1);
                //}
            });
            while (true)
            {
                if (source.Count() == excuteCount.Count())
                {
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
            return true;
        }
        public static bool ForEachParallelWaiteResult<T>(ObservableCollection<T> source, Action<T> method)
        {
            //object objlock = new object();
            CancellationTokenSource cts = new CancellationTokenSource();
            ParallelOptions pOption = new ParallelOptions() { CancellationToken = cts.Token };
            pOption.MaxDegreeOfParallelism = 20;
            BlockingCollection<int> excuteCount = new BlockingCollection<int>();
            ParallelLoopResult result = Parallel.ForEach(source, pOption, item =>
            {
                //lock (objlock)
                //{
                method(item);
                excuteCount.Add(1);
                //}
            });
            while (true)
            {
                if (source.Count() == excuteCount.Count())
                {
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
            return true;
        }
        public static bool ForEachParallelWaiteResult<T>(BlockingCollection<T> source, Action<T> method)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            ParallelOptions pOption = new ParallelOptions() { CancellationToken = cts.Token };
            pOption.MaxDegreeOfParallelism = 20;
            BlockingCollection<int> excuteCount = new BlockingCollection<int>();
            ParallelLoopResult result = Parallel.ForEach(source, pOption, item =>
            {
                method(item);
                excuteCount.Add(1);
            });
            while (true)
            {
                if (source.Count() == excuteCount.Count())
                {
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
            return true;
        }
        public static void ForGetIDStr<T>(List<T> source, int autoIndexNum, int autoRowNum, Action<string, int> method)
        {
            Type t = typeof(T);
            bool b = true;
            bool b2 = true;
            Logger.Debug(t.Name);
            if (t.Name.ToLower().Equals("int"))
            {
                b2 = false;
                b = false;
            }
            if (t.Name.ToLower().Equals("string"))
            {
                b2 = false;
            }
            if (b2)
            {
                throw new Exception("使用集合类型不正确，只能使用int,string类型（ForGetIDStr）");
            }
            if (source.Count > autoIndexNum)
            {
                int _arrayCount = source.Count / autoRowNum + ((source.Count % autoRowNum) > 0 ? 1 : 0);
                for (int _i = 0; _i < _arrayCount; _i++)
                {
                    //OLFunc.ForParallelWaiteResult(0, _arrayCount, (_i) =>
                    //{
                    Logger.Info("开始(ForGetIDStr)" + _i.ToString() + ":" + DateTime.Now.ToString());
                    int _pageNum = _i * autoRowNum;
                    var _list = source.Skip(_pageNum).Take(autoRowNum).ToList();
                    string drugNoListStr = UFunction.ToJoin(",", _list, b);
                    method(drugNoListStr, _i);
                    Logger.Info("结束(ForGetIDStr)" + _i.ToString() + ":" + DateTime.Now.ToString());
                    //});
                }
            }
            else
            {
                string drugNoListStr = UFunction.ToJoin(",", source, b);
                method(drugNoListStr, 0);
            }
        }
        public static void ForParallelGetIDStr<T>(List<T> source, int autoIndexNum, int autoRowNum, Action<string, int> method)
        {
            Type t = typeof(T);
            bool b = true;
            bool b2 = true;
            Logger.Debug(t.Name);
            if (t.Name.ToLower().Equals("int"))
            {
                b2 = false;
                b = false;
            }
            if (t.Name.ToLower().Equals("string"))
            {
                b2 = false;
            }
            if (b2)
            {
                throw new Exception("使用集合类型不正确，只能使用int,string类型（ForParallelGetIDStr）");
            }
            if (source.Count > autoIndexNum)
            {
                int _arrayCount = source.Count / autoRowNum + ((source.Count % autoRowNum) > 0 ? 1 : 0);
                UFunction.ForParallelWaiteResult(0, _arrayCount, (_i) =>
                {
                    Logger.Info("开始(ForParallelGetIDStr)" + _i.ToString() + ":" + DateTime.Now.ToString());
                    int _pageNum = _i * autoRowNum;
                    var _list = source.Skip(_pageNum).Take(autoRowNum).ToList();
                    string drugNoListStr = UFunction.ToJoin(",", _list, b);
                    method(drugNoListStr, _i);
                    Logger.Info("结束(ForParallelGetIDStr)" + _i.ToString() + ":" + DateTime.Now.ToString());
                });
            }
            else
            {
                string drugNoListStr = UFunction.ToJoin(",", source, b);
                method(drugNoListStr, 0);
            }
        }
        public static T GetPropertyValue<T>(object obj, string name)
        {
            object drv1 = GetPropertyValue(obj, name);
            T t = (T)drv1;
            return t;
        }
        public static object GetPropertyValue(object obj, string name)
        {
            var p = obj.GetType().GetProperty(name);
            if (p != null)
            {
                object drv1 = p.GetValue(obj, null);
                return drv1;
            }
            else
            {
                return null;
            }
        }
        public static object GetPropertyValueAdvanced(object obj, string name)
        {
            if (name.Contains("."))
            {
               var pName = name.Split('.').First();
               var p = obj.GetType().GetProperty(pName);
               if (p != null)
               {
                   object temp = p.GetValue(obj, null);
                   return GetPropertyValueAdvanced(temp, name.Replace(pName + ".", ""));
               }
               else
               {
                   return null;
               }
            }
            else
            {
                var p = obj.GetType().GetProperty(name);
                if (p != null)
                {
                    object drv1 = p.GetValue(obj, null);
                    return drv1;
                }
                else
                {
                    return null;
                }
            }
        }
        public static object GetExistPropertyValue(object obj, string name)
        {
            PropertyInfo property = obj.GetType().GetProperty(name);
            if (property != null)
            {
                object drv1 = property.GetValue(obj, null);
                return drv1;
            }
            else
            {
                return null;
            }

        }

        #region GetSql
        #region 获取字段名
        //public static string GetSQLField(string operationSQL, string columnName)
        //{
        //    try
        //    {
        //        operationSQL = operationSQL.ToUpper();
        //        columnName = columnName.ToUpper();
        //        string resultsql = "";
        //        operationSQL = operationSQL.Substring(0, operationSQL.IndexOf("FROM"));
        //        operationSQL = operationSQL.Replace("\r\n", " ").ToUpper().Trim();
        //        string[] strList = null;
        //        string sKey = operationSQL.Substring(0, 6);
        //        switch (sKey)
        //        {
        //            case "SELECT":
        //                operationSQL = operationSQL.Substring(6, operationSQL.Length - 6);
        //                Regex rg = new Regex("(?<=(" + @"DECIMAL\(" + "))[.\\s\\S]*?(?=(" + @"\)" + "))", RegexOptions.Multiline | RegexOptions.Singleline);
        //                MatchCollection match = rg.Matches(operationSQL);
        //                foreach (Match mt in match)
        //                {
        //                    operationSQL = operationSQL.Replace("DECIMAL(" + mt.Value + ")", "DECIMAL$" + mt.Value.Replace(",", "#") + "@");
        //                }
        //                string[] strListColumn = operationSQL.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
        //                foreach (var item in strListColumn)
        //                {
        //                    if (item.Contains("CAST("))
        //                    {
        //                        strList = item.Replace("RTRIM(", "").Replace(" ", "").Split(new string[3] { "(", "AS", ")" }, StringSplitOptions.RemoveEmptyEntries);
        //                        if (strList.Length >= 4)
        //                        {
        //                            if (strList[strList.Length - 1].Contains(columnName.ToUpper()))
        //                            {
        //                                resultsql = item.Replace(strList[strList.Length - 1], "");
        //                                if (item.Contains("DECIMAL"))
        //                                {
        //                                    resultsql = resultsql.Replace("#", ",").Replace("$", "(").Replace("@", ")");
        //                                }
        //                                return resultsql;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        strList = item.Split(new string[2] { " ", " AS " }, StringSplitOptions.RemoveEmptyEntries);
        //                        if (item.Contains("TOP"))
        //                        {
        //                            if (strList.Length == 4)
        //                            {
        //                                if (strList[strList.Length - 1].Contains(columnName.ToUpper()))
        //                                {
        //                                    resultsql = strList[strList.Length - 2];
        //                                    return resultsql;
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (strList.Length == 2)
        //                            {
        //                                if (strList[strList.Length - 1].Contains(columnName.ToUpper()))
        //                                {
        //                                    resultsql = strList[strList.Length - 2];
        //                                    return resultsql;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                break;
        //        }
        //        return resultsql;
        //    }
        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
        //}
        //public static string GetSQLFieldMoreTable(string operationSQL, string columnName)
        //{
        //    try
        //    {
        //        operationSQL = operationSQL.ToUpper();
        //        columnName = columnName.ToUpper();
        //        string resultsql = "";
        //        operationSQL = operationSQL.Substring(0, operationSQL.LastIndexOf("FROM"));
        //        operationSQL = operationSQL.Replace("\r\n", " ").ToUpper().Trim();
        //        string[] strList = null;
        //        string sKey = operationSQL.Substring(0, 6);
        //        switch (sKey)
        //        {
        //            case "SELECT":
        //                operationSQL = operationSQL.Substring(6, operationSQL.Length - 6);
        //                Regex rg = new Regex("(?<=(" + @"DECIMAL\(" + "))[.\\s\\S]*?(?=(" + @"\)" + "))", RegexOptions.Multiline | RegexOptions.Singleline);
        //                MatchCollection match = rg.Matches(operationSQL);
        //                foreach (Match mt in match)
        //                {
        //                    operationSQL = operationSQL.Replace("DECIMAL(" + mt.Value + ")", "DECIMAL$" + mt.Value.Replace(",", "#") + "@");
        //                }
        //                string[] strListColumn = operationSQL.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
        //                foreach (var item in strListColumn)
        //                {
        //                    if (item.Contains("CAST("))
        //                    {
        //                        strList = item.Replace("RTRIM(", "").Replace(" ", "").Split(new string[3] { "(", "AS", ")" }, StringSplitOptions.RemoveEmptyEntries);
        //                        if (strList.Length >= 4)
        //                        {
        //                            if (strList[strList.Length - 1].Contains(columnName.ToUpper()))
        //                            {
        //                                resultsql = item.Replace(strList[strList.Length - 1], "");
        //                                if (item.Contains("DECIMAL"))
        //                                {
        //                                    resultsql = resultsql.Replace("#", ",").Replace("$", "(").Replace("@", ")");
        //                                }

        //                                return resultsql;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        strList = item.Split(new string[2] { " ", " AS " }, StringSplitOptions.RemoveEmptyEntries);
        //                        if (item.Contains("TOP"))
        //                        {
        //                            if (strList.Length == 4)
        //                            {
        //                                if (strList[strList.Length - 1].Contains(columnName.ToUpper()))
        //                                {
        //                                    resultsql = strList[strList.Length - 2];
        //                                    return resultsql;
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (strList.Length == 2)
        //                            {
        //                                if (strList[strList.Length - 1].Contains(columnName.ToUpper()))
        //                                {
        //                                    resultsql = strList[strList.Length - 2];
        //                                    return resultsql;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                break;
        //        }
        //        return resultsql;
        //    }
        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
        //}


        public static string GetSQLField(string operationSQL, string columnName)
        {
            try
            {
                operationSQL = operationSQL.Replace("\r\n", " ").ToUpper().Trim();
                columnName = columnName.ToUpper();
                string resultsql = "";

                Regex rgRegex = new Regex("(?<=(" + @"SELECT" + "))[.\\s\\S]*?(?=(" + columnName + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                //MatchCollection matchRegex = rgRegex.Matches(operationSQL);
                Match matchRegex = rgRegex.Match(operationSQL);
                if (string.IsNullOrWhiteSpace(matchRegex.Value))
                {
                    return "";
                }
                operationSQL = matchRegex.Value.ToString() + columnName;
                //foreach (Match mt in matchRegex)
                //{

                //}
                //  operationSQL = operationSQL.Substring(0, operationSQL.LastIndexOf("FROM"));

                string[] strList = null;

                Regex rg = new Regex("(?<=(" + @"DECIMAL\(" + "))[.\\s\\S]*?(?=(" + @"\)" + "))", RegexOptions.Multiline | RegexOptions.Singleline);
                MatchCollection match = rg.Matches(operationSQL);
                foreach (Match mt in match)
                {
                    operationSQL = operationSQL.Replace("DECIMAL(" + mt.Value + ")", "DECIMAL$" + mt.Value.Replace(",", "#") + "@");
                }
                string[] strListColumn = operationSQL.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in strListColumn)
                {
                    if (item.Contains("CAST("))
                    {
                        strList = item.Replace("RTRIM(", "").Replace(" ", "").Split(new string[3] { "(", " AS ", ")" }, StringSplitOptions.RemoveEmptyEntries);
                        if (strList.Length >= 3)
                        {
                            if (strList[strList.Length - 1].Contains(columnName.ToUpper()))
                            {
                                resultsql = item.Replace(strList[strList.Length - 1], "");
                                if (item.Contains("DECIMAL"))
                                {
                                    resultsql = resultsql.Replace("#", ",").Replace("$", "(").Replace("@", ")");
                                }

                                return resultsql;
                            }
                        }
                    }
                    else
                    {
                        if (item.Contains("CASE"))
                        {
                            //   " (CASE CSSPBMZD.XSSFCZBZ WHEN 0 THEN 0 ELSE 1 END) AS ISUNPACKING"
                            strList = item.Trim().Split(new string[8] { "(", "CASE", "WHEN", "THEN", "ELSE", "END", ")", "AS" }, StringSplitOptions.RemoveEmptyEntries);
                            if (strList.Length > 0)
                            {
                                if (strList[strList.Length - 1].Contains(columnName.ToUpper()))
                                {
                                    resultsql = strList[0];
                                    return resultsql;
                                }
                            }
                         
                        }

                        strList = item.Split(new string[2] { " ", " AS " }, StringSplitOptions.RemoveEmptyEntries);
                        if (item.Contains("TOP"))
                        {
                            if (strList.Length == 4)
                            {
                                if (strList[strList.Length - 1].Contains(columnName.ToUpper()))
                                {
                                    resultsql = strList[strList.Length - 2];
                                    return resultsql;
                                }
                            }
                        } 
                        else
                        {
                            if (strList.Length == 2)
                            {
                                if (strList[strList.Length - 1].Contains(columnName.ToUpper()))
                                {
                                    resultsql = strList[strList.Length - 2];
                                    return resultsql;
                                }
                            }
                        }
                    }
                }

                return resultsql;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion
        #region 获取表名

        public static string GetTable(string operationSQL)
        {
            try
            {
                operationSQL = operationSQL.ToUpper();
                int start = operationSQL.IndexOf("FROM");
                int end = operationSQL.IndexOf("WHERE", start);
                if (end > 0)
                {
                    return operationSQL.Substring(start, end - start).Replace("\r\n", " ");
                }
                else
                {
                    return operationSQL.Substring(start, operationSQL.Length - start).Replace("\r\n", " ");
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string MoreGetTable(string operationSQL)
        {
            try
            {
                operationSQL = operationSQL.ToUpper();
                int start = operationSQL.LastIndexOf("FROM");
                int end = operationSQL.IndexOf("WHERE", start);
                if (end > 0)
                {
                    return operationSQL.Substring(start, end - start).Replace("\r\n", " ");
                }
                else
                {
                    return operationSQL.Substring(start, operationSQL.Length - start).Replace("\r\n", " ");
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        #endregion
        #region 拼接Erp Sql


        /// <summary>
        /// 获取erp查询sql
        /// </summary>
        /// <param name="operationSQL">erp数据库可执行sql</param>
        /// <param name="column">指定查询字段</param>
        /// <param name="useErpColumn">是否使用ERP字段</param>
        /// <returns></returns>
        public static string GetErpSql(string operationSQL, bool useErpColumn, params string[] column)
        {
            try
            {
                string table = UFunction.GetTable(operationSQL);
                List<string> excuteColumnList = new List<string>();
                operationSQL = operationSQL.ToUpper();
                foreach (var item in column)
                {
                    string columnName = item.ToUpper();
                    string temp = UFunction.GetSQLField(operationSQL, columnName);
                    if (!string.IsNullOrWhiteSpace(temp))
                    {
                        string addstr = temp;
                        if (!useErpColumn)
                        {
                            addstr = temp + " " + item;
                        }
                        excuteColumnList.Add(addstr);
                    }
                }

                if (excuteColumnList.Count() > 0)
                {
                    string excuteSql = "SELECT " + string.Join(",", excuteColumnList) + " " + table;

                    return excuteSql;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 查询Sql 存在多个表关联  取 最后一个
        /// </summary>
        /// <param name="operationSQL"></param>
        /// <param name="useErpColumn"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string GetErpSqlMoreTable(string operationSQL, bool useErpColumn, params string[] column)
        {
            try
            {
                string table = UFunction.MoreGetTable(operationSQL);
                List<string> excuteColumnList = new List<string>();
                operationSQL = operationSQL.ToUpper();
                foreach (var item in column)
                {
                    string columnName = item.ToUpper();
                    string temp = UFunction.GetSQLField(operationSQL, columnName);
                    if (!string.IsNullOrWhiteSpace(temp))
                    {
                        string addstr = temp;
                        if (!useErpColumn)
                        {
                            addstr = temp + " " + item;
                        }
                        excuteColumnList.Add(addstr);
                    }
                }

                if (excuteColumnList.Count() > 0)
                {
                    string excuteSql = "SELECT " + string.Join(",", excuteColumnList) + " " + table;

                    return excuteSql;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string GetErpSqlMoreTable(string operationSQL, bool useErpColumn, List<MacthColumn> column)
        {
            try
            {
                string table = UFunction.MoreGetTable(operationSQL);
                List<string> excuteColumnList = new List<string>();
                operationSQL = operationSQL.ToUpper();
                foreach (MacthColumn item in column)
                {
                    string columnName = item.ColumnName.ToUpper();
                    string temp = UFunction.GetSQLField(operationSQL, columnName);
                    if (!string.IsNullOrWhiteSpace(temp))
                    {
                        string addstr = temp;
                        if (!useErpColumn)
                        {
                            addstr = temp + " " + columnName;
                        }
                        excuteColumnList.Add(addstr);
                    }
                }

                if (excuteColumnList.Count() > 0)
                {
                    string excuteSql = "SELECT " + string.Join(",", excuteColumnList) + " " + table;

                    return excuteSql;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion
        #endregion

        public static T ClearWhiteSpace<T>(T source, T target)
        {
            List<System.Reflection.PropertyInfo> p1 = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).ToList();
            List<System.Reflection.PropertyInfo> p2 = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).ToList();
            foreach (var item in p1)
            {
                var v = p2.Where(q => q.Name == item.Name && q.PropertyType == item.PropertyType).ToList();
                object[] objAttrs = item.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), true);
                if (v.Count > 0 && v[0].GetValue(target, null) != item.GetValue(source, null) && objAttrs.Length < 1)
                {
                    var value = v[0].GetValue(target, null);
                    if (value != null && item.PropertyType.ToString().ToLower() == "system.string")
                    {
                        value = value.ToString().Trim();
                    }
                    item.SetValue(source, value, null);
                }
            }
            return target;
        }

        public static T ClearWhiteSpace<T>(T source)
        {
            List<System.Reflection.PropertyInfo> p1 = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).ToList();

            foreach (var item in p1)
            {

                object[] objAttrs = item.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), true);
                if (objAttrs.Length < 1)
                {
                    var value = item.GetValue(source, null);
                    if (value != null && item.PropertyType.ToString().ToLower() == "system.string")
                    {
                        value = value.ToString().Trim();
                    }
                    item.SetValue(source, value, null);
                }
            }
            return source;
        }

        public static bool? IsWhereContainColumn(string operationSQL, string operationColumn)
        {
            try
            {
                operationSQL = operationSQL.ToUpper();
                int start = operationSQL.IndexOf("WHERE");
                if (start > 0)
                {
                    int end = operationSQL.Length;
                    return operationSQL.Substring(start, end - start).Contains(operationColumn);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 判断 输入字符串  是不是 纯英文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckString(string str)
        {
            Match match = Regex.Match(str, "^[A-Za-z]+$");
            if (match.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool SymbolCalculate(string text, decimal? value)
        {
            bool result = false;
            if (text.Contains("&"))
            {
                var list = text.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var item in list)
                {
                    result = BoolCalculate(item.Trim(), value);
                    if (!result)
                    {
                        return result;
                    }
                }
            }
            else
            {
                result = BoolCalculate(text.Trim(), value);
            }
            return result;
        }
        public static bool BoolCalculate(string text, decimal? value)
        {
            decimal result = -1;
            #region 大于等于
            if (text.Contains(@">="))
            {
                string dec = text.Replace(@">=", "").Trim();
                if (dec != "")
                {
                    result = UFunction.ToDecimal(dec, -999999);
                }

                if (result == -999999)
                {
                    return false;
                }
                else
                {
                    if (value >= result)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            #endregion
            #region 大于
            if (text.Contains(@">"))
            {
                string dec = text.Replace(@">", "").Trim();
                if (dec != "")
                {
                    result = UFunction.ToDecimal(dec, -999999);
                }

                if (result == -999999)
                {
                    return false;
                }
                else
                {
                    if (value > result)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            #endregion
            #region 小于等于
            else if (text.Contains(@"<="))
            {
                string dec = text.Replace(@"<=", "").Trim();
                if (dec != "")
                {
                    result = UFunction.ToDecimal(dec, -999999);
                }
                if (result == -999999)
                {
                    return false;
                }
                else
                {
                    if (value <= result)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            #endregion
            #region 小于
            else if (text.Contains(@"<"))
            {
                string dec = text.Replace(@"<", "").Trim();
                if (dec != "")
                {
                    result = UFunction.ToDecimal(dec, -999999);
                }
                if (result == -999999)
                {
                    return false;
                }
                else
                {
                    if (value < result)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            #endregion

            #region 等于
            else if (text.Contains(@"="))
            {
                string dec = text.Replace(@"=", "").Trim();
                if (dec != "")
                {
                    result = UFunction.ToDecimal(dec, -999999);
                }
                if (result == -999999)
                {
                    return false;
                }
                else
                {
                    if (value == result)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            #endregion
            #region 其他
            else
            {

                if (!string.IsNullOrWhiteSpace(text))
                {
                    result = UFunction.ToDecimal(text.Trim(), -999999);
                }
                if (result == -999999)
                {
                    return false;
                }
                else
                {
                    if (value == result)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            #endregion
        }


        public static string Trim(string p)
        {
            return p == null ? p : p.Trim();
        }

        public static List<int> GetPlanId(string planIdStr, int planId)
        {
            List<int> list = new List<int>();
            if (string.IsNullOrWhiteSpace(planIdStr))
            {
                list.Add(planId);
            }
            else
            {
                var strList = planIdStr.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (strList != null && strList.Count > 0)
                {
                    foreach (var str in strList)
                    {
                        int i = UFunction.ToInt(str, planId);
                        list.Add(i);
                    }
                }
                else
                {
                    list.Add(planId);
                }
            }
            return list.Distinct().ToList();
        }

        public static T GetMultiValue<T>(string value, string separator = "*") where T : struct
        {
            try
            {
                dynamic result = 1;
                value = value.Trim(separator[0]);
                if (value.Contains(separator))
                {
                    string[] arr = value.Split(separator[0]);
                    foreach (string str in arr)
                    {
                        T temp = (T)Convert.ChangeType(Regex.Match(str, @"\d+\.?\d*").ToString(), typeof(T));
                        result = result * temp;
                    }
                }
                else
                {
                    result = Regex.Match(value, @"\d+\.?\d*").ToString();
                }
                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 得到数组列表以逗号分隔的字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetArrayStr(List<int> list)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i].ToString());
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }

        public static string GetArrayStr(List<string> list, string speater = ",")
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }

        

        /// <summary>
        /// 获取字符串中去掉小括号里的内容
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetContentMinusParentheses(string value)
        {
            return Regex.Replace(value.Replace("（", "(").Replace("）", ")"), @"\([^\(]*\)", "");
        }
    }
}
