using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using Microsoft.Win32;
using System.Xml.Serialization;
using System.IO;
using System.Net; 

namespace Utility
{

    public class HttpOP
    {
        public static void downLoadFile(string url, string downloadFileName)
        {
            //判断要下载的文件夹是否存在，如果存在删除文件
            if (File.Exists(downloadFileName))
            {
                File.Delete(downloadFileName);
            }
            //实例化流对象
            FileStream FStream = null;
            FStream = new FileStream(downloadFileName, FileMode.Create);
            Stream myStream = null;
            try
            {
                //打开网络连接
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                var request=(HttpWebResponse)myRequest.GetResponse();
                if (request.StatusCode == HttpStatusCode.OK)
                {
                    //向服务器请求,获得服务器的回应数据流
                    myStream = request.GetResponseStream();
                    //定义一个字节数据
                    byte[] btContent = new byte[512];
                    int intSize = 0;
                    intSize = myStream.Read(btContent, 0, 512);
                    while (intSize > 0)
                    {
                        FStream.Write(btContent, 0, intSize);
                        intSize = myStream.Read(btContent, 0, 512);
                    }
                    //关闭流
                    FStream.Close();
                    myStream.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                if (myStream != null)
                    myStream.Close();
                if (FStream != null)
                    FStream.Close();
                throw ex;
            }
            finally
            {
                FStream.Dispose();
                myStream.Dispose();
            }

        }

        public static string openNetSqlFile(string url)
        {
            Stream myStream = null;
            string str = "";
            try
            {
                //打开网络连接
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                //向服务器请求,获得服务器的回应数据流
                myStream = myRequest.GetResponse().GetResponseStream();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(myStream, Encoding.Default))
                {
                    str = reader.ReadToEnd();
                }
                //关闭流
                myStream.Close();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                if (myStream != null)
                    myStream.Close();
                throw ex;
            }
            finally
            {
                myStream.Dispose();
            }
            return str;
        }
    }

}