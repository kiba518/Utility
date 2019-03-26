 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Utility
{
    public  class FlieHelper
    {
        public FlieHelper()
        {

        }
        public static string GetAppPath()
        {
            string str = Application.StartupPath;
            return str;
        }
        public static string ReadFile(string fileName)
        {
            string text = "";
            if (!File.Exists(fileName))
            {
                return text;
            }
            FileStream FStream= new FileStream(fileName,FileMode.OpenOrCreate,FileAccess.Read);
            try
            {
                byte[] buffer = new byte[1024 * 1024 * 2];
                FStream.Read(buffer, 0, buffer.Length);
                int intSize = 0;
                intSize = FStream.Read(buffer, 0, buffer.Length);
                while (intSize > 0)
                {
                    text += Encoding.Default.GetString(buffer, 0, intSize);
                    intSize = FStream.Read(buffer, 0, buffer.Length);
                }
                
                //关闭流
                FStream.Close();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                if (FStream != null)
                    FStream.Close();
                throw ex;
            }
            finally
            {
                FStream.Dispose();
            }
            return text;
        }
        public static void SaveFile(string text, string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            FileStream FStream = null;
            FStream = new FileStream(fileName, FileMode.Create);
            try
            {
                byte[] btContent = Encoding.Default.GetBytes(text);
                FStream.Write(btContent, 0, btContent.Length);
                FStream.Close();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                if (FStream != null)
                    FStream.Close();
                throw ex;
            }
            finally
            {
                FStream.Dispose();
            }

        }
    }
}
