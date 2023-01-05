using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarketPricingSystem.Models;
using Org.BouncyCastle.Asn1.X509;
using MarketPricingSystem.security;
namespace MarketPricingSystem.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private EncryptandDecrypt encryptandDecrypt =new EncryptandDecrypt();
        private marketpricing _context;

        public UsersController()
        {
            _context = new marketpricing();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        public ActionResult Dashboard()
        {
            List<int> data = new List<int>();
            int supermarket = _context.supermarkets.Count();
            int products = _context.products.Count();
            int categories = _context.categories.Where(c=>c.categoryName !="NO CATEGORY").Count();
            int users = _context.users.Where(u => u.gmail !="admin@gmail.com").Count();

            data.Add(supermarket);
            data.Add(products);
            data.Add(categories);
            data.Add(users);




            return View(data);
        }

        public ActionResult Settings()
        {
            var gmail = User.Identity.Name.Split(',').First();
            user user = _context.users.FirstOrDefault(u => u.gmail == gmail);
            user.password = encryptandDecrypt.DecryptPassword(user.password);
            return View(user);
        }

        public ActionResult Updatesettings( int userid, string name, string phone, string newpassword)
        {



            var targetuser = _context.users.FirstOrDefault(u => u.UserId == userid);

           

            if(targetuser.phoneNumber != phone)
            {

                var uphonenb = _context.users.FirstOrDefault(u => u.phoneNumber == phone);
                if (uphonenb != null)
                {
                    TempData["Message1"] = "Phone number already in use";

                    return RedirectToAction("Settings");
                }


            }
           





            if (!String.IsNullOrWhiteSpace(newpassword) && !String.IsNullOrEmpty(newpassword))
            {
                targetuser.password = encryptandDecrypt.EncryptPassword(newpassword);
            }


            targetuser.Name = name;
            targetuser.phoneNumber = phone;
            _context.SaveChanges();


            return RedirectToAction("Dashboard");
        }

    }
}