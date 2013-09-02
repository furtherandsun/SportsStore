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

        private IOrderProcessor OrderProcessor
        {
            get;
            set;
        }

        public CartController(IProductRepository productRepository, IOrderProcessor orderProcessor)
        {
            ProductRepository = productRepository;
            OrderProcessor = orderProcessor;
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

        /// <summary>
        /// Get a cart summary partial view.
        /// </summary>
        /// <param name="cart">The customer's cart</param>
        /// <returns></returns>
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        /// <summary>
        /// Get the checkout page.
        /// </summary>
        /// <returns></returns>
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        /// <summary>
        /// Post the checkout page.
        /// </summary>
        /// <param name="cart">The customer's cart</param>
        /// <param name="shippingDetails">The customer's shipping details</param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                OrderProcessor.ProcessOrder(cart, shippingDetails);
                cart.ClearCart();
                return View("Completed");
            }

            // Return to the checkout view with the shipping details if the modelstate isn't valid
            return View(shippingDetails);
        }
	}
}