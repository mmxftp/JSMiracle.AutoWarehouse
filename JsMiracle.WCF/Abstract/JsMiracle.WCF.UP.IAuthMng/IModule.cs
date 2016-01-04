using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.BaseWcfClient;
using JsMiracle.WCF.Config;
using JsMiracle.WCF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JsMiracle.WCF.UP.IAuthMng
{
    /// <summary>
    /// 模块接口
    /// </summary>
    public interface IModule : IDataLayer<IMS_UP_MK>
    {
        /// <summary>
        /// 得所有根节点信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<IMS_UP_MK> GetRootModule();

        /// <summary>
        /// 得到模块实例
        /// </summary>
        /// <param name="moduleid">模块编号不可重覆</param>
        /// <returns></returns>
        [OperationContract]
        IMS_UP_MK GetEntityByModuleID(int moduleid);

        /// <summary>
        /// 根据父节点得子节点信息
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        [OperationContract]
        List<IMS_UP_MK> GetChildModuleList(int parentid);
    }



    //public class WcfClientModule : WcfDalClient<IMS_UP_MK>, IModule
    //{
    //    const string ContactName = "IModule";

    //    public WcfClientModule()
    //        : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
    //    { }


    //    public List<IMS_UP_MK> GetRootModule()
    //    {
    //        return
    //              WcfClient<IModule>.UseService<List<IMS_UP_MK>>(
    //              base.binding, base.remoteAddress,
    //              n => n.GetRootModule());
    //    }

    //    public IMS_UP_MK GetEntityByModuleID(int moduleid)
    //    {
    //        return
    //            WcfClient<IModule>.UseService<IMS_UP_MK>(
    //            base.binding, base.remoteAddress,
    //            n => n.GetEntityByModuleID(moduleid));
    //    }

    //    public List<IMS_UP_MK> GetChildModuleList(int parentid)
    //    {
    //        return
    //            WcfClient<IModule>.UseService<List<IMS_UP_MK>>(
    //            base.binding, base.remoteAddress,
    //            n => n.GetChildModuleList(parentid));
    //    }
    //}
    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(AuthKnownTypesProvider))]
    public interface IWcfModule : IWcfService
    {

    }

    public class WcfConfigModule : WcfClientConfig<IMS_UP_MK, IWcfModule>, IModule, IWcfModule
    {
        const string ContactName = "IModule";

        public WcfConfigModule()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

        public List<IMS_UP_MK> GetRootModule()
        {
            return RequestFunc<object[], List<IMS_UP_MK>>("GetRootModule", new object[]{null});
        }

        public IMS_UP_MK GetEntityByModuleID(int moduleid)
        {
            return RequestFunc<object[], IMS_UP_MK>("GetEntityByModuleID", new object[] { moduleid });
        }

        public List<IMS_UP_MK> GetChildModuleList(int parentid)
        {
            return RequestFunc<object[], List<IMS_UP_MK>>("GetChildModuleList", new object[] { parentid });
        }
    }
}
