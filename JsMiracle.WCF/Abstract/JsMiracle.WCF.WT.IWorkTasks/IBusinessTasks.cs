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
    /// 业务类工作任务
    /// </summary>
    public interface IBusinessTasks : IDataLayer<IMS_WT_YWRW>
    {

    }

    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(TasksKnownTypesProvider))]
    public interface IWcfBusinessTasks : IWcfService
    {

    }

    public class WcfConfigBusinessTasks : WcfClientConfig<IMS_WT_YWRW, IWcfBusinessTasks>, IBusinessTasks, IWcfBusinessTasks
    {
        const string ContactName = "IBusinessTasks";

        public WcfConfigBusinessTasks()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

    }
}
