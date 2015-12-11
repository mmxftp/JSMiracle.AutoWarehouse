using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JsMiracle.Dal.Abstract.CB;
using JsMiracle.Dal.Abstract.CM;
using JsMiracle.Dal.Abstract.UP;
using JsMiracle.Dal.Concrete;
using JsMiracle.Dal.Concrete.CB;
using JsMiracle.Dal.Concrete.CM;
using JsMiracle.Dal.Concrete.UP;
using JsMiracle.Framework.Cache;
using JsMiracle.WebUI.CommonSupport;
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
                .ImplementedBy<IMS_UP_Permission_Dal>().LifeStyle.PerWebRequest,

                Component.For<IRoleUser>()
                .ImplementedBy<IMS_UP_RoUr_Dal>().LifeStyle.PerWebRequest,

                Component.For<IFormsAuthentication>()
                .ImplementedBy<FormsAuthenticationService>().LifeStyle.PerWebRequest,

                Component.For<ICache>()
                .ImplementedBy<WebCache>().LifeStyle.PerWebRequest,

                Component.For<IActionPermission>()
                .ImplementedBy<ActionPermission>().LifeStyle.PerWebRequest,

                Component.For<IItem>()
                     .ImplementedBy<IMS_CB_Item_Dal>().LifeStyle.PerWebRequest,

                Component.For<ICode>()
                     .ImplementedBy<IMS_CM_Code_Dal>().LifeStyle.PerWebRequest,

                Component.For<ICodeType>()
                     .ImplementedBy<IMS_CM_CodeType_Dal>().LifeStyle.PerWebRequest,

                Component.For<IContainerType>()
                     .ImplementedBy<IMS_CB_ContainerType_Dal>().LifeStyle.PerWebRequest,

                Component.For<IContainer>()
                     .ImplementedBy<IMS_CB_Container_Dal>().LifeStyle.PerWebRequest,

                Component.For<ILocation>()
                     .ImplementedBy<IMS_CB_Location_Dal>().LifeStyle.PerWebRequest,

                Component.For<ILocationType>()
                     .ImplementedBy<IMS_CB_LocationType_Dal>().LifeStyle.PerWebRequest,

                Component.For<ILocationRelationship>()
                     .ImplementedBy<IMS_CB_LocationRelationship_Dal>().LifeStyle.PerWebRequest,

                Component.For<IObjectDataDictionary>()
                    .ImplementedBy<IMS_CM_DXXX_Dal>().LifeStyle.PerWebRequest,

                Component.For<IUserObject>()
                    .ImplementedBy<IMS_CM_YHDX_Dal>().LifeStyle.PerWebRequest,

                Classes.FromThisAssembly().BasedOn<IHttpController>().LifestyleTransient(),
                Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient()

                );


        }

    }

}