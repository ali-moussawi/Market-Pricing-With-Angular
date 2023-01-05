using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketPricingSystem.ViewModel
{
    public class productdetails
    {
        public int productid { get; set; }
        public string productname { get; set; }

        public int productBarcode { get; set; }
        public string productDescription { get; set; }

        public int productprice { get; set; }

        public string productcategory { get; set; }


    }
}