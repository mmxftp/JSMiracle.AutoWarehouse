using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.WCF;
using JsMiracle.WCF.VC.IOrderForm;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.VC.OrderForm
{
    /// <summary>
    /// 单据行
    /// </summary>
    public class IMS_VC_DJH_Dal : WcfDataLayerBase<IMS_VC_DJH>, IOrderData
    {
        public List<IMS_VC_DJH> GetDataListByDJBH(string djbh)
        {
            string filter = string.Format(" djbh =\"{0}\" ", djbh);
            return base.GetAllEntitesByFilter(filter);
        }
    }


    public class IMS_VC_DJH_WCF : WcfDataServiceBase<IMS_VC_DJH>, IWcfOrderData
    {
        IMS_VC_DJH_Dal dal = new IMS_VC_DJH_Dal();

        protected override Entities.WCF.WcfResponse RequestFun(Entities.WCF.WcfRequest req)
        {
            var res = new WcfResponse();

            object[] objs;
            switch (req.Head.RequestMethodName)
            {
                case "GetDataListByDJBH":
                    objs = (object[])req.Body.Parameters;
                    res.Body.Data = dal.GetDataListByDJBH((string)objs[0]);
                    break;
                default:
                    return null;
            }
            return res;
        }

        protected override Entities.IDataLayer<IMS_VC_DJH> DataLayer
        {
            get { return dal; }
        }
    }
}
