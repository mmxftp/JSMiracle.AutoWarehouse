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

namespace JsMiracle.WCF.CM.ICommonMng
{
    /// <summary>
    /// IMS_CM_YHDX
    /// </summary>
    public interface IUserObject : IDataLayer<IMS_CM_YHDX>
    {
    }


    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(CommonKnownTypesProvider))]
    public interface IWcfUserObject : IWcfService
    {

    }

    //public class WcfClientUserObject : WcfDalClient<IMS_CM_YHDX>, IUserObject
    //{
    //    const string ContactName = "IUserObject";

    //    public WcfClientUserObject()
    //        : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
    //    { }
    //}



    public class WcfConfigUserObject : WcfClientConfig<IMS_CM_YHDX, IWcfUserObject>, IUserObject
    {
        const string ContactName = "IUserObject";

        public WcfConfigUserObject()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }
    }
}
