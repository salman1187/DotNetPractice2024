using ProductCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.DataAccess
{
    public class ProductsDbContext : DbContext
    {
        //configure database
        public ProductsDbContext() : base("defaultConnection")
        {

        }
        //configure table
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Person> People { get; set; }   
    }
}
