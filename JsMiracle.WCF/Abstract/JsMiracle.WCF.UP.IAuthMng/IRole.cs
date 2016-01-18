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
    /// 角色信息接口
    /// </summary>.
    public interface IRole : IDataLayer<IMS_UP_JS>
    {
        /// <summary>
        /// 得所有角色
        /// </summary>
        /// <returns></returns>0
        [OperationContract]
        List<IMS_UP_JS> GetAllRole();
    }

    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(AuthKnownTypesProvider))]
    public interface IWcfRole : IWcfService
    {

    }


    //public class WcfClientRole : WcfDalClient<IMS_UP_JS>, IRole
    //{
    //    const string ContactName = "IRole";

    //    public WcfClientRole()
    //        : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
    //    { }


    //    public List<IMS_UP_JS> GetAllRole()
    //    {
    //        return
    //                WcfClient<IRole>.UseService<List<IMS_UP_JS>>(
    //                base.binding, base.remoteAddress,
    //                n => n.GetAllRole());
    //    }

    //}

    public class WcfConfigRole : WcfClientConfig<IMS_UP_JS, IWcfRole>, IRole
    {
        const string ContactName = "IRole";

        public WcfConfigRole()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

        public List<IMS_UP_JS> GetAllRole()
        {
            return RequestFunc<object[], List<IMS_UP_JS>>("GetAllRole", new object[] { null });
        }
    }
}
