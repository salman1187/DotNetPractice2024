using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDemo2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task tt1 = new Task(M1);
            Task tt2 = new Task(() => { M2(100); });
            Task<int> tt3 = new Task<int>(M3);
            //
            //
            //lines executing parallelly
            //
            //
            int x = tt3.Result;
            Task<int> tt4 = new Task<int>(() => { return M4(100); });
            //
            //
            //lines executing parallelly
            //
            //
            int y = tt4.Result;
        }
        static void M1() { }
        static void M2(int a) { }
        static int M3() { return 1;  }    
        static int M4(int a) { return 1; }    
    }
}
