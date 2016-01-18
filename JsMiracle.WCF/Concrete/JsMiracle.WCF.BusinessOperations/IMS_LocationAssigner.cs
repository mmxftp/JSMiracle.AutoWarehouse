using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.WCF;
using JsMiracle.Framework;
using JsMiracle.WCF.CB.CoreBussiness;
using JsMiracle.WCF.IBusinessOperations;
using JsMiracle.WCF.VC.OrderForm;
using JsMiracle.WCF.WcfBaseService;
using JsMiracle.WCF.WT.WorkTasks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Transactions;


namespace JsMiracle.WCF.BusinessOperations
{
    public class IMS_LocationAssigner : ILocationAssigner
    {
        //private ORGModels _dbContext;

        //protected virtual ORGModels DbContext
        //{
        //    get
        //    {
        //        if (_dbContext == null)
        //            _dbContext = new ORGModels();

        //        return _dbContext;
        //    }
        //}

        public void SaveLocationAssigner(IMS_CB_KC kc, IMS_CB_RQ rq, IMS_WT_CWRW cwrw, IMS_VC_DJH djh)
        {
            IMS_CB_Container_Dal dalContainer = new IMS_CB_Container_Dal();
            IMS_WT_CWRW_Dal dalTask = new IMS_WT_CWRW_Dal();
            IMS_VC_DJH_Dal dalOrderData = new IMS_VC_DJH_Dal();
            IMS_CB_Inventory_Dal dalInventory = new IMS_CB_Inventory_Dal();

            if (dalInventory.Exists(string.Format(" RQBH == \"{0}\" ", rq.RQBH)))
                throw new JsMiracleException("容器编号已在库存中，请重新输入容器编号");

            using (var tran = new TransactionScope())
            {
                try
                {
                    dalContainer.SaveOrUpdate(rq);
                    dalInventory.SaveOrUpdate(kc);
                    dalTask.SaveOrUpdate(cwrw);
                    dalOrderData.SaveOrUpdate(djh);

                    tran.Complete();
                }
                catch (Exception ex)
                {
                    throw new Exception("组盘失败", ex);
                }
            }
        }
    }

    public class IMS_LocationAssigner_WCF : WcfServiceBase, IWcfLocationAssigner
    {

        protected override WcfResponse BaseRequest(Entities.WCF.WcfRequest req)
        {
            IMS_LocationAssigner dal = new IMS_LocationAssigner();


            var res = new WcfResponse();

            object[] objs;
            switch (req.Head.RequestMethodName)
            {
                case "SaveLocationAssigner":
                    objs = (object[])req.Body.Parameters;
                    IMS_CB_KC kc = (IMS_CB_KC)objs[0];
                    IMS_CB_RQ rq = (IMS_CB_RQ)objs[1];
                    IMS_WT_CWRW cwrw = (IMS_WT_CWRW)objs[2];
                    IMS_VC_DJH djh = (IMS_VC_DJH)objs[3];

                    dal.SaveLocationAssigner(kc, rq, cwrw, djh);
                    break;
            }

            return res;
        }

    }

}
