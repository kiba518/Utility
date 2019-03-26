using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace UtilityTestConsole
{
    class ImgTest
    {
        static void AddWaterImg( )
        {
            ImageHelper.AddWaterImg(@"C:\Users\Administrator\Desktop\1.jpg",
                @"C:\Users\Administrator\Desktop\11.jpg", @"C:\Users\Administrator\Desktop\z.png");
        }
    }
}
