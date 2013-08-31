using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Collections.Generic;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);

            controller.PageSize = 3; // 3 products per page

            //act
            IEnumerable<Product> result = (IEnumerable<Product>) controller.List(page: 2).Model;

            //assert
            Product[] productsOnPage = result.ToArray();
            Assert.IsTrue(productsOnPage.Length == 2);
            Assert.AreEqual(productsOnPage[0].Name, "P4");
            Assert.AreEqual(productsOnPage[1].Name, "P5");

        }
    }
}
