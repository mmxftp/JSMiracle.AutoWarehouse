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
    public interface IContainerType : IDataLayer<IMS_CB_RQLX>
    {
    }


    public class WcfClientContainerType : WcfDalClient<IMS_CB_RQLX>, IContainerType
    {
        const string ContactName = "IContainerType";

        public WcfClientContainerType()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }
    }
}
