using SportsStore.Domain.Abstract;
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
    class EFProductRepository : IProductRepository
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
    }
}
