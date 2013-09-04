using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository ProductRepository
        {
            get;
            set;
        }

        public AdminController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        /// <summary>
        /// List all the products in the repository
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            return View(ProductRepository.Products);
        }

        /// <summary>
        /// Retrieves a product in the repository for edit purposes.
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <returns></returns>
        public ViewResult Edit(int productId)
        {
            Product product = ProductRepository.Products.FirstOrDefault(p => p.ProductID == productId);

            return View(product);
        }

        /// <summary>
        /// Saves an edited product to the repository.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.UpdateProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }
	}
}