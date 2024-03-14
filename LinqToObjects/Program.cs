using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LinqToObjects
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. List all products whose price in between 50K to 80K
            var prods1 = from p in ProductsDB.GetProducts()
                        where p.Price >= 50000 && p.Price <= 80000
                        select p;
            Console.WriteLine("1.");
            foreach (var p in prods1)
            {
                Console.WriteLine($"{p.Name} - {p.Price}");
            }

            //2. Extract all products belongs to Laptops catagory
            var prods2 = from p in ProductsDB.GetProducts()
                         where p.Catagory.Name == "Laptops"
                         select p;
            Console.WriteLine("2.");
            foreach (var p in prods2)
            {
                Console.WriteLine($"{p.Name} - {p.Price}");
            }

            //3. Extract/Show Product Name and Catagory Name only
            var prodCat = from p in ProductsDB.GetProducts()
                          select new {productName = p.Name, CatName = p.Catagory.Name};
            Console.WriteLine("3.");
            foreach(var pc in  prodCat)
            {
                Console.WriteLine($"{pc.productName} - {pc.CatName}");
            }
            //4. Show the costliest product name 
            var CostliestProd = (from p in ProductsDB.GetProducts()
                                 orderby p.Price descending
                                 select p).Take(1);
            Console.WriteLine("4.");
            foreach ( var p in CostliestProd)
                Console.WriteLine("Costliest Product Name: " + p.Name);

            //5. Show the cheepest product name and its price
            var cheapestProd = (from p in ProductsDB.GetProducts()
                                 orderby p.Price ascending
                                 select p).Take(1);
            Console.WriteLine("5.");
            foreach (var p in cheapestProd)
                Console.WriteLine($"Cheapest Product: {p.Name} - {p.Price}");
            //6. Show the 2nd and 3rd product details
            var prod23 = (from p in ProductsDB.GetProducts()
                          select p).Skip(1).Take(2);

            Console.WriteLine("6.");
            foreach(var p in prod23)
                Console.WriteLine($"{p.Name}");

            //7. List all products in assending order of their price
            var AscendingProds = from p in ProductsDB.GetProducts()
                                 orderby p.Price ascending
                                 select p;
            Console.WriteLine("7.");
            foreach (var p in AscendingProds)
                Console.WriteLine($"{p.Name} - {p.Price}");
            //8. Count the no. of products belong to Tablets
            var tabProds = from p in ProductsDB.GetProducts()
                           where p.Catagory.Name == "Tablets"
                           select p;
            Console.WriteLine("8.");
            Console.WriteLine($"tablet products are: {tabProds.Count()}");
            //9. Show which catagory has costly product
            Console.WriteLine("9.");
            foreach (var p in CostliestProd)
                Console.WriteLine("Costliest Product Name and Category: " + p.Name +" "+ p.Catagory.Name);
            //10. Show which catagory has less products
            var CatagoryLessProds = (from p in ProductsDB.GetProducts()
                                    group p by p.Catagory into grouped
                                    orderby grouped.Count()
                                    select grouped.Key.Name).FirstOrDefault();
            Console.WriteLine("10.");
            Console.WriteLine(CatagoryLessProds);
            //11. Extract the Product Details based on the catagory and show as 
            //Laptops
            //    Dell XPS 13
            //    HP 430
            //Mobiles
            //    IPhone 6
            //    Galaxy S6
            //Tablets
            //    IPad Pro

            var catagoryBased = from p in ProductsDB.GetProducts()
                                group p by p.Catagory into grouped
                                select grouped;
            Console.WriteLine("11.");
            foreach(var group in catagoryBased)
            {
                Console.WriteLine($"{group.Key.Name}");
                foreach (var product in group)
                {
                    Console.WriteLine($" - {product.Name}");
                }
            }

            //12. Extract the Product count based on the catagory and show as below
            //Laptops : 2
            //Mobiles: 2
            //Tablets: 1
            Console.WriteLine("12.");
            foreach (var group in catagoryBased)
            {
                Console.WriteLine($"{group.Key.Name} - {group.Count()}");
            }
        }
    }
    class ProductsDB
    {
        public static List<Product> GetProducts()
        {
            Catagory cat1 = new Catagory { CatagoryID = 101, Name = "Laptops" };
            Catagory cat2 = new Catagory { CatagoryID = 201, Name = "Mobiles" };
            Catagory cat3 = new Catagory { CatagoryID = 301, Name = "Tablets" };

            Product p1 = new Product { ProductID = 1, Name = "Dell XPS 13", Catagory = cat1, Price = 90000 };
            Product p2 = new Product { ProductID = 2, Name = "HP 430", Catagory = cat1, Price = 50000 };
            Product p3 = new Product { ProductID = 3, Name = "IPhone 6", Catagory = cat2, Price = 80000 };
            Product p4 = new Product { ProductID = 4, Name = "Galaxy S6", Catagory = cat2, Price = 74000 };
            Product p5 = new Product { ProductID = 5, Name = "IPad Pro", Catagory = cat3, Price = 44000 };

            cat1.Products.Add(p1);
            cat1.Products.Add(p2);
            cat2.Products.Add(p3);
            cat2.Products.Add(p4);
            cat3.Products.Add(p5);

            List<Product> products = new List<Product>();
            products.Add(p1);
            products.Add(p2);
            products.Add(p3);
            products.Add(p4);
            products.Add(p5);

            return products;
        }
    }
    class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Catagory Catagory { get; set; }
    }
    class Catagory
    {
        public int CatagoryID { get; set; }
        public string Name { get; set; }
        public List<Product> Products = new List<Product>();
    }
}
