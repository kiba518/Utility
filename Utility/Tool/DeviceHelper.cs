using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class DeviceHelper
    {

        public static List<string> GetCameraList()
        {
            List<string> result = new List<string>();
            result.Add(@"none");

            string sql = @"Select * From Win32_PnPEntity WHERE DEVICEID like 'USB\\VID%' and " +
                "ConfigManagerErrorCode = 0 and (Name like '%Camera%' or Name like '%Video%' or ( Name like '%Webcam%'and Service like '%video%') )";

            ManagementObjectCollection moCol;
            using (var searcher = new ManagementObjectSearcher(sql))
            {
                moCol = searcher.Get();
                foreach (ManagementObject mo in moCol)
                {
                    foreach (PropertyData pd in mo.Properties)
                    {
                        if (pd.Name == "Name" && pd.Value != null)
                        {
                            result.Add((string)pd.Value);
                        }
                    }
                }
            }
            return result;
        } // EOF<GetCameraList()>

        public static List<string> GetMicrophoneList()
        {
            List<string> result = new List<string>();
            result.Add(@"none");

            string sql = @"Select * From Win32_PnPEntity WHERE DEVICEID like 'SWD\\MMD%' and " +
                "ConfigManagerErrorCode = 0 and (name like '%Microphone%' or ( Name like '%Webcam%') )";

            ManagementObjectCollection moCol;
            using (var searcher = new ManagementObjectSearcher(sql))
            {
                moCol = searcher.Get();
                foreach (ManagementObject mo in moCol)
                {
                    foreach (PropertyData pd in mo.Properties)
                    {
                        if (pd.Name == "Name" && pd.Value != null)
                        {
                            result.Add((string)pd.Value);
                        }
                    }
                }
            }
            return result;
        }  

    } 
}
