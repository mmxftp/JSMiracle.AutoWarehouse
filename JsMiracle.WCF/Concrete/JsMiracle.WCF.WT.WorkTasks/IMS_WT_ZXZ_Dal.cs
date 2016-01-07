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
    /// 执行者
    /// </summary>
    public class IMS_WT_ZXZ_Dal : WcfDataLayerBase<IMS_WT_ZXZ>, IExecutor
    {
 
    }


    public class IMS_WT_ZXZ_WCF : WcfService<IMS_WT_ZXZ>, IWcfExecutor
    {
        IMS_WT_ZXZ_Dal dal = new IMS_WT_ZXZ_Dal();
        protected override Entities.WCF.WcfResponse RequestFun(Entities.WCF.WcfRequest req)
        {
            return null;
        }

        protected override Entities.IDataLayer<IMS_WT_ZXZ> DataLayer
        {
            get { return dal; }
        }
    }
}
