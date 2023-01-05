using MarketPricingSystem.CustomAuthorization;
using MarketPricingSystem.DAL;
using MarketPricingSystem.Models;
using MarketPricingSystem.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketPricingSystem.Controllers
{
    [Authorize]
    public class SupermarketController : Controller
    {

        private marketpricing _context;

        public SupermarketController()
        {
            _context = new marketpricing();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }




        [PermissionAuthorization(Roles : "viewsupermarket")]
        public ActionResult Allsupermarkets()
        {

            var supermarketlist = _context.supermarkets.ToList();

            return View(supermarketlist);

        }




        [PermissionAuthorization(Roles : "deletesupermarket")]
        public ActionResult ConfirmDelete(int id)
        {
            var targetsupermarket = _context.supermarkets.FirstOrDefault(m => m.supermarketId == id);

            _context.supermarkets.Remove(targetsupermarket);
            _context.SaveChanges();

            return RedirectToAction("Allsupermarkets");
        }

        [PermissionAuthorization(Roles : "updatesupermarket")]
        public ActionResult Updatesupermarket(int id)
        {
            var targetsupermarket = _context.supermarkets.FirstOrDefault(m => m.supermarketId == id);
            


            return View(targetsupermarket);
        }


        [PermissionAuthorization(Roles : "updatesupermarket")]
        [HttpPost]
        public ActionResult Confirmupdate(int Supermarketid, string MarketName, string MarketRegion, string MarketDescription, string MarketNumber)
        {
            var targetsupermarket = _context.supermarkets.FirstOrDefault(m => m.supermarketId == Supermarketid);
            targetsupermarket.supermarketDescription = MarketDescription;
            _context.SaveChanges();


            if (targetsupermarket.supermarketName!=MarketName && targetsupermarket.supermarketRegion!=MarketRegion) {
                var checkdbmarketname_region = _context.supermarkets.FirstOrDefault(p => p.supermarketName == MarketName && p.supermarketRegion == MarketRegion);


                if (checkdbmarketname_region != null)
                {
                    TempData["Message1"] = "Supermarket name and Region Already Exists";



                    return RedirectToAction("Updatesupermarket", new { @id = Supermarketid });

                }

            }

            if (targetsupermarket.phonenumber != MarketNumber)
            {
                var numberdb = _context.supermarkets.FirstOrDefault(m => m.phonenumber == MarketNumber);

                if (numberdb != null)
                {

                     TempData["Message2"] = "Phone number already taken";

                    return RedirectToAction("Updatesupermarket", new { @id = Supermarketid });
                }

            }

            targetsupermarket.supermarketName = MarketName;
            targetsupermarket.supermarketRegion = MarketRegion;
            targetsupermarket.supermarketDescription = MarketDescription;
            targetsupermarket.phonenumber = MarketNumber;
            _context.SaveChanges();



            var supermarketlist = _context.supermarkets.ToList();
            return RedirectToAction("Allsupermarkets",supermarketlist);
        }


        [PermissionAuthorization(Roles : "addsupermarket")]
        public ActionResult Createsupermarket()
        {

            return View();
        }



        [PermissionAuthorization(Roles : "addsupermarket")]
        [HttpPost]
        public ActionResult ConfirmCreate(string MarketName, string MarketRegion, string MarketDescription, string MarketNumber)
        {



            var checkdbmarketname_region = _context.supermarkets.FirstOrDefault(p => p.supermarketName == MarketName && p.supermarketRegion == MarketRegion);


            if (checkdbmarketname_region != null)
            {

                TempData["Message1"]  = "Supermarket name and Region Already Exists";


               
                return RedirectToAction("Createsupermarket");
                
            }

            var marketnumber = _context.supermarkets.FirstOrDefault(p => p.phonenumber == MarketNumber);


            if (marketnumber != null)
            {
                TempData["Message2"] = "Phone number already taken";



                return RedirectToAction("Createsupermarket");
            }







            supermarket market = new supermarket();
        
            market.supermarketName = MarketName;
            market.supermarketRegion = MarketRegion;
            market.phonenumber = MarketNumber;
            market.supermarketDescription = MarketDescription;
            _context.supermarkets.Add(market);
            _context.SaveChanges();
            var supermarketlist = _context.supermarkets.ToList();
            return RedirectToAction("Allsupermarkets", supermarketlist);
        }


    }
}