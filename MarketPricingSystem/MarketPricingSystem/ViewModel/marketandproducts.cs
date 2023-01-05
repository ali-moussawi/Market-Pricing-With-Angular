using MarketPricingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketPricingSystem.ViewModel
{
    public class marketandproducts
    {
        public int marketid { get; set; }
        public List<Models.product> allproducts { get; set; }

    }
}