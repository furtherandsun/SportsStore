using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Models
{
    /// <summary>
    /// View model for the List action method in the Product controller
    /// </summary>
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products
        {
            get;
            set;
        }

        public PagingInfo PagingInfo
        {
            get;
            set;
        }

        public string CurrentCategory
        {
            get;
            set;
        }
    }
}