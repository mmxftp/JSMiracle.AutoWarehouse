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
    /// 单据头
    /// </summary>
    public interface IOrderForms : IDataLayer<IMS_VC_DJT>
    {
        /// <summary>
        /// 把订单状态改为下一状态，验证状态步骤是否正确可行
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="userid">操作人</param>
        /// <param name="nextState">下一状态</param>
        void UpdateOrder(long id, string userid, EnumOrderFormState nextState);

        /// <summary>
        /// 分页面视图的数据
        /// </summary>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="orderBy"></param>
        /// <param name="where"></param>
        /// <param name="whereParams"></param>
        /// <returns></returns>
        List<V_IMS_VC_DJT> GetDataViewByPageDynamic(int intPageIndex
            , int intPageSize
            , out int rowCount
            , string orderBy
            , string where
            , params object[] whereParams);
    }


    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(OrderKnownTypesProvider))]
    public interface IWcfOrderForm : IWcfService
    {

    }

    public class WcfConfigOrderForm : WcfClientConfig<IMS_VC_DJT, IWcfOrderForm>, IOrderForms
    {
        const string ContactName = "IOrderForm";

        public WcfConfigOrderForm()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

        public void UpdateOrder(long id, string userid, EnumOrderFormState nextState)
        {
            base.RequestAction<object[]>("UpdateOrder", new object[] { id, userid, nextState });
        }

        public List<V_IMS_VC_DJT> GetDataViewByPageDynamic(int intPageIndex
            , int intPageSize
            , out int rowCount
            , string orderBy
            , string where
            , params object[] whereParams)
        {
            object[] obj = new object[] { 
                intPageIndex,
                intPageSize,
                orderBy,
                where,
                whereParams
            };

            var data = base.RequestFunc<object[], object[]>("GetDataViewByPageDynamic", obj);

            rowCount = (int)data[0];
            return (List<V_IMS_VC_DJT>)data[1];
        }
    }
}
