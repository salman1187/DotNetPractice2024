using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIODemo1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.Write("Enter a name: ");
            //string name = Console.ReadLine();

            //StreamWriter writer = new StreamWriter("C:\\Users\\MOHAMMAD SALMAN\\Documents\\names.txt", true);
            //writer.WriteLine(name);
            //writer.Close();

            string readLine;
            using (StreamReader reader = new StreamReader("C:\\Users\\MOHAMMAD SALMAN\\Documents\\names.txt"))
            {
                readLine = reader.ReadToEnd();
            }
            
            
        }
    }
}
