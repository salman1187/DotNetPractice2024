using ProductCatalog.DataAccess;
using ProductCatalog.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            //while(true)
            //{
            //    Console.WriteLine("1.Create\n2.Delete\n3.Edit\n4.Get All\n5.Get By Id\n6.Get Cheapest Produt\n7.Get Costliest Product\n8.Get Product Count\n0.Exit ");
            //    int c = int.Parse(Console.ReadLine());
            //    switch (c)
            //    {
            //        case 1:
            //            Create();
            //            break;
            //        case 2:
            //            Delete();
            //            break;
            //        case 3:
            //            Edit();
            //            break;
            //        case 4:
            //            GetAll();
            //            break;
            //        case 5:
            //            GetById();
            //            break;
            //        case 6:
            //            GetCheapest();
            //            break;
            //        case 7:
            //            GetCostliest();
            //            break;
            //        case 8:
            //            GetProductCount();
            //            break;
            //        case 0:
            //            return;
            //        default:
            //            Console.WriteLine("Wrong input");
            //            break;
            //    }
            //}
            ProductsDbContext db = new ProductsDbContext();
            //select all products and display product name and category name
            //var allProducts = from p in db.Products //.Include("Category") //eager loading 
            //                  select p;

            //foreach (var item in allProducts)
            //{
            //    Console.WriteLine($"{item.Name} - {item.Category.Name}");
            //}

            //add customer and one supplier
            //Customer c = new Customer { CustType = "Gold", Discount = 12, Email = "cust1@gmail.com", Location = "Hyderabad", Mobile = "97898780", Name = "Customer1" };
            //Supplier s = new Supplier { Email = "sup1@gmail.com", GST = "A2343", Location = "Bangalore", Mobile = "7889887", Name = "supplier1", TradeLic = "Tx345r2" };
            //db.People.Add(c);
            //db.People.Add(s);
            //db.SaveChanges();   
            //getting only customers:
            //var peoples = (from p in db.People
            //               select p).ToList();

            //foreach(var people in peoples)  
            //    if(people.GetType() == typeof(Customer))
            //        Console.WriteLine(people.Name);
            //or var allCustomers = db.People.OfType<Customer>().ToList(); 

            // !!!
            db.Database.ExecuteSqlCommand("update products set price = price + 1000"); //instead of iterating through all the records we can do this to improve the performance

        }
        public static void Create()
        {
            Product p = new Product();
            Console.WriteLine("Enter product name: ");
            p.Name = Console.ReadLine();
            Console.WriteLine("Enter price: ");
            p.Price = int.Parse(Console.ReadLine());

            IProductsRepository repo = new ProductsRepository();
            repo.Create(p);
            Console.WriteLine("Product Saved");
        }
        public static void Delete()
        {
            Console.WriteLine("Enter Id to delete");
            int id = int.Parse(Console.ReadLine());

            IProductsRepository repo = new ProductsRepository();
            repo.Delete(id);
            Console.WriteLine("Product Deleted");
        }
        public static void Edit()
        {
            IProductsRepository repo = new ProductsRepository();
            Product p = new Product();
            Console.WriteLine("Enter Id to edit");
            p.ProductID = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter product name: ");
            p.Name = Console.ReadLine();
            Console.WriteLine("Enter price: ");
            p.Price = int.Parse(Console.ReadLine());
            repo.Edit(p);
            Console.WriteLine("Product Edited");
        }
        public static void GetAll()
        {
            IProductsRepository repo = new ProductsRepository();
            List<Product> products = repo.GetAll();
            Console.WriteLine("All Products: ");
            foreach (Product p in products)
                Console.WriteLine($"{p.ProductID}. {p.Name} - {p.Price}");

        }
        public static void GetById()
        {
            Product p = new Product();
            Console.WriteLine("Enter Id: ");
            int id = int.Parse(Console.ReadLine());
            IProductsRepository repo = new ProductsRepository();
            p = repo.GetById(id);

            Console.WriteLine($"{p.ProductID}. {p.Name} - {p.Price}");
        }
        public static void GetCheapest()
        {
            IProductsRepository repo = new ProductsRepository();
            Product p = repo.GetCheapestProduct();
            Console.WriteLine("Cheapest: ");
            Console.WriteLine($"{p.ProductID}. {p.Name} - {p.Price}");
        }
        public static void GetCostliest()
        {
            IProductsRepository repo = new ProductsRepository();
            Product p = repo.GetCostliestProduct();
            Console.WriteLine("Costliest: ");
            Console.WriteLine($"{p.ProductID}. {p.Name} - {p.Price}");
        }
        public static void GetProductCount()
        {
            IProductsRepository repo = new ProductsRepository();
            Console.WriteLine($"Count: {repo.GetProductCount()}");
        }
    }
}
