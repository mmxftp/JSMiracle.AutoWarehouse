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
    /// 系统节点
    /// </summary>
    public interface ISystemNode : IDataLayer<IMS_WT_XTJD>
    {

    }

    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(TasksKnownTypesProvider))]
    public interface IWcfSystemNode : IWcfService
    {

    }

    public class WcfConfigSystemNode : WcfClientConfig<IMS_WT_XTJD, IWcfSystemNode>, ISystemNode, IWcfSystemNode
    {
        const string ContactName = "ISystemNode";

        public WcfConfigSystemNode()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

    }
}
