using MarketPricingSystem.Models;
using MarketPricingSystem.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace MarketPricingSystem.Controllers
{
    
    public class AccountController : Controller
    {
         
        private EncryptandDecrypt encryptandDecrypt = new EncryptandDecrypt();
        private marketpricing _context;

        public AccountController()
        {
            _context = new marketpricing();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Unauthorized()
        {
            return View();
        }





        [HttpPost]
        public ActionResult Validatesignin(string Gmail, string Password)
        {
            Password = encryptandDecrypt.EncryptPassword(Password);

            var userindb = _context.users.Where(x => x.gmail == Gmail && x.password == Password).FirstOrDefault();

            if (userindb != null)
            {
                var userid = userindb.UserId;
         
                var roleid = _context.users.Where(x => x.UserId == userid).FirstOrDefault().roleid;

                var rolename = _context.roles.Where(x => x.RoleId == roleid).FirstOrDefault().RoleName.ToLower();

                FormsAuthentication.SetAuthCookie(userindb.gmail.ToString() + "," + rolename.ToString(), false);


                //add user permissions to cookie

                var permissionsid = _context.rolepermissions.Where(pr => pr.RoleId == roleid).ToList();

                List<string> permissions = new List<string>();

                foreach(var pid in permissionsid)
                {
                    permissions.Add(_context.permissions.FirstOrDefault(p=>p.PermissionId == pid.PermissionId).PermissionName);

                }


                string allpermissions="";

                foreach(var perm in permissions)
                {

                    allpermissions += perm + ",";
                }

                HttpCookie sameSiteCookie = new HttpCookie("Permissions");



                sameSiteCookie.Value = encryptandDecrypt.EncryptPassword(allpermissions);
                sameSiteCookie.Secure = true;
                //add cookie to cookie collection
                Response.Cookies.Add(sameSiteCookie);


                return RedirectToAction("Dashboard", "Users");
            }

            ModelState.AddModelError("", "Username or password is wrong ");
            return View("signin");


        }


        public ActionResult Signout()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

    }
}