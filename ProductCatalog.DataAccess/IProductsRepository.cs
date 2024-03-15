using ProductCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.DataAccess
{
    public interface IProductsRepository
    {
        void Create(Product product);
        void Delete(int id);
        void Edit(Product product);

        List<Product> GetAll();
        Product GetById(int id);

        Product GetCostliestProduct();

        Product GetCheapestProduct();

        int GetProductCount();
    }
}
