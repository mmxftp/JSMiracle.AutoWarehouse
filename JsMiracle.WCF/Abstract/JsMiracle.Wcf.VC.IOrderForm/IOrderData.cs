using JsMiracle.Entities;
using JsMiracle.Entities.Enum;
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
    /// 单据行
    /// </summary>
    public interface IOrderData : IDataLayer<IMS_VC_DJH>
    {
        /// <summary>
        /// 得到
        /// </summary>
        /// <param name="djbh">单据编号</param>
        /// <returns></returns>
        List<V_IMS_VC_DJH> GetDataListByDJBH(string djbh);


        /// <summary>
        /// 把订单行状态改为下一状态，验证状态步骤是否正确可行
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="userid">操作人</param>
        /// <param name="nextState">下一状态</param>
        void UpdateOrderData(long id,string userid, EnumOrderDataState nextState);
    }


    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(OrderKnownTypesProvider))]
    public interface IWcfOrderData : IWcfService
    {

    }

    public class WcfConfigOrderData : WcfClientConfig<IMS_VC_DJH, IWcfOrderData>, IOrderData
    {
        const string ContactName = "IOrderData";

        public WcfConfigOrderData()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }


        public List<V_IMS_VC_DJH> GetDataListByDJBH(string djbh)
        {
            return base.RequestFunc<object[], List<V_IMS_VC_DJH>>("GetDataListByDJBH", new object[] { djbh });
        }



        public void UpdateOrderData(long id, string userid, EnumOrderDataState nextState)
        {
            base.RequestAction<object[]>("UpdateOrderData", new object[] { id,userid, nextState });
        }


    }
}
