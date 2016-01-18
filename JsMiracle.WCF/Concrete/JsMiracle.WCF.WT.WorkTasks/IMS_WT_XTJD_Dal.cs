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
    /// 系统节点
    /// </summary>
    public class IMS_WT_XTJD_Dal : WcfDataLayerBase<IMS_WT_XTJD>, ISystemNode
    {
 
    }


    public class IMS_WT_XTJD_WCF : WcfDataServiceBase<IMS_WT_XTJD>, IWcfSystemNode
    {
        IMS_WT_XTJD_Dal dal = new IMS_WT_XTJD_Dal();
        protected override Entities.WCF.WcfResponse RequestFun(Entities.WCF.WcfRequest req)
        {
            return null;
        }

        protected override Entities.IDataLayer<IMS_WT_XTJD> DataLayer
        {
            get { return dal; }
        }
    }
}
