using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {

        // The controller's internal representation of the Product repository
        private IProductRepository ProductRepository
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productRepository">Repository to inject</param>
        public ProductController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        /// <summary>
        /// Lists the products
        /// </summary>
        /// <returns>A view listing the products</returns>
        public ViewResult List()
        {
            return View(ProductRepository.Products);
        }
        
    }
}