using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JsMiracle.Dal.Abstract;
using JsMiracle.Dal.Concrete;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace JsMiracle.WebUI.Infrastructure
{

    public class DependencyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDataLayer<IMS_TB_UserInfo>>()
                .ImplementedBy<IMS_TB_UserInfo_Dal>().LifeStyle.PerWebRequest,

                Component.For<IDataLayer<IMS_TB_Module>>()
                .ImplementedBy<IMS_TB_Module_Dal>().LifeStyle.PerWebRequest,

                Component.For<IDataLayer<IMS_TB_ModuleFunction>>()
                .ImplementedBy<IMS_TB_ModuleFunction_Dal>().LifeStyle.PerWebRequest,

                Component.For<IDataLayer<IMS_TB_RoleInfo>>()
                .ImplementedBy<IMS_TB_RoleInfo_Dal>().LifeStyle.PerWebRequest,

                Component.For<IDataLayer<IMS_TB_Permission>>()
                .ImplementedBy<IMS_TB_Permission_Dal>().LifeStyle.PerWebRequest,

                Classes.FromThisAssembly().BasedOn<IHttpController>().LifestyleTransient(),
                Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient()

                );

        }
    }

}