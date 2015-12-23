using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JsMiracle.Framework.Cache;
using JsMiracle.Framework.Log;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace JsMiracle.WebAPI.Infrastructure
{

    public class DependencyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(

                Component.For<ICache>()
                .ImplementedBy<WebCache>().LifeStyle.PerWebRequest,
                
                Component.For<JsMiracle.Framework.Log.ILog>()
                .ImplementedBy<Net4Log>().LifeStyle.PerWebRequest,

                Classes.FromThisAssembly().BasedOn<IHttpController>().LifestyleTransient(),
                Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient()

                );


        }

    }

}