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
    public interface ILocationType:IDataLayer<IMS_CB_WZLX>
    {
    }

    public class WcfClientLocationType : WcfDalClient<IMS_CB_WZLX>, ILocationType
    {
        const string ContactName = "ILocationType";

        public WcfClientLocationType()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }
    }
}
