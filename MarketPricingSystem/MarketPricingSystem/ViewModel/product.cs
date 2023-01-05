using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketPricingSystem.ViewModel
{
    public class product
    {
        //this needed becuase we need to use product id to find some data like cheapest price so we need to pass id to next page
        public int productid { get; set; }
        public string productname { get; set; }

        public int productBarcode { get; set; }
        public string productDescription { get; set; }
        public string productcategory { get; set; }
    }
}