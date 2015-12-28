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
    public interface ILocationRelationship:IDataLayer<IMS_CB_WZGX>
    {
    }

    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(CoreKnownTypesProvider))]
    public interface IWcfLocationRelationship : IWcfService
    {

    }

    //public class WcfClientLocationRelationship : WcfDalClient<IMS_CB_WZGX, IWcfLocationRelationship>, ILocationRelationship
    //{
    //    const string ContactName = "ILocationRelationship";

    //    public WcfClientLocationRelationship()
    //        : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
    //    { }
    //}


    public class WcfConfigLocationRelationship : WcfClientConfig<IMS_CB_WZGX, IWcfLocationRelationship>, ILocationRelationship
    {
        const string ContactName = "ILocationRelationship";

        public WcfConfigLocationRelationship()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }
    }
}
