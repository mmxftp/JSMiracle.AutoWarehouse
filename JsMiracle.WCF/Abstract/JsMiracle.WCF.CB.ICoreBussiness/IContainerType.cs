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

namespace JsMiracle.WCF.CB.ICoreBussiness
{
    /// <summary>
    /// 容器类型
    /// </summary>
    public interface IContainerType : IDataLayer<IMS_CB_RQLX>
    {
    }

    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(CoreKnownTypesProvider))]
    public interface IWcfContainerType : IWcfService
    {

    }
    //public class WcfClientContainerType : WcfDalClient<IMS_CB_RQLX>, IContainerType
    //{
    //    const string ContactName = "IContainerType";

    //    public WcfClientContainerType()
    //        : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
    //    { }
    //}


    public class WcfConfigContainerType : WcfClientConfig<IMS_CB_RQLX, IWcfContainerType>, IContainerType
    {
        const string ContactName = "IContainerType";

        public WcfConfigContainerType()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

    }
}
