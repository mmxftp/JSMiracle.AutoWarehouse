using JsMiracle.InversionOfControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Config/log4net.config")));
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
            // 得当前程序集名称
            WindsorContaineFactory.MainAssembleName = Assembly.GetExecutingAssembly().GetName().Name;

            var controllerFactory = new WindsorControllerFactory(WindsorContaineFactory.GetContainer().Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

        }
    }
}