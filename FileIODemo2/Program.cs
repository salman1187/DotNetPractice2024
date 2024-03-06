using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileIODemo2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                Console.WriteLine($"{drive.Name} has total size of {drive.TotalSize} and free space of {drive.TotalFreeSpace}");
            }
        }
        private static void xmlSerialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Product));
            Product p = new Product { ProductId = 101, Name = "IPhone", Price = 90000 };
            Stream stream = File.OpenWrite("products.xml");
            xmlSerializer.Serialize(stream, p);
            stream.Close();
        }
        private static void Serialize()
        {
            Product p = new Product { ProductId = 101, Name = "IPhone", Price = 90000 };
            BinaryFormatter binary = new BinaryFormatter();
            Stream stream = File.OpenWrite("productBinary.dat");
            binary.Serialize(stream, p);
            stream.Close();
        }
        private static void Deserialize()
        {
            Product p = new Product();
            Stream stream = File.OpenRead("productBinary.dat");
            BinaryFormatter binary = new BinaryFormatter();
            p = (Product)binary.Deserialize(stream);
            stream.Close();
        }
    }

    [Serializable] //only needed for binary serialization
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }    
        public int Price { get; set; }  
    }
}
