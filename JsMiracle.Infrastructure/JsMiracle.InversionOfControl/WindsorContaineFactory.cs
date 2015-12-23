using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace JsMiracle.InversionOfControl
{
    /// <summary>
    /// 用于所有contain的缓存
    /// </summary>
    public static class WindsorContaineFactory
    {
        const string ApplicationContainerName = "container";

        public static IWindsorContainer GetContainer()
        {
            if (HttpContext.Current.Application[ApplicationContainerName] == null)
            {
                var containerByCon = new WindsorContainer().Install(new DependencyInstaller(MainAssembleName));

                HttpContext.Current.Application.Add(ApplicationContainerName, containerByCon);
            }

            return (IWindsorContainer)HttpContext.Current.Application[ApplicationContainerName];
        }

        public static string MainAssembleName
        {
            get;
            set;
        }

    }
}