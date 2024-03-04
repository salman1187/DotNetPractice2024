using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesDemo
{
    // delegate keyword inherits the Delegate class and implements it.
    // declaration: 
    delegate void MyDelegate(string str);
    internal class Program
    {
        static void Main(string[] args)
        {
            //instantiation AND initialization: 
            MyDelegate del = new MyDelegate(Greeting);
            Program p = new Program();
            del += p.Hi; // subscription
            //invocation:
            //del.Invoke("Hello");
            del("Hello");

        }
        static void Greeting(string text)
        {
            Console.WriteLine($"Greeting: {text}");
        }
        public void Hi(string str)
        {
            Console.WriteLine($"Hi: {str}");
        }
    }
}
