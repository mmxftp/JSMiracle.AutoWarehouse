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
    public interface IOrderForm : IDataLayer<IMS_VC_DJT>
    {
        /// <summary>
        /// 把订单状态改为下一状态，验证状态步骤是否正确可行
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="nextState">下一状态</param>
        void UpdateOrder(long id, EnumOrderFormState nextState);

    }


    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(OrderKnownTypesProvider))]
    public interface IWcfOrderForm : IWcfService
    {

    }

    public class WcfConfigOrderForm : WcfClientConfig<IMS_VC_DJT, IWcfOrderForm>, IOrderForm
    {
        const string ContactName = "IOrderForm";

        public WcfConfigOrderForm()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

        public void UpdateOrder(long id, EnumOrderFormState nextState)
        {
            throw new NotImplementedException();
        }
    }
}
