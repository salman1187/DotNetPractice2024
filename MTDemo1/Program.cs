using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;

namespace MTDemo1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Main() running in thread id: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Sequence: ");
            Stopwatch sw = Stopwatch.StartNew();
            M1();
            M2();
            Console.WriteLine(sw.ElapsedMilliseconds);

            Console.WriteLine("Threads: ");
            sw = Stopwatch.StartNew();

            ThreadStart ts1 = new ThreadStart(M1);
            Thread t1 = new Thread(ts1); //child thread
            t1.Start();
            Thread t2 = new Thread(M2);
            t2.Start();

            t1.Join();
            t2.Join(); //waiting for t1 and t2 end
            Console.WriteLine(sw.ElapsedMilliseconds);

            //Using Task class 
            Console.WriteLine("Task Threads: ");
            sw = Stopwatch.StartNew();
            Task tt1 = new Task(M1);
            tt1.Start();
            Task tt2 = new Task(M2);
            tt2.Start();
            tt1.Wait();
            tt2.Wait();
            Console.WriteLine(sw.ElapsedMilliseconds);

            //Using Parallel class (blocks the parent thread)
            Console.WriteLine("Parallel Threads: ");
            sw = Stopwatch.StartNew();
            Parallel.Invoke(M11, M22);
            Console.WriteLine(sw.ElapsedMilliseconds);

            //Console.WriteLine(Environment.ProcessorCount);

        }
        static void M1()
        {
            Console.WriteLine($"M1() running in thread id: {Thread.CurrentThread.ManagedThreadId}");
            for(int i=0; i<10; i++)
            {
                Thread.Sleep(1000);
            }

        }
        static void M2()
        {
            Console.WriteLine($"M2() running in thread id: {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
            }
        }
        static void M11()
        {
            Console.WriteLine($"M11() running in thread id: {Thread.CurrentThread.ManagedThreadId}");

            //passing these options allows using only given no. of cores
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = Environment.ProcessorCount / 2;

            //for (int i = 0; i < 10; i++)
            Parallel.For(0,10, options, i =>
            {
                Thread.Sleep(1000);
            });

        }
        static void M22()
        {
            Console.WriteLine($"M22() running in thread id: {Thread.CurrentThread.ManagedThreadId}");
            Parallel.For(0, 10, i =>
            {
                Thread.Sleep(1000);
            });
        }
    }
}
