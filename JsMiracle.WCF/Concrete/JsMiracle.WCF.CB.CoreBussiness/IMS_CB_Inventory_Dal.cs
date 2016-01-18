using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.WCF;
using JsMiracle.WCF.CB.ICoreBussiness;
using JsMiracle.WCF.Interface;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.CB.CoreBussiness
{
    /// <summary>
    /// 库存
    /// </summary>
    public class IMS_CB_Inventory_Dal : WcfDataLayerBase<IMS_CB_KC>, IInventory
    {
    }


    public class IMS_CB_Inventory_WCF : WcfDataServiceBase<IMS_CB_KC>, IWcfInventory
    {
        IMS_CB_Inventory_Dal dal = new IMS_CB_Inventory_Dal();

        protected override WcfResponse RequestFun(WcfRequest req)
        {
            WcfResponse res = new WcfResponse();
            switch (req.Head.RequestMethodName)
            {
                default:
                    return null;
            }

            res.Head.IsSuccess = true;
            return res;
        }

        protected override IDataLayer<IMS_CB_KC> DataLayer
        {
            get { return dal; }
        }
    }
}
