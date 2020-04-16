using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Utility
{
    public class CMDHelper
    {
        public static bool StartAdministrator(string programPath)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = false;
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; 
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.FileName = programPath;
                startInfo.CreateNoWindow = true;
                //设置启动动作,确保以管理员身份运行
                startInfo.Verb = "runas";
                
                Process myProcess = new Process();
                myProcess.EnableRaisingEvents = false;
                var pc = Process.Start(startInfo);
                 
                return true;
            }
            catch(Exception ex)
            {
                //OliveLogger.Error(ex);
                try
                {
                    //OliveLogger.Info(programPath);
                    
                    System.Diagnostics.Process.Start(programPath);
                    return true;
                }
                catch (Exception ex2)
                {
                    //OliveLogger.Error(ex2);
                }
                return false;
            }
           
        }
        public static bool Start(string programPath)
        {
            try
            {
                Process myProcess = new Process();
                myProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = programPath;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.EnableRaisingEvents = false;
                bool boo = myProcess.Start();
                return boo;
            }
            catch
            {
                return false;
            }

        }
        public static bool Start(string programPath, string para)
        {
            try
            {
                Process myProcess = new Process();
                myProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = programPath;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.Arguments = para;
                myProcess.EnableRaisingEvents = false;
                bool boo = myProcess.Start();
                return boo;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public static bool IsAdministrator()
        {
            /**
              * 当前用户是管理员的时候，直接启动应用程序
              * 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
              */
            //获得当前登录的Windows用户标示
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            //判断当前登录用户是否为管理员
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsRunProcess(string processName)
        {
            bool result=false;
            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process process in processList)
            {
                if (process.ProcessName.ToUpper() == processName.ToUpper())
                {
                    result= true;
                    break;
                }
            }
            return result;
        }

        public static void KillProcess(string monitor_ProcessName)
        {
            Logger.LogStart();
            try
            {
                Process[] ps = Process.GetProcesses();
                string processNameStrList = string.Join(",", (ps.Select(p => p.ProcessName).ToArray()));
                Logger.Debug("系统当前运行全部进程：" + processNameStrList);
                foreach (Process item in ps)
                {
                    string processName = item.ProcessName.ToLower();
                    monitor_ProcessName = monitor_ProcessName.ToLower();
                    if (monitor_ProcessName.Contains(processName))
                    {
                        Logger.Info("发现当前运行的进程" + monitor_ProcessName);
                        if (!item.HasExited)
                        {
                            Logger.Info("执行Kill进程" + monitor_ProcessName);
                            item.Kill();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            Logger.LogEnd();
        }

        public static int AutoRunApplication(string fileName, bool isAutoRun)
        {
            Logger.LogStart();
            int resset = 0;
            RegistryKey reg = null;
            try
            {
                if (isAutoRun && !System.IO.File.Exists(fileName))//如果是启动 检测软件是否存在
                {
                    resset = -1;
                    Logger.Info("软件地址不存在：" + fileName);
                    return resset;
                }
                String name = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
                reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (reg == null)
                {
                    reg = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    if (reg == null)
                    {
                        Logger.Info("设置自启动软件失败：" + fileName);
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
                Logger.Error(ex);
                return -1;
            }
            finally
            {
                if (reg != null)
                {
                    reg.Close();
                }
            }
            Logger.Info("设置自启动软件成功：" + fileName);
            Logger.LogEnd();
            return resset;
        }
    }
}
