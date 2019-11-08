using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio;
using NAudio.CoreAudioApi;

namespace Utility
{
    public class Base64Helper
    {
        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="code"></param>
        /// <param name="code_type"></param> 
        /// <returns></returns>
        public static string EncodeBase64(byte[] code, string code_type = "utf-8")
        {
            string encode = "";
            byte[] bytes = code;
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = "";
            }
            return encode;
        }
        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="code"></param>
        /// <param name="code_type"></param> 
        /// <returns></returns>
        public static string EncodeBase64(string code, string code_type = "utf-8")
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }
        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="code"></param>
        /// <param name="code_type"></param> 
        /// <returns></returns>
        public static string DecodeBase64(string code, string code_type = "utf-8")
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.GetEncoding(code_type).GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }
        
    } 
}
