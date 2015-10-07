using Microsoft.Web.WebPages.OAuth;
using MvcDemo.Filters;
using MvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;


namespace MvcDemo.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the Student Finance Support system";

            return View();
        }

        public ActionResult siteMap()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = " These are the main features of the completley new SFS system";

            return View();
        }
        
        public ActionResult adminOnly()
        {
            ViewBag.Message = "This is the admin only page";
            using (var ctx = new SFS())
            {
                return View(ctx.UserProfiles.ToList());
            }
            
        }
       
 
        public ActionResult GetUsers()
        {
            var users = Membership.GetAllUsers();
            return View(users);
        }

        public ActionResult AdminError()
        {
            ViewBag.Message = "You need to be an admin too access this page";

            return View();
        }

    }
}
