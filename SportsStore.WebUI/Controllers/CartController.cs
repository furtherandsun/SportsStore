using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository ProductRepository
        {
            get;
            set;
        }

        public CartController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel()
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        /// <summary>
        /// Add a product to the cart.
        /// </summary>
        /// <param name="productId">Id of the product to add</param>
        /// <param name="returnUrl">The URL to return to</param>
        /// <returns></returns>
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = ProductRepository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new
            {
                returnUrl = returnUrl
            });
        }

        /// <summary>
        /// Remove a product from the cart.
        /// </summary>
        /// <param name="productId">Id of the product to remove</param>
        /// <param name="returnUrl">The URL to return to</param>
        /// <returns></returns>
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = ProductRepository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.RemoveItem(product);
            }

            return RedirectToAction("Index", new
            {
                returnUrl = returnUrl
            });
        }

	}
}