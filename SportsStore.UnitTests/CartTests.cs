using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;

namespace SportsStore.UnitTests
{
    /// <summary>
    /// Tests concerning the Cart entity
    /// </summary>
    [TestClass]
    public class CartTests
    {
        /// <summary>
        /// Tests adding new products to a cart.
        /// </summary>
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            //arrange
            Product p1 = new Product
            {
                ProductID = 1,
                Name = "P1"
            };

            Product p2 = new Product
            {
                ProductID = 2,
                Name = "P2"
            };

            Cart cart = new Cart();

            //act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);
            var result = cart.Lines.ToArray();

            //assert
            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(result[0].Product == p1);
            Assert.IsTrue(result[1].Product == p2);
        }

        /// <summary>
        /// Tests adding products to a cart that already has that product in it.
        /// </summary>
        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //arrange
            Product p1 = new Product
            {
                ProductID = 1,
                Name = "P1"
            };

            Product p2 = new Product
            {
                ProductID = 2,
                Name = "P2"
            };

            Cart cart = new Cart();

            //act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 2);
            CartLine[] result = cart.Lines.ToArray();

            //assert
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(3, result[0].Quantity);
            Assert.AreEqual(1, result[1].Quantity);

        }

        /// <summary>
        /// Tests removing products from a cart.
        /// </summary>
        [TestMethod]
        public void Can_Remove_Line()
        {
            //arrange
            Product p1 = new Product
            {
                ProductID = 1,
                Name = "P1"
            };

            Product p2 = new Product
            {
                ProductID = 2,
                Name = "P2"
            };
            
            Product p3 = new Product
            {
                ProductID = 3,
                Name = "P3"
            };

            Cart cart = new Cart();

            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);
            cart.AddItem(p3, 5);
            cart.AddItem(p1, 2);

            //act
            cart.RemoveItem(p1);
            var result = cart.Lines.ToArray();

            //assert
            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(result[0].Product == p2);
            Assert.IsTrue(result[1].Product == p3);
            Assert.AreEqual(0, result.Where(cl => cl.Product == p1).Count());
        }

        /// <summary>
        /// Tests calculating the total value of the cart.
        /// </summary>
        [TestMethod]
        public void Calculate_Cart_Total()
        {
            //arrange
            Product p1 = new Product
            {
                ProductID = 1,
                Name = "P1",
                Price = 10M
            };

            Product p2 = new Product
            {
                ProductID = 2,
                Name = "P2",
                Price = 12
            };

            Cart cart = new Cart();

            cart.AddItem(p1, 2);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 1);

            // act
            decimal result = cart.CountTotalValue();

            // assert
            Assert.AreEqual(42M, result);
        }

        /// <summary>
        /// Tests clearing the cart from products completely.
        /// </summary>
        [TestMethod]
        public void Can_Clear_Cart()
        {
            //arrange
            Product p1 = new Product
            {
                ProductID = 1,
                Name = "P1"
            };

            Product p2 = new Product
            {
                ProductID = 2,
                Name = "P2"
            };

            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);

            //act
            cart.ClearCart();
            
            //assert
            Assert.AreEqual(0, cart.Lines.Count());
        }
    }
}
