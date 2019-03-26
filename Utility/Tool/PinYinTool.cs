using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class PinYinTool
    {
        public static string PY(string hz)
        {
            string ls_second_eng = "SE";
            string ls_second_ch = "参诶";
            string return_py = "";
            byte[] array = new byte[2];
            for (int i = 0; i < hz.Length; i++)
            {
                array = System.Text.Encoding.Default.GetBytes(hz[i].ToString());
                //非汉字
                if (array.Length < 2)
                {
                    return_py += hz[i];
                }
                else if (ls_second_ch.Contains(hz[i].ToString()))
                {
                    return_py += ls_second_eng.Substring(ls_second_ch.IndexOf(hz[i].ToString(), 0), 1);
                }
                else if (array.Length == 2) //&& array[0] <= 215
                {
                    if (hz[i].ToString().CompareTo("匝") >= 0)
                        return_py += "z";
                    else if (hz[i].ToString().CompareTo("压") >= 0)
                        return_py += "y";
                    else if (hz[i].ToString().CompareTo("夕") >= 0)
                        return_py += "x";
                    else if (hz[i].ToString().CompareTo("哇") >= 0)
                        return_py += "w";
                    else if (hz[i].ToString().CompareTo("他") >= 0)
                        return_py += "t";
                    else if (hz[i].ToString().CompareTo("撒") >= 0)
                        return_py += "s";
                    else if (hz[i].ToString().CompareTo("然") >= 0)
                        return_py += "r";
                    else if (hz[i].ToString().CompareTo("七") >= 0)
                        return_py += "q";
                    else if (hz[i].ToString().CompareTo("趴") >= 0)
                        return_py += "p";
                    else if (hz[i].ToString().CompareTo("哦") >= 0)
                        return_py += "o";
                    else if (hz[i].ToString().CompareTo("拿") >= 0)
                        return_py += "n";
                    else if (hz[i].ToString().CompareTo("妈") >= 0)
                        return_py += "m";
                    else if (hz[i].ToString().CompareTo("垃") >= 0)
                        return_py += "l";
                    else if (hz[i].ToString().CompareTo("喀") >= 0)
                        return_py += "k";
                    else if (hz[i].ToString().CompareTo("讥") >= 0)
                        return_py += "j";
                    else if (hz[i].ToString().CompareTo("哈") >= 0)
                        return_py += "h";
                    else if (hz[i].ToString().CompareTo("伽") >= 0)
                        return_py += "g";
                    else if (hz[i].ToString().CompareTo("发") >= 0)
                        return_py += "f";
                    else if (hz[i].ToString().CompareTo("讹") >= 0)
                        return_py += "e";
                    else if (hz[i].ToString().CompareTo("搭") >= 0)
                        return_py += "d";
                    else if (hz[i].ToString().CompareTo("擦") >= 0)
                        return_py += "c";
                    else if (hz[i].ToString().CompareTo("芭") >= 0)
                        return_py += "b";
                    else if (hz[i].ToString().CompareTo("阿") >= 0)
                        return_py += "a";
                }

            }
            return return_py.ToUpper();
        }

    }
}
