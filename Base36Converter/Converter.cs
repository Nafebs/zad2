using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base36Converter
{
    public static class Converter
    {
        private const int Base = 36;
        private const string Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string encode(int value)
        {
            string result = "";

            while (value > 0)
            {
                result = Chars[value % Base] + result;
                value /= Base;
            }

            return result;
        }

        public static long decode(string number)
        {
            var reversed = number.ToUpper().Reverse();
            long result = 0;
            int pos = 0;
            foreach (char c in reversed)
            {
                result += Chars.IndexOf(c) * (long)Math.Pow(36, pos);
                pos++;
            }
            return result;
        }
    }
}
