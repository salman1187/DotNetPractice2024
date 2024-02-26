using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypesDemo
{
    internal class Program
    {
        //Product p1 = new Product(); //has-a
        static void Main(string[] args)
        {
            //Product p1 = new Product(1, "iPhone", 100000, "Apple", "Mobiles"); //uses
            //Object Initialization Syntax
            Product p2 = new Product() { ProductID = 1, Brand = "Apple" };
            //Anonymous Tyes - cannot return these
            var p3 = new { ProductID = 2, Name = "iPhone" };
        }
    }

    class Product //: Program //is-a
    {
        public int ProductID {  get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        /*
        public Product(int id, string name, int price, string brand, string catgeory)
        {
            ProductID = id;
            Name = name;
            Price = price;
            Brand = brand;
            Category = catgeory;
        }
        */
    }
}
