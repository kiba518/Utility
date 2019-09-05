using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{ 
    // Registry.LocalMachine.CreateSubKey("SOFTWARE\\XXX");
    public class RegistryHelper
    {
        public static int AutoRunApplication(string fileName, bool isAutoRun)
        {
            int resset = 0;
            RegistryKey reg = null;
            try
            {
                if (isAutoRun && !System.IO.File.Exists(fileName))//如果是启动 检测软件是否存在
                {
                    resset = -1;
                    //OliveLogger.Info("软件地址不存在：" + fileName);
                    return resset;
                }
                String name = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
                reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (reg == null)
                {
                    reg = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    if (reg == null)
                    {
                        // OliveLogger.Info("设置自启动软件失败：" + fileName);
                        return -1;
                    }
                }
                System.Threading.Thread.Sleep(1000);
                if (isAutoRun)
                {
                    reg.SetValue(name, fileName);
                }
                else
                {
                    reg.DeleteValue(name, false);
                }
                System.Threading.Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                if (reg != null)
                {
                    reg.Close();
                }
            }
            //OliveLogger.Info("设置自启动软件成功：" + fileName);

            return resset;
        }
        /// <summary>
        /// 读取指定名称的注册表的值 string portName = RegistryHelper.GetRegistryData(Registry.LocalMachine, "SOFTWARE\\TagReceiver\\Params\\SerialPort", "PortName");
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetValue(RegistryKey root, string subkey, string name)
        {
            string registData = "";
            RegistryKey myKey = root.OpenSubKey(subkey, true);
            if (myKey != null)
            {
                registData = myKey.GetValue(name).ToString();
            }

            return registData;
        }

        /// <summary>
        /// 向注册表中写数据 RegistryHelper.SetRegistryData(Registry.LocalMachine, "SOFTWARE\\TagReceiver\\Params\\SerialPort", "PortName", portName);
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tovalue"></param> 
        public static void SetValue(RegistryKey root, string subkey, string name, string value)
        {
            RegistryKey aimdir = root.CreateSubKey(subkey);
            aimdir.SetValue(name, value);
        }

        /// <summary>
        /// 删除注册表中指定的注册表项
        /// </summary>
        /// <param name="name"></param>
        public static void Delete(RegistryKey root, string subkey, string name)
        {
            string[] subkeyNames;
            RegistryKey myKey = root.OpenSubKey(subkey, true);
            subkeyNames = myKey.GetSubKeyNames();
            foreach (string aimKey in subkeyNames)
            {
                if (aimKey == name)
                    myKey.DeleteSubKeyTree(name);
            }
        }

        /// <summary>
        /// 判断指定注册表项是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsExist(RegistryKey root, string subkey, string name)
        {
            bool _exit = false;
            string[] valueNames;
            RegistryKey myKey = root.OpenSubKey(subkey, true);
            if (myKey != null)
            {
                valueNames = myKey.GetValueNames();
                foreach (string keyName in valueNames)
                {
                    if (keyName == name)
                    {
                        _exit = true;
                        return _exit;
                    }
                }
            }
            return _exit;
        }
    }
}
