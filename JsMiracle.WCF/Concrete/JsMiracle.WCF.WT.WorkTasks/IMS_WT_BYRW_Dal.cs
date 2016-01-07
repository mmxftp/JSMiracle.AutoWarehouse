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
    /// 搬运类工作任务
    /// </summary>
    public class IMS_WT_BYRW_Dal : WcfDataLayerBase<IMS_WT_BYRW>, IHandlingTasks
    {
 
    }


    public class IMS_WT_BYRW_WCF : WcfService<IMS_WT_BYRW>, IWcfHandlingTasks
    {
        IMS_WT_BYRW_Dal dal = new IMS_WT_BYRW_Dal();
        protected override Entities.WCF.WcfResponse RequestFun(Entities.WCF.WcfRequest req)
        {
            return null;
        }

        protected override Entities.IDataLayer<IMS_WT_BYRW> DataLayer
        {
            get { return dal; }
        }
    }
}
