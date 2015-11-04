using JsMiracle.Dal.Abstract;
using JsMiracle.Dal.Concrete;
using JsMiracle.Entities;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JsMiracle.WebUI.Infrastructure
{
    public class NinjectControllerFactory:DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBinding();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {

            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBinding()
        {
            IIMS_ORGEntities ent = new IIMS_ORGEntities();
            ninjectKernel.Bind<IDataLayer<IMS_TB_UserInfo>>()
                .To<IMS_TB_UserInfo_Dal>().WithConstructorArgument(ent);

            ninjectKernel.Bind<IDataLayer<IMS_TB_Module>>()
                .To<IMS_TB_Module_Dal>().WithConstructorArgument(ent);

            ninjectKernel.Bind<IDataLayer<IMS_TB_ModuleFunction>>()
                .To<IMS_TB_ModuleFunction_Dal>().WithConstructorArgument(ent);
        }


    }


}