﻿using Castle.MicroKernel.Registration;
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
                .ImplementedBy<IMS_UP_User_Dal>().LifeStyle.PerWebRequest,

                Component.For<IModule>()
                .ImplementedBy<IMS_UP_Modu_Dal>().LifeStyle.PerWebRequest,

                Component.For<IModuleFunction>()
                .ImplementedBy<IMS_UP_MoFn_Dal>().LifeStyle.PerWebRequest,

                Component.For<IRole>()
                .ImplementedBy<IMS_UP_Role_Dal>().LifeStyle.PerWebRequest,

                Component.For<IPermission>()
                .ImplementedBy<IMS_UP_RoMo_Dal>().LifeStyle.PerWebRequest,

                Component.For<IRoleUser>()
                .ImplementedBy<IMS_UP_RoUr_Dal>().LifeStyle.PerWebRequest,

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