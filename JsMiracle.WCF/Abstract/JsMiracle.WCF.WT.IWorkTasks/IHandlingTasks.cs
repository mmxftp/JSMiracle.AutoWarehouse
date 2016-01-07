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
    /// 搬运类工作任务
    /// </summary>
    public interface IHandlingTasks : IDataLayer<IMS_WT_BYRW>
    {

    }

    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(TasksKnownTypesProvider))]
    public interface IWcfHandlingTasks : IWcfService
    {

    }

    public class WcfConfigHandlingTasks : WcfClientConfig<IMS_WT_BYRW, IWcfHandlingTasks>, IHandlingTasks, IWcfHandlingTasks
    {
        const string ContactName = "IHandlingTasks";

        public WcfConfigHandlingTasks()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

    }
}
