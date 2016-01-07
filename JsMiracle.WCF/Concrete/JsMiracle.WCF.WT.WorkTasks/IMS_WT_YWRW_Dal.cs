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
    /// 业务类工作任务
    /// </summary>
    public class IMS_WT_YWRW_Dal : WcfDataLayerBase<IMS_WT_YWRW>, IBusinessTasks
    {
 
    }


    public class IMS_WT_YWRW_WCF : WcfService<IMS_WT_YWRW>, IWcfBusinessTasks
    {
        IMS_WT_YWRW_Dal dal = new IMS_WT_YWRW_Dal();
        protected override Entities.WCF.WcfResponse RequestFun(Entities.WCF.WcfRequest req)
        {
            return null;
        }

        protected override Entities.IDataLayer<IMS_WT_YWRW> DataLayer
        {
            get { return dal; }
        }
    }
}
