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
    /// 动作执行类工作任务
    /// </summary>
    public interface IActionTasks : IDataLayer<IMS_WT_DZRW>
    {

    }

    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(TasksKnownTypesProvider))]
    public interface IWcfActionTasks : IWcfService
    {

    }

    public class WcfConfigActionTasks : WcfClientConfig<IMS_WT_DZRW, IWcfActionTasks>, IActionTasks
    {
        const string ContactName = "IActionTask";

        public WcfConfigActionTasks()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

    }
}
