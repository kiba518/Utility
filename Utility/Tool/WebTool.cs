using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class WebTool
    {
        public static void OpenNetPage(string url)
        {
            System.Diagnostics.Process.Start("iexplore", url);//System.Diagnostics.Process.Start(url);默认浏览器打开  
        }  
    }
}
