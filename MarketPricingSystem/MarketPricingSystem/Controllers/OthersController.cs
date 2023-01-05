using MarketPricingSystem.DAL;
using MarketPricingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarketPricingSystem.ViewModel;
using MarketPricingSystem.security;
using MarketPricingSystem.CustomAuthorization;
using System.Web.Security;

namespace MarketPricingSystem.Controllers
{
    [Authorize]
    public class OthersController : Controller
    {
        private EncryptandDecrypt encryptandDecrypt = new EncryptandDecrypt();
        private otherDal _otherdal = new otherDal();

        

        private marketpricing _context;

        public OthersController()
        {

            _context = new marketpricing();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        [PermissionAuthorization(Roles : "viewusers")]
        public ActionResult Allusers()
        {

            List<Userdetails> users = _otherdal.allusers();




            return View(users);

        }



        [PermissionAuthorization(Roles : "updateuser")]
        public ActionResult Updateuser(int id)
        {
           Userandroles updateuser = new Userandroles();
            var targetuser = _context.users.FirstOrDefault(u => u.UserId == id);
            targetuser.password =encryptandDecrypt.DecryptPassword(targetuser.password);
            var rolelist = _context.roles.Where(r=> r.RoleName != "Admin").ToList();
            updateuser.User=targetuser;
            updateuser.Rolelist= rolelist;



            return View(updateuser);
        }


        [PermissionAuthorization(Roles : "updateuser")]
        public ActionResult Confirmupdate(int userid, string username, string phonenumber , string password, int roleid)
        {
            var targetuser = _context.users.FirstOrDefault(m => m.UserId == userid);



           

            if (!String.IsNullOrWhiteSpace(password) && !String.IsNullOrEmpty(password))
            {
                password = encryptandDecrypt.EncryptPassword(password);

                targetuser.password = password;
            }


            if (targetuser.phoneNumber != phonenumber)
            {
                var checknb = _context.users.FirstOrDefault(u => u.phoneNumber == phonenumber);

                if (checknb != null)
                {
                    TempData["Message1"] = "Phone number Already in use";

                    return RedirectToAction("Updateuser", new { @id= targetuser.UserId});
                }
            }

            targetuser.roleid = roleid;
            targetuser.Name = username;
            targetuser.phoneNumber = phonenumber;
            _context.SaveChanges();

            return RedirectToAction("Allusers");
        }






        [PermissionAuthorization(Roles : "deleteuser")]
        public ActionResult ConfirmDelete(int id)
        {
            var targetuser = _context.users.FirstOrDefault(m => m.UserId == id);

            _context.users.Remove(targetuser);
            _context.SaveChanges();

            return RedirectToAction("Allusers");
        }



        [PermissionAuthorization(Roles : "adduser")]
        public ActionResult Createuser()
        {
            var roleslist = _context.roles.Where(r=>r.RoleName != "Admin").ToList();
            

            return View(roleslist);
        }



        [PermissionAuthorization(Roles : "adduser")]
        public ActionResult Confirmcreate(string username ,string phonenumber, string gmail, string password,  int? roleid)
        {
           



            var chechnb = _context.users.FirstOrDefault(u => u.phoneNumber == phonenumber);
            if (chechnb != null)
            {
                TempData["Message1"] = "Phone number Already in use";
                return RedirectToAction("Createuser");
             


            }

            var chechgmail = _context.users.FirstOrDefault(u => u.gmail == gmail);
            if (chechgmail != null)
            {
                TempData["Message2"] = "Email Already in use";
                return RedirectToAction("Createuser");


            }
            if (roleid == null)
            {
                TempData["Message3"] = "Please select a role";
                return RedirectToAction("Createuser");
            }


            user newuser = new user();
            newuser.Name = username;
            newuser.phoneNumber = phonenumber;
            newuser.gmail = gmail;

            password = encryptandDecrypt.EncryptPassword(password);

            newuser.password = password;
            newuser.roleid = roleid.Value;
            _context.users.Add(newuser);
            _context.SaveChanges();



            return RedirectToAction("Allusers");
        }







    }
}