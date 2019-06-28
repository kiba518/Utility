using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Utility.Tool
{
    public class ByteHelper
    {
        /// <summary>
        /// 将文件转换成byte[]数组
        /// </summary>
        /// <param name="fileUrl">文件路径文件名称</param>
        /// <returns>byte[]数组</returns>
        public static byte[] FileToByte(string fileUrl)
        {
            try
            {
                using (FileStream fs = new FileStream(fileUrl, FileMode.Open, FileAccess.Read))
                {
                    byte[] byteArray = new byte[fs.Length];
                    fs.Read(byteArray, 0, byteArray.Length);
                    return byteArray;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 将byte[]数组保存成文件
        /// </summary>
        /// <param name="byteArray">byte[]数组</param>
        /// <param name="fileName">保存至硬盘的文件路径</param>
        /// <returns></returns>
        public static bool ByteToFile(byte[] byteArray, string fileName)
        {
            bool result = false;
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static byte[] IntToByte(int source)
        {
            byte[] intBuff = BitConverter.GetBytes(source);
            return intBuff;
        }
        /// <summary>
        /// 使用方法 ByteHelper.ByteToInt(pByte.Skip(0).Take(4).ToArray());
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int ByteToInt32(byte[] source)
        {
            int intBuff = BitConverter.ToInt32(source, 0);
            return intBuff;
        }

        public static short ByteToInt16(byte[] source)
        {
            short intBuff = BitConverter.ToInt16(source, 0);
            return intBuff;
        }
        public static bool ByteToBool(byte[] source)
        {
            bool booBuff = BitConverter.ToBoolean(source, 0);
            return booBuff;
        }
        public static char ByteToChar(byte[] source)
        {
            char charBuff = BitConverter.ToChar(source, 0);
            return charBuff;
        }
        public static String ByteToString(byte[] source)
        {
            String strBuff = BitConverter.ToString(source, 0);
            return strBuff;
        }
    }
}
