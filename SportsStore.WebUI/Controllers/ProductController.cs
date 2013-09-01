using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
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

        // Number of products per page 
        private int _pageSize = 4;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
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
        public ViewResult List(string category, int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = ProductRepository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
              
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        ProductRepository.Products.Count() :
                        ProductRepository.Products.Where(p => p.Category == category).Count()
                },

                CurrentCategory = category
            };

            return View(model);
        }

    }
}