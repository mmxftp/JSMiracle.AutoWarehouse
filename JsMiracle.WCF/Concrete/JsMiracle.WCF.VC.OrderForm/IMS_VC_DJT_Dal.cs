using JsMiracle.Entities.TabelEntities;
using JsMiracle.Wcf.VC.IOrderForm;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.VC.OrderForm
{
    public class IMS_VC_DJT_Dal : WcfDataLayerBase<IMS_VC_DJT>, IOrderForm
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
    }

    public class IMS_VC_DJT_WCF : WcfService<IMS_VC_DJT>, IWcfOrderForm
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
