using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Utility 
{
    /*
    调用
            //string serviceFilePath = Environment.CurrentDirectory + @"\Plus\WS\Pur_WinService.exe";
            //string serviceFilePath = @"E:\采购平台\Code\采购平台\PurchasePlatform\Pur_WinService\bin\Debug\Pur_WinService.exe";
            //string serviceName = "Pur_LoopService";
            //if (!WindowsServiceHelper.IsServiceExisted(serviceName))
            //{
            //    WindowsServiceHelper.UninstallService(serviceFilePath);
            //    WindowsServiceHelper.InstallService(serviceFilePath);
            //    WindowsServiceHelper.ServiceStart(serviceName);
            //}
    */

    public class WindowsServiceHelper
    {
        public static string InstallbatFilePath = System.Windows.Forms.Application.StartupPath + @"\bat\Install.bat";
        public static string StartbatFilePath = System.Windows.Forms.Application.StartupPath + @"\bat\Start.bat";
        public static string UninstallbatFilePath = System.Windows.Forms.Application.StartupPath + @"\bat\Uninstall.bat";
        public static string KillbatFilePath = System.Windows.Forms.Application.StartupPath + @"\bat\kill.bat";
 
        public static string AdminBatString = "";

//@"@echo off
//
//>nul 2>&1 ""%SYSTEMROOT%\system32\cacls.exe"" ""%SYSTEMROOT%\system32\config\system""
//
//if '%errorlevel%' NEQ '0' (
//
//echo 请求管理员权限...
//
//goto UACPrompt
//
//) else ( goto gotAdmin )
//
//:UACPrompt
//
//echo Set UAC = CreateObject^(""Shell.Application""^) > ""%temp%\getadmin.vbs""
//
//echo UAC.ShellExecute ""%~s0"", """", """", ""runas"", 1 >> ""%temp%\getadmin.vbs""
//
//""%temp%\getadmin.vbs""
//
//exit /B
//
//:gotAdmin
//
//";

        //判断服务是否存在
        public static bool IsServiceExisted(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController sc in services)
            {
                if (sc.ServiceName.ToLower() == serviceName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
        //安装服务
        public static void InstallService(string serviceFilePath)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = serviceFilePath;
                IDictionary savedState = new Hashtable();
                installer.Install(savedState);
                installer.Commit(savedState);
            }
        }

        //卸载服务
        public static void UninstallService(string serviceFilePath)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = serviceFilePath;
                installer.Uninstall(null);
            }
        }
        //启动服务
        public static void ServiceStart(string serviceName)
        {
            try
            {
                using (ServiceController control = new ServiceController(serviceName))
                {
                    if (control.Status == ServiceControllerStatus.Stopped)
                    {
                        control.Start();
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        //停止服务
        public static void ServiceStop(string serviceName)
        {
            using (ServiceController control = new ServiceController(serviceName))
            {
                if (control.Status == ServiceControllerStatus.Running)
                {
                    control.Stop();
                }
            }
        }
        public static bool IsStart(string serviceName)
        {
            bool result = false;
            using (ServiceController control = new ServiceController(serviceName))
            {
                if (control.Status == ServiceControllerStatus.Running)
                {
                    result = true;
                }
            }
            return result;
        }

        public static string Installbat(string serviceName, string serverPath=null)
        {
            try
            {
                string localPath = System.Windows.Forms.Application.StartupPath;
               
                if (serverPath != null)
                {   
                    string zipDirPath = localPath + @"\Plus\WinServiceTemp";//待解压的文件夹
                    Logger.Debug("待解压的文件夹zipDirPath:" + zipDirPath);
                    string zipFilePath = zipDirPath + @"\service.zip"; //待解压的文件
                    Logger.Debug("待解压的文件zipDirPath:" + zipDirPath);
                    FileHelper.CreateDirectory(zipDirPath);
                    HttpOP.downLoadFile(serverPath, zipFilePath);
                 
                    string zipedDir = localPath + @"\Plus\WinService";//解压的指定目录
                    Logger.Debug("清空解压的指定目录");
                    FileHelper.ClearDirectory(zipedDir); 
                    Logger.Debug("重新创建解压的指定目录");
                    FileHelper.CreateDirectory(zipedDir);
                    UnZipClass.UnZip(zipFilePath, zipedDir, "");
                    Logger.Debug("解压缩文件夹成功");
                   
                }
                else
                {
                    Logger.Debug("serverPath is null");
                }
                FileHelper.CopyFolder(localPath + @"\Plus\WinService", @"c:\cache\Plus");
                 
                string bat = @"%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe  c:\cache\Plus\WinService\Pur_WinService.exe
Net Start Pur_LoopService
sc config Pur_LoopService start= auto";
                FileHelper.CreateDirectory(localPath + @"\Bat");
               
                FileHelper.Write_gb2313(InstallbatFilePath,AdminBatString+ bat);

                return InstallbatFilePath;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }
        public static string Startbat(string serviceName)
        {
            try
            {
                string bat = @"Net Start Pur_LoopService
sc config Pur_LoopService start= auto";
                FileHelper.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\Bat");
                FileHelper.Write_gb2313(StartbatFilePath, AdminBatString+bat);

                return StartbatFilePath;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }
        public static string Uninstallbat(string serviceName)
        {
            try
            {
                string bat = @"%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe /u c:\cache\Plus\WinService\Pur_WinService.exe";
                FileHelper.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\Bat");
                FileHelper.Write_gb2313(UninstallbatFilePath, AdminBatString+bat);

                return UninstallbatFilePath;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }

        public static string KillProcess(string monitor_ProcessName)
        {
            string bat = @"taskkill /f /t /im " + monitor_ProcessName;
            FileHelper.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\Bat");
            FileHelper.Write_gb2313(KillbatFilePath, AdminBatString+bat);
            return KillbatFilePath;
            //OliveLogger.LogStart();
            //try
            //{
            //    Process[] ps = Process.GetProcesses();
            //    string processNameStrList = string.Join(",", (ps.Select(p => p.ProcessName).ToArray()));
            //    OliveLogger.Debug("系统当前运行全部进程：" + processNameStrList);
            //    foreach (Process item in ps)
            //    {
            //        string processName = item.ProcessName.ToLower();
            //        monitor_ProcessName = monitor_ProcessName.ToLower();
            //        if (monitor_ProcessName.Contains(processName))
            //        {
            //            OliveLogger.Info("发现当前运行的进程" + monitor_ProcessName);

            //            OliveLogger.Info("执行Kill进程" + monitor_ProcessName);
            //            item.CloseMainWindow();
            //            item.Close();
            //            item.Dispose(); 
            //            item.Kill();
            //            break;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    OliveLogger.Error(ex);
            //}
            //OliveLogger.LogEnd();
        }
       
    }
}
