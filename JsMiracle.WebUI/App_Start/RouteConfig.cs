using JsMiracle.WebUI.CommonSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JsMiracle.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var defaultAction = new { controller = "Account", action = "LogIn", id = UrlParameter.Optional };

            if (CurrentUser.IsAdmin)
                defaultAction = new { controller = "Home", action = "Index", id = UrlParameter.Optional };


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: defaultAction
                //defaults: new { controller = "Account", action = "LogIn", id = UrlParameter.Optional }
            );

        }
    }
}