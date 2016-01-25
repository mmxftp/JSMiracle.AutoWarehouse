using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JsMiracle.Framework.Cache;
using JsMiracle.Framework.FormAuth;
using JsMiracle.Framework.Log;
using JsMiracle.WCF.VC.IOrderForm;
using JsMiracle.WCF.CB.ICoreBussiness;
using JsMiracle.WCF.CM.ICommonMng;
using JsMiracle.WCF.UP.IAuthMng;
using JsMiracle.WCF.WT.IWorkTasks;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace JsMiracle.InversionOfControl
{

    public class DependencyInstaller : IWindsorInstaller
    {
        readonly string mainAssemble;

        public DependencyInstaller(string mainAssembleName)
        {
            mainAssemble = mainAssembleName;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                
                #region JsMiracle.WCF.UP.IAuthMng
                Component.For<IUser>()
                .ImplementedBy<WcfConfigUser>().LifeStyle.PerWebRequest,

                Component.For<IMembershipService>()
                .ImplementedBy<WcfConfigMembershipService>().LifeStyle.PerWebRequest,

                Component.For<IModule>()
                .ImplementedBy<WcfConfigModule>().LifeStyle.PerWebRequest,

                Component.For<IModuleFunction>()
                .ImplementedBy<WcfConfigModuleFunction>().LifeStyle.PerWebRequest,

                Component.For<IRole>()
                .ImplementedBy<WcfConfigRole>().LifeStyle.PerWebRequest,

                Component.For<IPermission>()
                .ImplementedBy<WcfConfigPermission>().LifeStyle.PerWebRequest,

                Component.For<IRoleUser>()
                .ImplementedBy<WcfConfigRoleUser>().LifeStyle.PerWebRequest,

                Component.For<IFormsAuthentication>()
                .ImplementedBy<FormsAuthenticationService>().LifeStyle.PerWebRequest,
 
                #endregion

                //Component.For<IActionPermission>()
                //.ImplementedBy<ActionPermission>().LifeStyle.PerWebRequest,

                #region JsMiracle.WCF.CB.ICoreBussiness

                Component.For<IItem>()
                     .ImplementedBy<WcfConfigItem>().LifeStyle.PerWebRequest,

                Component.For<ICode>()
                     .ImplementedBy<WcfConfigCode>().LifeStyle.PerWebRequest,

                Component.For<ICodeType>()
                     .ImplementedBy<WcfConfigCodeType>().LifeStyle.PerWebRequest,

                Component.For<IContainerType>()
                     .ImplementedBy<WcfConfigContainerType>().LifeStyle.PerWebRequest,

                Component.For<IContainer>()
                     .ImplementedBy<WcfConfigContainer>().LifeStyle.PerWebRequest,

                Component.For<ILocation>()
                     .ImplementedBy<WcfConfigLocation>().LifeStyle.PerWebRequest,

                Component.For<ILocationType>()
                     .ImplementedBy<WcfConfigLocationType>().LifeStyle.PerWebRequest,

                Component.For<ILocationRelationship>()
                     .ImplementedBy<WcfConfigLocationRelationship>().LifeStyle.PerWebRequest,
                #endregion

                #region JsMiracle.WCF.CM.ICommonMng
                Component.For<IObjectDataDictionary>()
                    .ImplementedBy<WcfConfigObjectDataDictionary>().LifeStyle.PerWebRequest,

                Component.For<IUserObject>()
                    .ImplementedBy<WcfConfigUserObject>().LifeStyle.PerWebRequest,
                #endregion

                #region JsMiracle.WCF.VC.IOrderForm
                Component.For<IOrderForms>()
                    .ImplementedBy<WcfConfigOrderForm>().LifeStyle.PerWebRequest,

                Component.For<IOrderData>()
                    .ImplementedBy<WcfConfigOrderData>().LifeStyle.PerWebRequest,

                Component.For<IBusinessConstraints>()
                    .ImplementedBy<WcfConfigBusinessConstraints>().LifeStyle.PerWebRequest,
                #endregion

                #region JsMiracle.WCF.WT.IWorkTasks
                Component.For<IHandlingTasks>()
                    .ImplementedBy<WcfConfigHandlingTasks>().LifeStyle.PerWebRequest,

                Component.For<IActionTasks>()
                    .ImplementedBy<WcfConfigActionTasks>().LifeStyle.PerWebRequest,

                Component.For<IBusinessTasks>()
                    .ImplementedBy<WcfConfigBusinessTasks>().LifeStyle.PerWebRequest,

                Component.For<IExecutionPath>()
                    .ImplementedBy<WcfConfigExecutionPath>().LifeStyle.PerWebRequest,

                Component.For<IExecutor>()
                    .ImplementedBy<WcfConfigExecutor>().LifeStyle.PerWebRequest,

                Component.For<IOperationalTasks>()
                    .ImplementedBy<WcfConfigOperationalTasks>().LifeStyle.PerWebRequest,

                Component.For<ISystemNode>()
                    .ImplementedBy<WcfConfigSystemNode>().LifeStyle.PerWebRequest,
            #endregion

                Component.For<ICache>()
                    .ImplementedBy<WebCache>().LifeStyle.PerWebRequest,

                Component.For<JsMiracle.Framework.Log.ILog>()
                    .ImplementedBy<Net4Log>().LifeStyle.PerWebRequest,

                //Classes.FromThisAssembly().BasedOn<IHttpController>().LifestyleTransient(),
                //Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient()

                Classes.FromAssemblyNamed(mainAssemble).BasedOn<IHttpController>().LifestyleTransient(),
                Classes.FromAssemblyNamed(mainAssemble).BasedOn<IController>().LifestyleTransient()


                );


        }

    }

}