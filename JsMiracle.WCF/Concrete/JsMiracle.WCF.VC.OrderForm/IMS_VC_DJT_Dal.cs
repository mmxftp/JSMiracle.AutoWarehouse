using JsMiracle.Entities.Enum;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Framework;
using JsMiracle.WCF.VC.IOrderForm;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using JsMiracle.Entities.WCF;
using System.Data.Entity;

namespace JsMiracle.WCF.VC.OrderForm
{
    /// <summary>
    /// 单据头
    /// </summary>
    public class IMS_VC_DJT_Dal : WcfDataLayerBase<IMS_VC_DJT>, IOrderForms
    {
        protected override bool IsAddEntity(IMS_VC_DJT entity)
        {
            var isAdd = base.IsAddEntity(entity);

            if (isAdd)
            {
                if (this.DbContext.IMS_VC_DJT_S.Any(n => n.DJBH == entity.DJBH))
                    throw new JsMiracleException("单据编号({0})已被使用不得重覆");
            }
            else
            {
                if (this.DbContext.IMS_VC_DJT_S.Any(n => n.DJBH == entity.DJBH && n.ID != entity.ID))
                    throw new JsMiracleException("单据编号({0})已被使用不得重覆");
            }

            return isAdd;
        }


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

        public override IMS_VC_DJT GetEntity(object id)
        {
            var entity = base.GetEntity(id);

            if (entity == null)
                return entity;


            var currentState = FunctionHelp.GetEnum<EnumOrderFormState>(entity.DJZT);
            var canStatus=  OrderStateWorkFlow.CanUseNextStepOrderFormStateList(currentState);

            //  下一步骤可用状态是修改过提交则，为可修改状态
            if (canStatus.Exists(n => n == EnumOrderFormState.VHSTS_Ready || n == EnumOrderFormState.VHSTS_Submit))
                entity.CanModify = true;
            else
                entity.CanModify = false;

            return entity;
        }

        public void UpdateOrder(long id, string userid, EnumOrderFormState nextState)
        {
            var ent = GetEntity(id);

            var currentState = JsMiracle.Framework.FunctionHelp.GetEnum<EnumOrderFormState>(ent.DJZT);
            if (!OrderStateWorkFlow.ContainNextOrderFormState(currentState, nextState))
                throw new JsMiracleException(string.Format("当前状态:{0},下一状态不得是:{1}", currentState, nextState));

            ent.DJZT = (int)nextState;

            var orderDataState = OrderStateWorkFlow.GetOrderDataStateByOrderFormState(nextState);

            var orderDataList = this.DbContext.IMS_VC_DJH_S.Where(n => n.DJBH == ent.DJBH);

            if (orderDataList != null && orderDataState != EnumOrderDataState.None)
            {
                foreach (var data in orderDataList)
                {
                    data.ZT = (int)orderDataState;
                }
            }


            using (var tran = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    DbContext.SaveChanges();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception("更新单据状态失败", ex);
                }
            }
        }


        public virtual List<V_IMS_VC_DJT> GetDataViewByPageDynamic(int intPageIndex
            , int intPageSize
            , out int rowCount
            , string orderBy
            , string where
            , params object[] whereParams)
        {
            IQueryable<V_IMS_VC_DJT> query = null;

            if (!string.IsNullOrEmpty(where))
            {
                query = DbContext.Set<V_IMS_VC_DJT>()
                    .Where(where, whereParams)
                    .OrderBy(orderBy)
                    .Skip((intPageIndex - 1) * intPageSize)
                    .Take(intPageSize);

                rowCount = DbContext.Set<V_IMS_VC_DJT>().Where(where, whereParams).Count();
            }
            else
            {
                query = DbContext.Set<V_IMS_VC_DJT>()
                    .OrderBy(orderBy)
                    .Skip((intPageIndex - 1) * intPageSize)
                    .Take(intPageSize);

                rowCount = DbContext.Set<V_IMS_VC_DJT>().Count();
            }

            var list = query.ToList();

            foreach (var d in list)
            {
                OrderStateWorkFlow.SetOrderFormProp(d);
            }

            return list;
        }


    }

    public class IMS_VC_DJT_WCF : WcfDataServiceBase<IMS_VC_DJT>, IWcfOrderForm
    {
        IMS_VC_DJT_Dal dal = new IMS_VC_DJT_Dal();

        protected override Entities.WCF.WcfResponse RequestFun(Entities.WCF.WcfRequest req)
        {

            var res = new WcfResponse();

            object[] objs;
            switch (req.Head.RequestMethodName)
            {
                case "GetDataViewByPageDynamic":
                    objs = (object[])req.Body.Parameters;
                    int rowCount;

                    var dataList =
                        dal.GetDataViewByPageDynamic(
                            (int)objs[0]        // PageIndex
                            , (int)objs[1]      // PageSize
                            , out rowCount
                            , (string)objs[2]   // OrderBy
                            , (string)objs[3]   // Where
                            , (object[])objs[4]); //WhereParams

                    res.Body.Data = new object[] { rowCount, dataList };
                    break;

                case "UpdateOrder":
                    objs = (object[])req.Body.Parameters;
                    dal.UpdateOrder((long)objs[0], (string)objs[1], (EnumOrderFormState)objs[2]);
                    break;

                default:
                    return null;
            }
            return res;
        }

        protected override Entities.IDataLayer<IMS_VC_DJT> DataLayer
        {
            get { return dal; }
        }
    }

}
