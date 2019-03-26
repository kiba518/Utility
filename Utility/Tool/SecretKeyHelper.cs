using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Utility 
{
    public class SecretKeyHelper
    {
        public static string GetSecretKey()
        {
            string os = Environment.OSVersion.VersionString;
            string username = Environment.UserName;
            IPAddress[] ipIPAddress = Dns.GetHostAddresses(Dns.GetHostName().ToString());
            string ip = "";
            foreach (IPAddress ipa in ipIPAddress)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    ip += ipa.ToString();
            }


            string s = "", mac = "";
            string hostInfo = Dns.GetHostName();
            System.Net.IPAddress[] addressList = Dns.GetHostByName(Dns.GetHostName()).AddressList;
            for (int i = 0; i < addressList.Length; i++)
            {
                s += addressList[i].ToString();
            }
            ManagementClass mc;
            mc = new ManagementClass("Win32_NetworkAdapterConfiguration"); ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["IPEnabled"].ToString() == "True")
                    mac = mo["MacAddress"].ToString();

            }
            byte[] result = Encoding.Default.GetBytes(mac + os);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);

            return BitConverter.ToString(output).Replace("-", "");
        }
    }
}
