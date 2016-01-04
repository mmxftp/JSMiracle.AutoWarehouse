using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.VC.IOrderForm;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.VC.OrderForm
{   
    public class IMS_VC_YWYS_Dal : WcfDataLayerBase<IMS_VC_YWYS>, IBusinessConstraints
    {

    }




    public class IMS_VC_YWYS_WCF : WcfService<IMS_VC_YWYS>, IWcfBusinessConstraints
    {
        IMS_VC_YWYS_Dal dal = new IMS_VC_YWYS_Dal();

        protected override Entities.WCF.WcfResponse RequestFun(Entities.WCF.WcfRequest req)
        {
            return null;
        }

        protected override Entities.IDataLayer<IMS_VC_YWYS> DataLayer
        {
            get { return dal; }
        }

    }
}
