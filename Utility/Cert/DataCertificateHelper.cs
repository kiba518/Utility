using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Utility;
namespace Utility
{
    public  class DataCertificateHelper
    {
        //加密
        public const string publicKey = "<RSAKeyValue><Modulus>zz7IQIJC6B56EdskOinWpRXY9mAfjcoNw3VRk1Q0FpYt+uOncqman6/WjG2LusSxAz+WmQKb0HoqLLTN/YluJLRNJMCkcB9bRATrFBjWkA3QMyoqmr+QPERzCzUVoceGWys1H0odpi/v+cL6GdgbhGcUxWVcUtDXcmDMv/b6UpE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        //解密
        public const string privateKey = "<RSAKeyValue><Modulus>zz7IQIJC6B56EdskOinWpRXY9mAfjcoNw3VRk1Q0FpYt+uOncqman6/WjG2LusSxAz+WmQKb0HoqLLTN/YluJLRNJMCkcB9bRATrFBjWkA3QMyoqmr+QPERzCzUVoceGWys1H0odpi/v+cL6GdgbhGcUxWVcUtDXcmDMv/b6UpE=</Modulus><Exponent>AQAB</Exponent><P>6N8DMG8JMaQlPfiHOCeMWhuRMw4PGj1SLcLglTMYSFwx6ZYHLHb/cCg56ou4AynPEUnaTRXL8yFBpsiprmZ5mw==</P><Q>49Q0e86O4uSiqF8GfO6Ke3IyHXGrEhV4WHgfwqUz4t2q2cmpOtXQrOmhvqoipI71eDqas+aJpmACWxpPrfztQw==</Q><DP>yQxJD5a9hEsoEBGyhGu4g88LE94OceQBQBrglE9xpn9aZEWv2da/ABDqt7F649hDurRdMXIhC75plNnnjPdSAw==</DP><DQ>UY4RC5CxjX8SyvrZM7egUvhaADhEaMDOx7yYgfDpVfjLxBJwReNsQ7mOcNYueIHEVTmFT4jjFW+g6EPO6hV0SQ==</DQ><InverseQ>HKXnLS+GNcXXcP8sFPpMlBq2q5PAIyTkPyhtmnIw4ID1F4EIY6OxTkdrlZsWS/rbzCGR7qGgc1k/t/rN3BWn3g==</InverseQ><D>wwhXVfYAVwdPY3xypFX5TNS5oGqnZz0uJLJEeby1ZEgir0gMriiUfhDxfKge5j3yQ5dU91vwHIrLI9VnQWFTB4AjaGxZuJi+qtXviXpYiPt78lzKpZuLKH6VqQu5i9aSWicHQdosJDoPVA51k+3KELvG8xDzEI5CCVOC90dGHj0=</D></RSAKeyValue>";

        public static  void CopyPfxAndGetInfo()
        { 
            string keyName = "VanPeng.Desktop.Licence";
            var ret = DataCertificate.CreateCertWithPrivateKey(keyName, @"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\makecert.exe");
            if(ret)
            {
                DataCertificate.ExportToPfxFile(keyName, "VanPeng.pfx", "123456", true);
                X509Certificate2 x509 = DataCertificate.GetCertificateFromPfxFile("VanPeng.pfx", "123456");
                string publickey = x509.PublicKey.Key.ToXmlString(false);
                string privatekey = x509.PrivateKey.ToXmlString(true);

                string myname = "my name is VanPeng.Desktop.Licence!";
                string enStr = RSAEncrypt(publickey, myname); 
                string deStr = RSADecrypt(privatekey, enStr); 
            }
        }
        public static void TestRsa()
        {
            string myname = "my name is VanPeng.Desktop.Licence!";
            string enStr = RSAEncrypt(publicKey, myname);
            string deStr = RSADecrypt(privateKey, enStr);
        }
       
   /// <summary> 
        /// RSA解密 
        /// </summary> 
        /// <param name="xmlPrivateKey"></param> 
        /// <param name="m_strDecryptString"></param> 
        /// <returns></returns> 
        public static string RSADecrypt(string xmlPrivateKey, string m_strDecryptString)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(xmlPrivateKey);
            byte[] rgb = Convert.FromBase64String(m_strDecryptString);
            byte[] bytes = provider.Decrypt(rgb, false);
            return new UnicodeEncoding().GetString(bytes);
        }
        /// <summary> 
        /// RSA加密 
        /// </summary> 
        /// <param name="xmlPublicKey"></param> 
        /// <param name="m_strEncryptString"></param> 
        /// <returns></returns> 
        public static string RSAEncrypt(string xmlPublicKey, string m_strEncryptString)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(xmlPublicKey);
            byte[] bytes = new UnicodeEncoding().GetBytes(m_strEncryptString);
            return Convert.ToBase64String(provider.Encrypt(bytes, false));
        }
       
      
    }
}
