using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MarketPricingSystem.Models;
namespace MarketPricingSystem.CustomAuthorization
{



    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PermissionAuthorization :AuthorizeAttribute
    {
         private marketpricing _context =new marketpricing();

        public string Roles { get; set; }

        public PermissionAuthorization(string Roles)
        {
            this.Roles = Roles;

        }
       
        


        override
        public void OnAuthorization(AuthorizationContext filterContext)
        {




            var userrole =filterContext.HttpContext.User.Identity.Name.Split(',')[1];

            var roleid = _context.roles.FirstOrDefault(r => r.RoleName == userrole).RoleId;
            var allpermissionsid = _context.rolepermissions.Where(rp => rp.RoleId == roleid).ToList();


            List<MarketPricingSystem.Models.permission> permissions = new List<MarketPricingSystem.Models.permission>();

            foreach (var target in allpermissionsid)
            {
                var targetid = target.PermissionId;

                permissions.Add(_context.permissions.FirstOrDefault(p => p.PermissionId == targetid));
            }
            //now we have all permissions of the user that have this role;

            var targetpermissions = permissions.FirstOrDefault(p => p.PermissionName == Roles);

            if (targetpermissions == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new

                {
                    controller = "Account",
                    action ="UnAuthorized"



                }));
            }
            
        }



    }
}