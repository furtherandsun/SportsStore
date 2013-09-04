using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;

namespace SportsStore.UnitTests
{
    /// <summary>
    /// Tests for the admin controller.
    /// </summary>
    [TestClass]
    public class AdminTests
    {

        private static Mock<IProductRepository> getProductRepositoryMock()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product() { ProductID=1, Name="P1"},
                new Product() { ProductID=2, Name="P2"},
                new Product() { ProductID=3, Name="P3"},
            }.AsQueryable());
            return mock;
        }

        /// <summary>
        /// Tests if the model passed to the index view holds all the products in the repository.
        /// </summary>
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            //arrange
            Mock<IProductRepository> mock = getProductRepositoryMock();

            AdminController controller = new AdminController(mock.Object);

            //act
            Product[] result = ((IEnumerable<Product>) controller.Index().ViewData.Model).ToArray();

            //assert
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }


        /// <summary>
        /// Tests if it is possible to edit products that exists in the repository
        /// </summary>
        [TestMethod]
        public void Can_Edit_Product()
        {
            // arrange
            Mock<IProductRepository> mock = getProductRepositoryMock();

            AdminController controller = new AdminController(mock.Object);

            //act
            Product p1 = controller.Edit(1).ViewData.Model as Product;
            Product p2 = controller.Edit(2).ViewData.Model as Product;
            Product p3 = controller.Edit(3).ViewData.Model as Product;

            //assert
            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);
        }

        /// <summary>
        /// Tests if it is possible to edit products that doesn't exists in the repository
        /// </summary>
        [TestMethod]
        public void Cannot_Edit_Nonexistant_Product()
        {
            //arrange
            Mock<IProductRepository> mock = getProductRepositoryMock();

            AdminController controller = new AdminController(mock.Object);

            //act
            Product product = controller.Edit(4).ViewData.Model as Product;

            //assert
            Assert.IsNull(product);
        }

        /// <summary>
        /// Tests the ability to save a correct model
        /// </summary>
        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            AdminController controller = new AdminController(mock.Object);

            Product product = new Product()
            {
                ProductID = 1,
                Name = "P1"
            };

            //act
            ActionResult result = controller.Edit(product);

            //assert
            mock.Verify(m => m.UpdateProduct(product));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        /// Tests the ability to save an incorrect model
        /// </summary>
        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {            
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            AdminController controller = new AdminController(mock.Object);

            Product product = new Product()
            {
                ProductID = 1,
                Name = "P1"
            };

            controller.ModelState.AddModelError("error", "error");

            //act
            ActionResult result = controller.Edit(product);

            //Assert
            mock.Verify(m => m.UpdateProduct(product), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
