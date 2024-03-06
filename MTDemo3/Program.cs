using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MTDemo3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BigData bigData = new BigData();
            Parallel.Invoke(bigData.Fill, bigData.Fill);
            Console.WriteLine(bigData.Stack.Count);
        }
    }
    class BigData
    {
        public ConcurrentStack<int> Stack = new ConcurrentStack<int>();
        //[MethodImpl(MethodImplOptions.Synchronized)] //Sequencializes the following method
        //use the above if a method has every line as critical
        public void Fill()
        {
            for (int i = 0; i<1000000;  i++)
            {
                //Monitor.Enter(this);
                //Monitor.Exit(this);
                //lock(this) {}
                Stack.Push(i);
            }
        }
    }
}
