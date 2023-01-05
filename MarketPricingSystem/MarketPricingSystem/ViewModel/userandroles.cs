using MarketPricingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketPricingSystem.ViewModel
{
    public class Userandroles
    {


        public user    User { get; set; }

        public IEnumerable<MarketPricingSystem.Models.role> Rolelist { get; set; }
    }
}