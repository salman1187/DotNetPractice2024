using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace DelegatesDemo2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // anonymous delegate
            FilterDelegate filter = new FilterDelegate(delegate (Process p){
            return p.WorkingSet64 >= 100 * 1024 * 1024;
            });

            // lambda expressions - light weight syntax for anonymous delegates
            // lambda statement:
            //FilterDelegate filter = new FilterDelegate((Process p) => {
            //    return p.WorkingSet64 >= 100 * 1024 * 1024;
            //});

            ProcessManager.ShowProcessList(filter);
        }
        public static bool FilterByName(Process p)
        {
            return p.ProcessName.StartsWith("W");
        }
        //public static bool FilterBySize(Process p)
        //{
        //    return p.WorkingSet64 >= 100 * 1024 * 1024;
        //}
    }
    public delegate bool FilterDelegate(Process p);
    public class ProcessManager
    {
        public static void ShowProcessList(FilterDelegate filter)
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (filter(p))
                    Console.WriteLine(p.ProcessName);
            }
        }
    }
}
