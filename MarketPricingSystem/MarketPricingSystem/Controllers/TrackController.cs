using MarketPricingSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarketPricingSystem.ViewModel;
using System.Web.UI.WebControls;
using MarketPricingSystem.Models;

namespace MarketPricingSystem.Controllers
{
    public class TrackController : Controller
    {

        private marketpricing _context = new Models.marketpricing();
        private TrackDal track = new TrackDal();



        public ActionResult Graph(int marketid, int productid)
        {
            List<int> mp = new List<int>();
            mp.Add(marketid);
            mp.Add(productid);
            
            return View(mp);



                
        }


        public ActionResult Allsupermarkets()
        {

            var allsupermarkets = _context.supermarkets.ToList();
            return View(allsupermarkets);
        }

        public ActionResult Supermarketproducts(int id)
        {
            productDal product = new productDal();
            List<productdetails> supermarketproducts = product.supermarketproductlist(id);



            marketidwithproducts marketidwithproducts = new marketidwithproducts();
            marketidwithproducts.marketid = id;
            marketidwithproducts.supermarketproducts = supermarketproducts;

            return View(marketidwithproducts);

        }


        [HttpPost]
        public ActionResult Fetchdata(int marketid, int productid)
        {


            Track data = new Track();
            data.data = track.productprices(marketid,productid);

            data.data = data.data.Remove(data.data.Length - 1);

            return Json(new { success = true, data = data.data }, JsonRequestBehavior.AllowGet);

        }
    



    }
}