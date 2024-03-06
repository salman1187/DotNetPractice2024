using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesLab1
{
    delegate string StringModifier(string str);
    internal class Program
    {
        static void Main(string[] args)
        {
            StringModifier del = new StringModifier(Reverse);
            StringModifier del2 = new StringModifier(Uppercase);
            string ans = del2(del("Animal"));

            Console.WriteLine(ans);
        }
        public static string Uppercase(string str)
        {
            return str.ToUpper();
        }
        public static string Lowercase(string str)
        {
            return str.ToLower();
        }
        public static string Reverse(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            string reversed = new string(charArray);
            return reversed;
        }
    }
}
