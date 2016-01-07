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

namespace JsMiracle.WCF.WT.IWorkTasks
{
    /// <summary>
    /// 执行者
    /// </summary>
    public interface IExecutor : IDataLayer<IMS_WT_ZXZ>
    {

    }

    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(TasksKnownTypesProvider))]
    public interface IWcfExecutor : IWcfService
    {

    }

    public class WcfConfigExecutor : WcfClientConfig<IMS_WT_ZXZ, IWcfExecutor>, IExecutor, IWcfExecutor
    {
        const string ContactName = "IExecutor";

        public WcfConfigExecutor()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

    }
}
