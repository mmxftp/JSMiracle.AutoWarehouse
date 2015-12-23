using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.BaseWcfClient;
using JsMiracle.WCF.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.CB.ICoreBussiness
{
    public interface ILocationRelationship:IDataLayer<IMS_CB_WZGX>
    {
    }

    public class WcfClientLocationRelationship : WcfDalClient<IMS_CB_WZGX>, ILocationRelationship
    {
        const string ContactName = "ILocationRelationship";

        public WcfClientLocationRelationship()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }
    }
}
