using MarketPricingSystem.CustomAuthorization;
using MarketPricingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MarketPricingSystem.Controllers
{
    [Authorize]
    public class PermissionController : Controller
    {
        private marketpricing _context;

        public PermissionController()
        {
            _context = new marketpricing();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }




        [PermissionAuthorization(Roles :"viewpermissions")]
        public ActionResult Allpermissions()
        {

            var permissionlits = _context.permissions.ToList();

            return View(permissionlits);

        }
    }
}