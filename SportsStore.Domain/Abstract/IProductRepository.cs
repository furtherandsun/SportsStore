using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products
        {
            get;
        }

        void AddProduct(Product product);

        void UpdateProduct(Product product);

        Product DeleteProduct(int productId);
    }
}
