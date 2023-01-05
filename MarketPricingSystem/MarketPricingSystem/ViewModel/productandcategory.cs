using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MarketPricingSystem.ViewModel;
using MarketPricingSystem.Models;
namespace MarketPricingSystem.ViewModel
{
    public class productandcategory
    {
       public Models.product Product { get; set; }
       public IEnumerable<category> Categories { get; set; }

    }
}