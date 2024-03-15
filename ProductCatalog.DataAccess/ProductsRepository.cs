using ProductCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.DataAccess
{
    public class ProductsRepository : IProductsRepository
    {
        private ProductsDbContext db = new ProductsDbContext();
        public void Create(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Product p = db.Products.Find(id);
            db.Products.Remove(p);
            db.SaveChanges();
        }

        public void Edit(Product product)
        {
            Product p = db.Products.Find(product.ProductID);
            p.ProductID = product.ProductID;
            p.Name = product.Name;
            p.Price = product.Price;
            db.SaveChanges();   
        }

        public List<Product> GetAll()
        {
            var products = (from p in db.Products
                           select p).ToList();

            return products;
        }

        public Product GetById(int id)
        {
            var p = db.Products.Find(id);
            return p;
        }

        public Product GetCheapestProduct()
        {
            var p = (from pro in db.Products
                     orderby pro.Price ascending
                     select pro).FirstOrDefault();
            return p;
        }

        public Product GetCostliestProduct()
        {
            var p = (from pro in db.Products
                     orderby pro.Price descending
                     select pro).FirstOrDefault();
            return p;
        }

        public int GetProductCount()
        {
            var count = db.Products.Count();
            return count;
        }
    }
}
