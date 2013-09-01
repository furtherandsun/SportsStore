using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    /// <summary>
    /// Represents a shopping cart on the site.
    /// </summary>
    public class Cart
    {
        private List<CartLine> collection = new List<CartLine>();

        /// <summary>
        /// Adds an item to the cart.
        /// </summary>
        /// <param name="product">Product to add</param>
        /// <param name="quantity">Product quantity</param>
        public void AddItem(Product product, int quantity)
        {
            CartLine line = collection.FirstOrDefault(cl => cl.Product.ProductID == product.ProductID);

            if (line == null)
            {
                collection.Add(new CartLine()
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        /// <summary>
        /// Remove and item from the cart.
        /// </summary>
        /// <param name="product">Product to remove</param>
        public void RemoveItem(Product product)
        {
            collection.RemoveAll(cl => cl.Product.ProductID == product.ProductID);
        }

        /// <summary>
        /// Clear the cart completely.
        /// </summary>
        public void ClearCart()
        {
            collection.Clear();
        }

        /// <summary>
        /// Count the total value of the products in the cart.
        /// </summary>
        /// <returns>Total cart value</returns>
        public decimal CountTotalValue()
        {
            return collection.Sum(cl => cl.Product.Price * cl.Quantity);
        }

        /// <summary>
        /// Exposes the internal collection as an enumerable.
        /// </summary>
        public IEnumerable<CartLine> Lines
        {
            get
            {
                return collection;
            }
        }
    }

    /// <summary>
    /// Represents a product and its quantity in a cart.
    /// </summary>
    public class CartLine
    {
        public Product Product
        {
            get;
            set;
        }

        public int Quantity
        {
            get;
            set;
        }
    }
}
