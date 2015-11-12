using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JsMiracle.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            //System.Web.Security.MembershipProvider
            //System.Web.Profile.SqlProfileProvider
        }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Login(string user, string password, string returnUrl)
        //{

        //}

    }
}
