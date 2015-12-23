using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.BaseWcfClient;
using JsMiracle.WCF.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.CM.ICommonMng
{
    public interface IUserObject : IDataLayer<IMS_CM_YHDX>
    {
    }


    public class WcfClientUserObject : WcfDalClient<IMS_CM_YHDX>, IUserObject
    {
        const string ContactName = "IUserObject";

        public WcfClientUserObject()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }
    }
}
