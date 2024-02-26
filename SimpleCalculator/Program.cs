using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int fno, sno;
            Console.Write("Enter the first number: ");
            fno = int.Parse(Console.ReadLine());
            Console.Write("Enter the second number: ");
            sno = int.Parse(Console.ReadLine());

            //SRP = Single responsibility principle
            ComputeLibrary.Calculator c = new ComputeLibrary.Calculator();
            int max = c.FindMax(fno, sno);
            
            Console.WriteLine($"The maximum of {fno} and {sno} is {max}");
        }
    }
}