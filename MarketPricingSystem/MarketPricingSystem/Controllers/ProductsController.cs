using MarketPricingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarketPricingSystem.DAL;
using MarketPricingSystem.ViewModel;
using MarketPricingSystem.CustomAuthorization;
using System.Web.Security;

namespace MarketPricingSystem.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private marketpricing _context;
        private productDal productDal = new productDal();

        public ProductsController()
        {
            _context = new marketpricing();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        [PermissionAuthorization(Roles : "viewproduct")]
        public ActionResult AllProducts()
        {
           
            List < ViewModel.product > pdlist= productDal.ProductList();
          

            return View(pdlist);

        }




        [PermissionAuthorization(Roles : "deleteproduct")]
        public ActionResult ConfirmDelete(int id)
        {
            var targetproduct = _context.products.FirstOrDefault(m => m.productId == id);

            _context.products.Remove(targetproduct);
            _context.SaveChanges();

            return RedirectToAction("AllProducts");
        }



        [PermissionAuthorization(Roles : "updateproduct")]
        public ActionResult Updateproduct(int id)
        {
            var targetproduct = _context.products.FirstOrDefault(m => m.productId == id);
            var allcategories = _context.categories.Where(c => c.categoryName != "NO CATEGORY").ToList();

            productandcategory productandcategory = new productandcategory
            {
                Product = targetproduct,
                Categories = allcategories,
            };
            
           
          


            return View(productandcategory);
        }



        [PermissionAuthorization(Roles : "updateproduct")]
        [HttpPost]
        public ActionResult Confirmupdate(int Productid, string productname, int Barcode, string ProductDescription, int? Categoryid)
        {
            var targetproduct = _context.products.FirstOrDefault(p => p.productId == Productid);


            if (targetproduct.productName != productname)
            {
                var checkdbproductname = _context.products.FirstOrDefault(p => p.productName == productname);

                if (checkdbproductname != null)
                {
                    TempData["Message1"] = "Product name already taken";

                    return RedirectToAction("Updateproduct", new { @id = targetproduct.productId });
                }
            }

            if (targetproduct.BarcodeNb != Barcode)
            {
                var checkdbproductbr = _context.products.FirstOrDefault(p => p.BarcodeNb == Barcode);

                if (checkdbproductbr != null)
                {
                    TempData["Message2"] = "Barcode Already Exists";

                    return RedirectToAction("Updateproduct", new { @id = targetproduct.productId });

                }
            }
            if (Categoryid == null)
            {

                TempData["Message3"] = "Please select a category";

                return RedirectToAction("Updateproduct", new { @id = targetproduct.productId });

            }

            targetproduct.productName = productname;
            targetproduct.BarcodeNb=Barcode;
            targetproduct.productDescription = ProductDescription;
            targetproduct.categoryId = Categoryid.Value;
            _context.SaveChanges();


            return RedirectToAction("AllProducts");
        }



        [PermissionAuthorization( Roles :"addproduct")]
        public ActionResult Createproduct()
        {
            var allcategories = _context.categories.Where(c => c.categoryName != "NO CATEGORY").ToList();


            return View(allcategories);
        }


        [PermissionAuthorization(Roles : "addproduct")]
        [HttpPost]
        public ActionResult ConfirmCreate(string productname, int Barcode, string ProductDescription, int? Categoryid)
        {
         


            var checkdbproductname = _context.products.FirstOrDefault(p => p.productName == productname);
            if (checkdbproductname != null)
            {
                TempData["Message1"] = "Product name already taken";
                var allcategories = _context.categories.Where(c => c.categoryName != "NO CATEGORY").ToList();
                return RedirectToAction("Createproduct", allcategories);
            }



            var checkdbproductbr = _context.products.FirstOrDefault(p => p.BarcodeNb == Barcode);

            if (checkdbproductbr != null)
            {
                var allcategories = _context.categories.Where(c => c.categoryName != "NO CATEGORY").ToList();

                TempData["Message2"] = "Barcode Already Exists";

                return RedirectToAction("Createproduct", allcategories);
            }

            if (Categoryid == null)
            {

                TempData["Message3"] = "Please select a category";

                return RedirectToAction("Createproduct");

            }

                Models.product product = new Models.product();

                product.productName = productname;
                product.BarcodeNb = Barcode;
                product.productDescription = ProductDescription;
                product.categoryId = Categoryid.Value;
                _context.products.Add(product);
                _context.SaveChanges();

                List<ViewModel.product> pdlist = productDal.ProductList();



                return RedirectToAction("AllProducts", pdlist);
            

         
        }

    }
}