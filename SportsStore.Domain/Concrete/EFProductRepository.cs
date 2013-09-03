using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    /// <summary>
    /// Concrete implementation of the product repository targeting EF.
    /// </summary>
    public class EFProductRepository : IProductRepository
    {
        EFDbContext context = new EFDbContext();

        /// <summary>
        /// Retrieve all products.
        /// </summary>
        public IQueryable<Entities.Product> Products
        {
            get
            {
                return context.Products;
            }
        }

        /// <summary>
        /// Updates a product in the repository.
        /// </summary>
        /// <param name="product">Product to update</param>
        public void UpdateProduct(Product product)
        {

            Product entry = context.Products.Find(product.ProductID);
            if (entry != null)
            {
                entry.Name = product.Name;
                entry.Category = product.Category;
                entry.Description = product.Description;
                entry.Price = product.Price;

                context.SaveChanges();
            }

        }

        /// <summary>
        /// Adds a product to the repository.
        /// </summary>
        /// <param name="product">Product to add</param>
        public void AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }
    }
}
