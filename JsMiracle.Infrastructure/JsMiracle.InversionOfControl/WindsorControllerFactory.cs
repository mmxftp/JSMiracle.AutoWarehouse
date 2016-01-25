using Castle.MicroKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JsMiracle.InversionOfControl
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            kernel.ReleaseComponent(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {            
            if (controllerType == null)
            {
                //return null;
                //if (requestContext.HttpContext.Request.Path.Contains("favicon.ico"))
                //    return null;

                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }

            return (IController)kernel.Resolve(controllerType);
        }


        public static WindsorControllerFactory GetControllerFactory()
        {
            return new WindsorControllerFactory(WindsorContaineFactory.GetContainer().Kernel);
        }

    }

}