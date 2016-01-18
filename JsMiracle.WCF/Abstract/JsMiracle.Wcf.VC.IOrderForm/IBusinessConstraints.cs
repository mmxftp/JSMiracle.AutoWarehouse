using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.VC.IOrderForm;
using JsMiracle.WCF.BaseWcfClient;
using JsMiracle.WCF.Config;
using JsMiracle.WCF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JsMiracle.WCF.VC.IOrderForm
{
    /// <summary>
    /// 业务约束
    /// </summary>
    public interface IBusinessConstraints : IDataLayer<IMS_VC_YWYS>
    {

    }


    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(OrderKnownTypesProvider))]
    public interface IWcfBusinessConstraints : IWcfService
    {

    }

    public class WcfConfigBusinessConstraints : WcfClientConfig<IMS_VC_YWYS, IWcfBusinessConstraints>, IBusinessConstraints
    {
        const string ContactName = "IBusinessConstraints";

        public WcfConfigBusinessConstraints()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }
    }
}
