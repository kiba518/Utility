using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net; 
using System.Globalization; 

namespace Utility
{

    public class FtpOP
    {  

        public FtpOP()
        {   
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="filename"></param>
        public void Upload(string source,string destn)
        {
            FileInfo fileInf = new FileInfo(source);
            
            FtpWebRequest reqFTP; 
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(destn)); 
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;  

            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            Stream strm = null;
            try
            {
                strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
            }
            catch (Exception ex)
            {
                
                Logger.Error(ex);
            }
            finally
            { 
                if (strm != null)
                    strm.Close();
                if (fs != null)
                    fs.Close();
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="filePath">保存文件的地址</param>
        /// <param name="fileName">保存文件的名称</param>
        public void Download(string filePath, string fileName, string ftpURI)
        {
            //ftpURI = "ftp://ftpuser:Neusoft123@10.10.55.45/1.0.0.zip";
            FtpWebRequest reqFTP;
            FileStream outputStream = null;
            FtpWebResponse response = null;
            Stream ftpStream = null;
            try
            {
                outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;

                response = (FtpWebResponse)reqFTP.GetResponse();
                ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                return;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
            finally
            {
                if (ftpStream != null)
                    ftpStream.Close();
                if (outputStream != null)
                    outputStream.Close();
                if (response != null)
                    response.Close();
            }
        }

      

    }

}