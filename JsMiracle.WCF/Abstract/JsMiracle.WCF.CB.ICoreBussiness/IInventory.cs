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
    /// <summary>
    /// 库存信息
    /// </summary>
    public interface IInventory: IDataLayer<IMS_CB_KC>
    {
    }


    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(CoreKnownTypesProvider))]
    public interface IWcfInventory : IWcfService
    {

    }


    public class WcfConfigInventory : WcfClientConfig<IMS_CB_KC, IWcfInventory>, IInventory
    {
        const string ContactName = "IInventory";

        public WcfConfigInventory()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

    
    }
}
