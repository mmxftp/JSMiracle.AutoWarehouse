﻿using JsMiracle.Entities.TabelEntities;
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
    /// 储位分配置
    /// </summary>
    public interface ILocationAssigner
    {
        void SaveLocationAssigner(IMS_CB_KC kc, IMS_CB_RQ rq, IMS_WT_CWRW cwrw, IMS_VC_DJH djh);

    }

    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(OrderKnownTypesProvider))]
    public interface IWcfLocationAssigner : IWcfService
    {

    }


    //public class WcfConfigOrderForm : WcfClientConfig<IMS_VC_DJT, IWcfOrderForm>, IOrderForm, IWcfOrderForm
    //{
    //    const string ContactName = "IOrderForm";

    //    public WcfConfigOrderForm()
    //        : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
    //    { }
    //}

    public class WcfClientLocationAssigner : WcfClient<IWcfLocationAssigner>, ILocationAssigner
    {
        const string ContactName = "ILocationAssigner";

        public WcfClientLocationAssigner()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        {

        }

        public void SaveLocationAssigner(IMS_CB_KC kc, IMS_CB_RQ rq, IMS_WT_CWRW cwrw, IMS_VC_DJH djh)
        {
            base.RequestAction<object[]>("SaveLocationAssigner", new object[] { kc, rq, cwrw, djh });

        }
    }
}
