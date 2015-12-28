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
    public interface ILocationType:IDataLayer<IMS_CB_WZLX>
    {
    }

    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(CoreKnownTypesProvider))]
    public interface IWcfLocationType : IWcfService
    {

    }

    //public class WcfClientLocationType : WcfDalClient<IMS_CB_WZLX>, ILocationType
    //{
    //    const string ContactName = "ILocationType";

    //    public WcfClientLocationType()
    //        : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
    //    { }
    //}

    public class WcfConfigLocationType : WcfClientConfig<IMS_CB_WZLX, IWcfLocationType>, ILocationType
    {
        const string ContactName = "ILocationType";

        public WcfConfigLocationType()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }
    }
}
