using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Collections.Generic;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class ProductControllerTests
    {
        /// <summary>
        /// Create a mock repository for test purposes.
        /// </summary>
        /// <returns>Mock repository</returns>
        private static Mock<IProductRepository> getMockProductRepository()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            }.AsQueryable());
            return mock;
        }
        
        /// <summary>
        /// Tests if the pagination in the List action method works correctly.
        /// </summary>
        [TestMethod]
        public void Can_Paginate()
        {
            //arrange
            Mock<IProductRepository> mock = getMockProductRepository();

            ProductController controller = new ProductController(mock.Object);

            controller.PageSize = 3; // 3 products per page

            //act
            ProductListViewModel result = (ProductListViewModel) controller.List(page: 2).Model;

            //assert
            Product[] productsOnPage = result.Products.ToArray();
            Assert.IsTrue(productsOnPage.Length == 2);
            Assert.AreEqual(productsOnPage[0].Name, "P4");
            Assert.AreEqual(productsOnPage[1].Name, "P5");

        }

        /// <summary>
        /// Tests if the List action method returns a correct ProductViewModel.
        /// </summary>
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //arrange
            Mock<IProductRepository> mock = getMockProductRepository();
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //act
            ProductListViewModel result = (ProductListViewModel) controller.List(page: 2).Model;

            //assert
            Assert.AreEqual(2, result.PagingInfo.CurrentPage);
            Assert.AreEqual(3, result.PagingInfo.ItemsPerPage);
            Assert.AreEqual(2, result.PagingInfo.TotalPages);
            Assert.AreEqual(5, result.PagingInfo.TotalItems);
        }
    }
}
