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
        //TPC
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().Map(e => e.MapInheritedProperties());
            modelBuilder.Entity<Customer>().ToTable("Customers");

            modelBuilder.Entity<Supplier>().Map(e => e.MapInheritedProperties());
            modelBuilder.Entity<Supplier>().ToTable("Suppliers");

            modelBuilder.Types().Configure(t => t.MapToStoredProcedures()); //!!!
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Types().Configure(t => t.MapToStoredProcedures()); //!!!
        //}
    }
}
