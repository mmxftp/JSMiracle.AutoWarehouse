using Castle.Windsor;
using JsMiracle.WebUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JsMiracle.WebUI
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //API注入包
            //GlobalConfiguration.Configuration.Services.Replace(
            //    typeof(IHttpControllerActivator),
            //    new WindsorActivator(this.containerByApi));
            //controller 注入包
            var controllerFactory = new WindsorControllerFactory(WindsorContaineFactory.GetContainer().Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

        }
    }
}