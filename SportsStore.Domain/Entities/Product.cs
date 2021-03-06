﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue=false)]
        public int ProductID
        {
            get;
            set;
        }

        [Required(ErrorMessage="Please enter a product name")]
        public string Name
        {
            get;
            set;
        }

        [Required(ErrorMessage="Please enter a product description")]
        [DataType(DataType.MultilineText)]
        public string Description
        {
            get;
            set;
        }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage="Please enter a positive value")]
        public decimal Price
        {
            get;
            set;
        }

        [Required(ErrorMessage="Please specify a category")]
        public string Category
        {
            get;
            set;
        }
    }
}
