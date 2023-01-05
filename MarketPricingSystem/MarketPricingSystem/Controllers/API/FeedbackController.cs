
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using MarketPricingSystem.Models;
using Microsoft.Ajax.Utilities;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace MarketPricingSystem.Controllers.API
{
    [EnableCors(origins: " http://localhost:4200", headers: "*", methods: "*")] 

    public class FeedbackController : ApiController
    {
        private marketpricing db = new marketpricing();

        [HttpGet]
       public IHttpActionResult Addmessage(string name, string email, string message)
        {


            message a = new message();
            a.name = name;
            a.email = email;
            a.message1 = message;
            db.messages.Add(a);
            db.SaveChanges();

            return Ok("ok");
          
        }

        [HttpGet]
        public IHttpActionResult getmessages(int Id)
        {
            List<MarketPricingSystem.Models.message> messages = db.messages.ToList();
            return Json(messages);
          
              


        }




    }
}