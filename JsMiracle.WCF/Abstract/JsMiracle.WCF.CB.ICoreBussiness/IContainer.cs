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
    /// <summary>
    /// 容器类型
    /// </summary>
    public interface IContainer : IDataLayer<IMS_CB_RQ>
    {

    }

    public class WcfClientContainer : WcfDalClient<IMS_CB_RQ>, IContainer
    {
        const string ContactName = "IContainer";

        public WcfClientContainer()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }
    }


}
