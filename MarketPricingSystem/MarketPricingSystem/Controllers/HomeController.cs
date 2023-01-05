using MarketPricingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using MySql.Data.MySqlClient;
using MarketPricingSystem.ViewModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Web.Security;
using MarketPricingSystem.DAL;
namespace MarketPricingSystem.Controllers
{
    
    public class HomeController : Controller
    {
      
        private marketpricing _context;

        public HomeController()
        {
            _context = new marketpricing();  
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }





        public ActionResult Index()
        {
            return View();
        }


        public ActionResult SuperMarketList()
        {
            var supermarketlist = _context.supermarkets.ToList();

            return View(supermarketlist);


        }

        public ActionResult Options()
        {

            return View();
        }


        public ActionResult ProductList(int id)//find list of product in supermarket (no productid)
        {
            productDal product = new productDal();

            List<productdetails> supermarketproducts = product.supermarketproductlist(id);

            return View(supermarketproducts);

            

        }

        public ActionResult Allproducts()//list of all priced product with productid that used to find cheapest and expensive products
        {
            productDal product= new productDal();

            List <ViewModel.product> allproducts = product.Allproducts();

            return View(allproducts);

        }

        public ActionResult GetCheapestproduct(int id) // this id is product id
        {
            productDal product = new productDal();
            List<productvalue> cheapestproducts = product.GetCheapestproduct(id);
                      
            return View(cheapestproducts);

        }


        public ActionResult GetExpensiveproduct(int id)
        {
            productDal product = new productDal();
            List<productvalue> topexpensivethree = product.GetExpensiveproduct(id); 
         
            return View(topexpensivethree);

        }


        public ActionResult SuperMarketCategories(int id)
        {


            return View();
        }


    }


}