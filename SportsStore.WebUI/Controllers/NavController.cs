using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    /// <summary>
    /// Navigation controller.
    /// </summary>
    public class NavController : Controller
    {
        private IProductRepository ProductRepository
        {
            get;
            set;
        }

        public NavController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        /// <summary>
        /// Action method for the menu view.
        /// </summary>
        /// <returns>The menu</returns>
        public PartialViewResult Menu(string category = null)
        {
            NavMenuViewModel vm = new NavMenuViewModel()
            {
                Categories = ProductRepository.Products
                    .Select(p => p.Category)
                    .Distinct()
                    .OrderBy(p => p),

                CurrentCategory = category
            };

            return PartialView(vm);
        }
	}
}