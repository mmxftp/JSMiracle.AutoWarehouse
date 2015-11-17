using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JsMiracle.Dal.Abstract;
using JsMiracle.Dal.Concrete;
using JsMiracle.Entities;
using JsMiracle.Framework.Cache;
using JsMiracle.WebUI.CommonSupport;
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
                Component.For<IUser, IMembershipService>()
                .ImplementedBy<IMS_TB_UserInfo_Dal>().LifeStyle.PerWebRequest,

                Component.For<IModule>()
                .ImplementedBy<IMS_TB_Module_Dal>().LifeStyle.PerWebRequest,

                Component.For<IModuleFunction>()
                .ImplementedBy<IMS_TB_ModuleFunction_Dal>().LifeStyle.PerWebRequest,

                Component.For<IRole>()
                .ImplementedBy<IMS_TB_RoleInfo_Dal>().LifeStyle.PerWebRequest,

                Component.For<IPermission>()
                .ImplementedBy<IMS_TB_Permission_Dal>().LifeStyle.PerWebRequest,

                Component.For<IRoleUser>()
                .ImplementedBy<IMS_TB_RoleUser_Dal>().LifeStyle.PerWebRequest,

                Component.For<IFormsAuthentication>()
                .ImplementedBy<FormsAuthenticationService>().LifeStyle.PerWebRequest,

                Component.For<ICache>()
                .ImplementedBy<WebCache>().LifeStyle.PerWebRequest,

                Component.For<IActionPermission>()
                .ImplementedBy<ActionPermission>().LifeStyle.PerWebRequest,

                Classes.FromThisAssembly().BasedOn<IHttpController>().LifestyleTransient(),
                Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient()

                );
        }

    }

}