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
        /// Retrieves a product in the repository for editing purposes.
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
                TempData["message"] = string.Format("{0} has been edited", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        /// <summary>
        /// Retrieves the view for creating a new product.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View(new Product());
        }

        /// <summary>
        /// Saves a new product to the repository.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.AddProduct(product);
                TempData["message"] = string.Format("{0} has been created", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }
	}
}