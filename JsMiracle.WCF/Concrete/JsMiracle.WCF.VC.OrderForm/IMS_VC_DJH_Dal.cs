using JsMiracle.Entities.Enum;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.WCF;
using JsMiracle.Framework;
using JsMiracle.WCF.VC.IOrderForm;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.VC.OrderForm
{
    /// <summary>
    /// 单据行
    /// </summary>
    public class IMS_VC_DJH_Dal : WcfDataLayerBase<IMS_VC_DJH>, IOrderData
    {
        public List<V_IMS_VC_DJH> GetDataListByDJBH(string djbh)
        {
            var data = base.DbContext.V_IMS_VC_DJH_S.Where(n => n.DJBH == djbh);
            var list = data.ToList();

            foreach (var d in list)
            {
                OrderStateWorkFlow.SetOrderDataProp(d);
            }

            return list;
        }


        public void UpdateOrderData(long id, string userid, EnumOrderDataState nextState)
        {
            var ent = GetEntity(id);

            var currentState = FunctionHelp.GetEnum<EnumOrderDataState>(ent.ZT);
            if (!OrderStateWorkFlow.ContainNextOrderDataState(currentState, nextState))
                throw new JsMiracleException(string.Format("当前状态:{0},下一状态不得是:{1}", currentState, nextState));

            ent.ZT = (int)nextState;

            // 单据释放时需要把单据行信息加入业务任务中　
            IMS_WT_YWRW bizTask = null;
            if (nextState == EnumOrderDataState.VLSTS_Released)
            {
                bizTask = DbContext.IMS_WT_YWRW_S.Where(n => n.DJH_ID == ent.ID).FirstOrDefault();

                if (bizTask == null)
                {
                    bizTask = new IMS_WT_YWRW()
                    {
                        DJBH = ent.DJBH,
                        CJR = userid,
                        DJH_ID = ent.ID,
                        CJSJ = System.DateTime.Now,
                        DJSL = ent.DJSL,
                        HXW = ent.HXW,
                        HH = ent.HH,
                        LYD = ent.LYD,
                        LYSYZ = ent.LYSYZ,
                        MDD = ent.MDD,
                        MDSYZ = ent.MDSYZ,
                        PCBH = ent.PCBH,
                        SKU = ent.SKU,
                        ZT = (int)EnumWorkTaskState.WTSTS_New,
                    };
                }
                else
                {
                    bizTask.LYD = ent.LYD;
                    bizTask.LYSYZ = ent.LYSYZ;
                    bizTask.MDSYZ = ent.MDSYZ;
                    bizTask.MDD = ent.MDD;
                    bizTask.PCBH = ent.PCBH;
                    bizTask.SKU = ent.SKU;
                }
            }

            using (var tran = new System.Transactions.TransactionScope())
            {
                try
                {
                    if (bizTask != null)
                    {
                        if (bizTask.ID == 0){
                            DbContext.IMS_WT_YWRW_S.Add(bizTask);
                        }

                        DbContext.SaveChanges();
                    }

                    SaveOrUpdate(ent);

                    tran.Complete();
                }
                catch(Exception ex)
                {
                    throw new JsMiracleException(string.Format("单据更新失败:{0}",ex.Message));
                }
            }

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

                case "UpdateOrderData":
                    objs = (object[])req.Body.Parameters;
                    dal.UpdateOrderData((long)objs[0],(string)objs[1],(EnumOrderDataState)objs[2]);
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
