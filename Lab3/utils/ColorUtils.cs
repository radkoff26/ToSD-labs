using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.utils
{
    internal class ColorUtils
    {

        public static string ColorToHexString(Color color)
        {
            string a = color.A.ToString("X");
            string r = color.R.ToString("X");
            string g = color.G.ToString("X");
            string b = color.B.ToString("X");
            return "#" + a + r + g + b;
        }
    }
}
