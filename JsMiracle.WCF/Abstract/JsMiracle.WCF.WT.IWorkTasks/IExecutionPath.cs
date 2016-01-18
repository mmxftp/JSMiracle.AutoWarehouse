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
    /// 执行路径
    /// </summary>
    public interface IExecutionPath : IDataLayer<IMS_WT_ZXLJ>
    {

    }

    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(TasksKnownTypesProvider))]
    public interface IWcfExecutionPath : IWcfService
    {

    }

    public class WcfConfigExecutionPath : WcfClientConfig<IMS_WT_ZXLJ, IWcfExecutionPath>, IExecutionPath
    {
        const string ContactName = "IExecutionPath";

        public WcfConfigExecutionPath()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

    }
}
