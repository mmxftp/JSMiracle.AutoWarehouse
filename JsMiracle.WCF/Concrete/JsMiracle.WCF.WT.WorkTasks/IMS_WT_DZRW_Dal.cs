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
    /// 动作执行类工作任务
    /// </summary>
    public class IMS_WT_DZRW_Dal : WcfDataLayerBase<IMS_WT_DZRW>, IActionTasks
    {
 
    }


    public class IMS_WT_DZRW_WCF : WcfDataServiceBase<IMS_WT_DZRW>, IWcfActionTasks
    {
        IMS_WT_DZRW_Dal dal = new IMS_WT_DZRW_Dal();
        protected override Entities.WCF.WcfResponse RequestFun(Entities.WCF.WcfRequest req)
        {
            return null;
        }

        protected override Entities.IDataLayer<IMS_WT_DZRW> DataLayer
        {
            get { return dal; }
        }
    }
}
