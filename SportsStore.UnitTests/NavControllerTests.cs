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
    public class NavControllerTests
    {
        /// <summary>
        /// Create a mock repository for test purposes.
        /// </summary>
        /// <returns>Mock repository</returns>
        private static Mock<IProductRepository> getMockProductRepository()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "C1"},
                new Product {ProductID = 2, Name = "P2", Category = "C1"},
                new Product {ProductID = 3, Name = "P3", Category = "C2"},
                new Product {ProductID = 4, Name = "P4", Category = "C2"},
                new Product {ProductID = 5, Name = "P5", Category = "C2"}
            }.AsQueryable());
            return mock;
        }

        /// <summary>
        /// Tests of the Menu action method generates the categories correct.
        /// </summary>
        [TestMethod]
        public void Can_Generate_Categories()
        {
            //arrange
            Mock<IProductRepository> mock = getMockProductRepository();
            NavController controller = new NavController(mock.Object);

            //act
            string[] result = ((NavMenuViewModel) controller.Menu().Model).Categories.ToArray();

            //assert
            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(result[0] == "C1");
            Assert.IsTrue(result[1] == "C2");

        }

        /// <summary>
        /// Test for checking if the current category is correct in the Menu action method.
        /// </summary>
        [TestMethod]
        public void Indicates_Selected_Category()
        {
            //arrange
            Mock<IProductRepository> mock = getMockProductRepository();
            NavController controller = new NavController(mock.Object);
            string selectedCategory = "C2";

            //act
            string result = ((NavMenuViewModel) controller.Menu(selectedCategory).Model).CurrentCategory;

            //assert
            Assert.AreEqual(selectedCategory, result);
        }
    }
}
