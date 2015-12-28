
using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.WCF;
using JsMiracle.WCF.CM.ICommonMng;
using JsMiracle.WCF.Interface;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.CM.CommonMng
{
    public class IMS_CM_YHDX_Dal : WcfDataLayerBase<IMS_CM_YHDX>, IUserObject
    {
    }



    public class IMS_CM_YHDX_WCF : WcfService<IMS_CM_YHDX>, IWcfUserObject
    {
        IMS_CM_YHDX_Dal dal = new IMS_CM_YHDX_Dal();

        protected override WcfResponse RequestFun(WcfRequest req)
        {
            return null;
        }

        protected override IDataLayer<IMS_CM_YHDX> DataLayer
        {
            get { return dal; }
        }
    }
}
