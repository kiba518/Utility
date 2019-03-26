using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.Tool
{
    public class RegisterHelper
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
    }
}
