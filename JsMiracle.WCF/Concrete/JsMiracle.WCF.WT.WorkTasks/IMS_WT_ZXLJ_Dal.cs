using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.WCF;
using JsMiracle.WCF.WcfBaseService;
using JsMiracle.WCF.WT.IWorkTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.WT.WorkTasks
{
    /// <summary>
    /// 执行路径
    /// </summary>
    public class IMS_WT_ZXLJ_Dal : WcfDataLayerBase<IMS_WT_ZXLJ>, IExecutionPath
    {
 
    }


    public class IMS_WT_ZXLJ_WCF : WcfDataServiceBase<IMS_WT_ZXLJ>, IWcfExecutionPath
    {
        IMS_WT_ZXLJ_Dal dal = new IMS_WT_ZXLJ_Dal();
        protected override Entities.WCF.WcfResponse RequestFun(Entities.WCF.WcfRequest req)
        {
            return null;
        }

        protected override Entities.IDataLayer<IMS_WT_ZXLJ> DataLayer
        {
            get { return dal; }
        }
    }
}
