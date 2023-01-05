using MarketPricingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarketPricingSystem.DAL;
using MarketPricingSystem.ViewModel;
using Microsoft.EntityFrameworkCore;
using MarketPricingSystem.CustomAuthorization;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace MarketPricingSystem.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {

        private RolesDal roleDal = new RolesDal();

        private marketpricing _context;

        public RolesController()
        {

            _context = new marketpricing();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        [PermissionAuthorization(Roles : "viewroles")]
        public ActionResult AllRoles()
        {

            var rolelist = _context.roles.Where(r => r.RoleName != "Admin").ToList();

            return View(rolelist);

        }






        public ActionResult Rolepermissions(int id)
        {
            IEnumerable<permission> permissions = roleDal.Rolepermission(id);

            return View(permissions);
        }









        [PermissionAuthorization(Roles : "createrole")]
        public ActionResult Createrole()
        {
            var allpermissions = _context.permissions.ToList();

            return View(allpermissions);
        }






        [PermissionAuthorization(Roles : "createrole")]
        [HttpPost]
        public ActionResult ConfirmCreate(string Data)
        {
            string[] dataarrived = Data.Split(',');



            List<string> usefuldata = dataarrived.ToList();

            var rolename = usefuldata[0];



            var checkdbrolename = _context.roles.FirstOrDefault(r => r.RoleName == rolename);
            if (checkdbrolename != null)
            {


                return Json(new { success = false, responseText = "Role " + rolename + " Already Exists" }, JsonRequestBehavior.AllowGet);
            }





            else
            {
                Models.role newrole = new Models.role();
                newrole.RoleName = rolename;

                _context.roles.Add(newrole);
                _context.SaveChanges();
                var newroleid = _context.roles.FirstOrDefault(r => r.RoleName == rolename).RoleId;


                usefuldata.RemoveAt(0);
                List<string> checkedpermissionsid = usefuldata;

                foreach (var perid in checkedpermissionsid)
                {


                    roleDal.addperTorole(newroleid, Convert.ToInt32(perid));

                }


                return Json(new { redirecturl = Url.Action("AllRoles", "Roles"), isRedirect = true, success = true, responseText = "Role " + rolename + " Added Succesfully" }, JsonRequestBehavior.AllowGet);



            }



        }




        [PermissionAuthorization(Roles : "updaterole")]
        public ActionResult Updaterole(int id)
        {


            roleandpermissionsandcheckedone data = new roleandpermissionsandcheckedone();

            data.role = _context.roles.FirstOrDefault(r => r.RoleId == id);
            data.permissions = _context.permissions.ToList();
            data.checkedpermissionsid = _context.rolepermissions.Where(r => r.RoleId == id).ToList();


            return View(data);

        }






        [PermissionAuthorization(Roles : "updaterole")]
        [HttpPost]
        public ActionResult Confirmupdate(string Data)
        {

            string[] dataarrived = Data.Split(',');



            List<string> usefuldata = dataarrived.ToList();

            var rolename = usefuldata[0];
            var roleid = usefuldata[1];
          

            var targetrole = _context.roles.FirstOrDefault(r => r.RoleId == Convert.ToInt32(roleid));
            if(targetrole.RoleName != rolename)
            {
                var checknm = _context.roles.FirstOrDefault(r => r.RoleName == rolename);
                if(checknm != null)
                {
                    return Json(new { success = false, responseText= "Role " + rolename + " already exists" }, JsonRequestBehavior.AllowGet);
                }



            }

                Models.role newrole = new Models.role();
                newrole.RoleName = rolename;
                
                _context.roles.Add(newrole);
                _context.SaveChanges();
            var newroleid = _context.roles.FirstOrDefault(r => r.RoleName == rolename && r.RoleId != Convert.ToInt32(roleid)).RoleId;

          
            if (usefuldata.Count > 2)
            {
                for(int i = 2;i<usefuldata.Count; i++)
                {
                    roleDal.addperTorole(newroleid, Convert.ToInt32(usefuldata.ElementAt(i)));

                }




            }



            var users = _context.users.Where(u => u.roleid == Convert.ToInt32(roleid)).ToList();

            if (users.Count() > 0)
            {
                foreach (var user in users)
                {
                    user.roleid = newroleid;
                    _context.SaveChanges();
                }

            }

            //remove old role
            var oldrole =_context.roles.FirstOrDefault(r => r.RoleId == Convert.ToInt32(roleid));
            _context.roles.Remove(oldrole);
            _context.SaveChanges();

                return Json(new { redirecturl = Url.Action("AllRoles", "Roles") ,isRedirect = true,success = true, responseText= "Role " + targetrole.RoleName + " updated Succesfully" }, JsonRequestBehavior.AllowGet);

            
        }










        }
}