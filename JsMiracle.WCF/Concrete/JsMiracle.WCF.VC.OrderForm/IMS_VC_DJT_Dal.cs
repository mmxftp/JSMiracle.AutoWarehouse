using JsMiracle.Entities.Enum;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Framework;
using JsMiracle.WCF.VC.IOrderForm;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.VC.OrderForm
{
    /// <summary>
    /// 单据头
    /// </summary>
    public class IMS_VC_DJT_Dal : WcfDataLayerBase<IMS_VC_DJT>, JsMiracle.WCF.VC.IOrderForm.IOrderForm
    {
        public override void Delete(object id)
        {
            var orderForm = GetEntity(id);
            if (orderForm == null)
                return;

            var orderDataList = this.DbContext.IMS_VC_DJH_S.Where(n => n.DJBH == orderForm.DJBH);

            using (var tran = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    this.DbContext.IMS_VC_DJH_S.RemoveRange(orderDataList);
                    this.DbContext.IMS_VC_DJT_S.Remove(orderForm);
                    this.DbContext.SaveChanges();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception("删除订单数据失败", ex);
                }
            }
        }

        public void UpdateOrder(long id, Entities.Enum.EnumOrderFormState nextState)
        {
            var ent = GetEntity(id);

            var sts = JsMiracle.Framework.FunctionHelp.GetEnum<EnumOrderFormState>(ent.DJZT);
            var nextSts = OrderStateWorkFlow.GetNextOrderFormState(sts);

            if (nextState != nextSts)
                throw new JsMiracleException(string.Format("下一状态应是:{0},不是:{1}", nextSts,nextState));

            ent.DJZT = (int)nextState;


            var orderDataList = this.DbContext.IMS_VC_DJH_S.Where(n => n.DJBH == ent.DJBH);

            if (orderDataList != null)
            {
                foreach (var data in orderDataList)
                {
                    //data.ZT = 
                }
            }


            using (var tran = this.DbContext.Database.BeginTransaction())
            {
                try
                {


                    
                    tran.Commit();
                }
                catch(Exception ex)
                {
                    tran.Rollback();
                    throw new Exception("更新单据状态失败", ex);
                }
            }

        }


    }

    public class IMS_VC_DJT_WCF : WcfDataServiceBase<IMS_VC_DJT>, IWcfOrderForm
    {
        IMS_VC_DJT_Dal dal = new IMS_VC_DJT_Dal();

        protected override Entities.WCF.WcfResponse RequestFun(Entities.WCF.WcfRequest req)
        {
            return null;
        }

        protected override Entities.IDataLayer<IMS_VC_DJT> DataLayer
        {
            get { return dal; }
        }
    }

}
