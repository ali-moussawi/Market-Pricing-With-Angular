using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarketPricingSystem.CustomAuthorization;
using System.Web.Security;
using MarketPricingSystem.DAL;
using MarketPricingSystem.Models;
using MarketPricingSystem.ViewModel;
using Microsoft.EntityFrameworkCore.Internal;

namespace MarketPricingSystem.Controllers
{
    [Authorize]
    public class SupermarketproductsController : Controller
    {
        private marketpricing _context;

        public SupermarketproductsController()
        {
            _context = new marketpricing();

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [PermissionAuthorization(Roles : "viewsp")]
        public ActionResult Allsupermarkets()
        {
            var allsupermarkets = _context.supermarkets.ToList();
            return View(allsupermarkets);
        }


        [PermissionAuthorization(Roles : "viewspp")]
        public ActionResult Supermarketproducts(int id)
        {
            productDal product = new productDal();
            List<productdetails> supermarketproducts = product.supermarketproductlist(id);



            marketidwithproducts marketidwithproducts = new marketidwithproducts();
            marketidwithproducts.marketid = id;
            marketidwithproducts.supermarketproducts = supermarketproducts;

            return View(marketidwithproducts);

        }


        [PermissionAuthorization(Roles : "insertproducts")]
        public ActionResult Createproduct(int id)
        {
            marketandproducts marketandproducts = new marketandproducts();

            var targetsupermarktetid = _context.supermarkets.FirstOrDefault(m => m.supermarketId == id).supermarketId;

            var allproducts = _context.products.ToList();

            var oldproductsadded = _context.productprices.Where(p => p.supermarketid == id && p.IsActivePrice == 0).ToList();

            List<int> productidtodelete = new List<int>();

            for (int i = 0; i < allproducts.Count(); i++)
            {
                for (int j = 0; j < oldproductsadded.Count(); j++)
                {
                    if (allproducts[i].productId == oldproductsadded[j].productId)
                    {
                        productidtodelete.Add(allproducts[i].productId);

                    }

                }

            }

            for (int i = 0; i < productidtodelete.Count(); i++)
            {

                for (int j = 0; j < allproducts.Count(); j++)
                {
                    if (allproducts[j].productId == productidtodelete[i])
                    {
                        allproducts.RemoveAt(j);
                    }
                }


            }
            marketandproducts.marketid = targetsupermarktetid;
            marketandproducts.allproducts = allproducts;


            return View(marketandproducts);

        }


        [PermissionAuthorization(Roles : "insertproducts")]
        public ActionResult Confirmcreate(int marketid, int? productid, int price)
        {
            if(productid == null)
            {
               
                    TempData["Message3"] = "Please select a product";
                    return RedirectToAction("Createproduct",new { @id = marketid});
                }
            
         
            
                productprice newproductprice = new productprice();
                newproductprice.productId = productid.Value;
                newproductprice.supermarketid = marketid;
                newproductprice.price = price;
                newproductprice.date = DateTime.Now;
                newproductprice.IsActivePrice = 0;
                _context.productprices.Add(newproductprice);
                _context.SaveChanges();

                return RedirectToAction("Supermarketproducts", "Supermarketproducts", new { @id = marketid });


            }




        [PermissionAuthorization(Roles : "updateproductprice")]
        public ActionResult Updateproductprice(int marketid, int productid)
        {
            var targetproduct = _context.productprices.FirstOrDefault(p => p.supermarketid == marketid && p.productId == productid && p.IsActivePrice == 0);

            return View(targetproduct);
        }





        [PermissionAuthorization(Roles : "updateproductprice")]
        public ActionResult Confirmupdate(int marketid, int productid, int price)
        {

            var checksameprice = _context.productprices.FirstOrDefault(p => p.supermarketid == marketid && p.productId == productid && p.IsActivePrice == 0);

            if (checksameprice.price == price)
            {

                return RedirectToAction("Supermarketproducts", "Supermarketproducts", new { @id = marketid });

            }
            else
            {
                checksameprice.IsActivePrice = 1;
                _context.SaveChanges();

                productprice newproductprice = new productprice();
                newproductprice.productId = productid;
                newproductprice.supermarketid = marketid;
                newproductprice.price = price;
                newproductprice.date = DateTime.Now;
                newproductprice.IsActivePrice = 0;
                _context.productprices.Add(newproductprice);
                _context.SaveChanges();

                return RedirectToAction("Supermarketproducts", "Supermarketproducts", new { @id = marketid });


            }
        }



        [PermissionAuthorization(Roles :"deleteproductpriced")]
        public ActionResult ConfirmDelete(int marketid, int productid)
        {
            var targetpricedproduct = _context.productprices.FirstOrDefault(p => p.supermarketid == marketid && p.productId == productid && p.IsActivePrice == 0);

            targetpricedproduct.IsActivePrice = 1;
            _context.SaveChanges();

            return RedirectToAction("Supermarketproducts", "Supermarketproducts", new { @id = marketid });


        }


    }
}