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

namespace JsMiracle.WCF.VC.IOrderForm
{
    /// <summary>
    /// 单据头
    /// </summary>
    public interface IOrderForm : IDataLayer<IMS_VC_DJT>
    {
    }


    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(OrderKnownTypesProvider))]
    public interface IWcfOrderForm : IWcfService
    {

    }

    public class WcfConfigOrderForm : WcfClientConfig<IMS_VC_DJT, IWcfOrderForm>, IOrderForm, IWcfOrderForm
    {
        const string ContactName = "IOrderForm";

        public WcfConfigOrderForm()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }
    }
}
