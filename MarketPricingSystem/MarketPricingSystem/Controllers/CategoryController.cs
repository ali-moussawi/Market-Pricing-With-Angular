using MarketPricingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarketPricingSystem.DAL;
using MarketPricingSystem.CustomAuthorization;
using System.Web.Security;

namespace MarketPricingSystem.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {

        private CategoryDAL _categoryDAL = new CategoryDAL();

        private marketpricing _context;

        public CategoryController()
        {
            _context = new marketpricing();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        [PermissionAuthorization(Roles : "viewcategory")]
        public ActionResult AllCategories() { 

            var categoryList = _context.categories.Where(c=>c.categoryName != "NO CATEGORY").ToList();

            return View(categoryList);

        }


        [PermissionAuthorization(Roles : "deletecategory")]
        public ActionResult ConfirmDelete(int id)
        {
            _categoryDAL.setproductnocategory(id);


            var targetcategory = _context.categories.FirstOrDefault(m => m.categoryId == id);

            _context.categories.Remove(targetcategory);
            _context.SaveChanges();

            return RedirectToAction("AllCategories");
        }



        [PermissionAuthorization(Roles : "updatecategory")]
        public ActionResult Updatecategory(int id)
        {
            var targetcategory = _context.categories.FirstOrDefault(m => m.categoryId == id);



            return View(targetcategory);
        }


        [PermissionAuthorization(Roles : "updatecategory")]
        [HttpPost]
        public ActionResult Confirmupdate(int Categoryid, string Categoryname)
        {
            var targetcategory = _context.categories.FirstOrDefault(m => m.categoryId == Categoryid);


            if (targetcategory.categoryName != Categoryname)
            {


                var checkdb = _context.categories.FirstOrDefault(c => c.categoryName == Categoryname);
                if (checkdb != null)
                {
                    TempData["Message1"] = "Category already exists";

                    return RedirectToAction("Updatecategory", new {@id = targetcategory.categoryId });

                }

            }
          

           
                targetcategory.categoryName = Categoryname;
                _context.SaveChanges();

                var categorylist = _context.categories.ToList();
                return RedirectToAction("AllCategories", categorylist);    
        }




        [PermissionAuthorization(Roles : "addcategory")]
        public ActionResult Createcategory()
        {

            return View();
        }

        [PermissionAuthorization(Roles : "addcategory")]
        [HttpPost]
        public ActionResult ConfirmCreate(string CategoryName)
        {

            var checkdb =_context.categories.FirstOrDefault(c => c.categoryName == CategoryName);   
            if(checkdb != null)
            {
                TempData["Message1"] = "Category already exists";
                return RedirectToAction("Createcategory");
                    
            }



            category category = new category();
            category.categoryName = CategoryName;

            _context.categories.Add(category);
            _context.SaveChanges();
            var categorylist = _context.categories.ToList();
            return RedirectToAction("AllCategories", categorylist);
        }












    }
}