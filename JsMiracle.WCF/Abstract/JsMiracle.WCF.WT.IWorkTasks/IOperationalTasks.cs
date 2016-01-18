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
    /// 操作类工作任务
    /// </summary>
    public interface IOperationalTasks : IDataLayer<IMS_WT_CWRW>
    {
        ///// <summary>
        ///// 入库组盘
        ///// </summary>
        ///// <param name="itemId">物料编号</param>
        ///// <param name="number">数量</param>
        //void CombinedPallet(string itemId, int number);
    }

    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(TasksKnownTypesProvider))]
    public interface IWcfOperationalTasks : IWcfService
    {

    }

    public class WcfConfigOperationalTasks : WcfClientConfig<IMS_WT_CWRW, IWcfOperationalTasks>, IOperationalTasks
    {
        const string ContactName = "IOperationalTasks";

        public WcfConfigOperationalTasks()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

     
    }
}
