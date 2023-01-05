using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MarketPricingSystem.Models;

namespace MarketPricingSystem.ViewModel
{
    public class roleandpermissionsandcheckedone
    {
        public MarketPricingSystem.Models.role role { get; set; } //target role

        public List<permission> permissions { get; set; }//all permissions

        public List<rolepermission> checkedpermissionsid { get; set; } //ids of permissions where roleid = roleid

    }
}