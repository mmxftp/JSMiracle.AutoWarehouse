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

namespace JsMiracle.Wcf.VC.IOrderForm
{
    /// <summary>
    /// 单据行
    /// </summary>
    public interface IOrderData : IDataLayer<IMS_VC_DJH>
    {
        /// <summary>
        /// 得到
        /// </summary>
        /// <param name="djbh">单据编号</param>
        /// <returns></returns>
        List<IMS_VC_DJH> GetDataListByDJBH(string djbh);

    }


    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(OrderKnownTypesProvider))]
    public interface IWcfOrderData : IWcfService
    {

    }

    public class WcfConfigOrderData : WcfClientConfig<IMS_VC_DJH, IWcfOrderData>, IOrderData, IWcfOrderData
    {
        const string ContactName = "IOrderData";

        public WcfConfigOrderData()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }


        public List<IMS_VC_DJH> GetDataListByDJBH(string djbh)
        {
            return base.RequestFunc<object[], List<IMS_VC_DJH>>("GetDataListByDJBH", new object[] { djbh });
        }

    }
}
