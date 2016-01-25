using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.WCF;
using JsMiracle.Framework;
using JsMiracle.WCF.WcfBaseService;
using JsMiracle.WCF.WT.IWorkTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace JsMiracle.WCF.WT.WorkTasks
{
    /// <summary>
    /// 业务类工作任务
    /// </summary>
    public class IMS_WT_YWRW_Dal : WcfDataLayerBase<IMS_WT_YWRW>, IBusinessTasks
    {

        public IMS_WT_YWRW GetTaskByDJHID(long djhid)
        {
            var ent = DbContext.IMS_WT_YWRW_S.Where(n => n.DJH_ID == djhid).FirstOrDefault();
            return ent;
        }

        public void ZPIn(long id, string userid, long sku, decimal zpsl, string rqbh)
        {
            var bizEnt = GetEntity(id);

            if (bizEnt == null)
                throw new JsMiracleException("业务任务不存在无法处理");

            if (DbContext.IMS_CB_KC_S.Any(n => n.RQBH.Equals(rqbh, StringComparison.CurrentCultureIgnoreCase)))
                throw new JsMiracleException("容器编号已存在库存中，请重新输入容器编号");

            if (!DbContext.IMS_CB_WL_S.Any(n => n.ID == sku))
                throw new JsMiracleException(string.Format("物料信息不存在sku({0})", sku));

            // 已生成单据的操作数
            var buildTaskNumber = 
                DbContext.IMS_WT_CWRW_S.Where(n => n.FRWID == bizEnt.ID).Sum(n => n.RWSL) ?? 0;

            // 生成单据数据加本次组盘数量大于 业务单据最大数量，报错
            if ((buildTaskNumber + zpsl) > bizEnt.DJSL)
                throw new JsMiracleException(string.Format("本次组盘数量大于本笔业务的剩余可用数量",zpsl));

            var rqEnt = DbContext.IMS_CB_RQ_S.Where(n =>
                n.RQBH.Equals(rqbh, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (rqEnt == null)
            {
                rqEnt = new IMS_CB_RQ()
                {
                    RQBH = rqbh,
                    DQCD = 0,
                    DQKD = 0,
                    DQWZ = "",
                    DQZL = 0,
                    LXBH = "",
                    SDZT = 0,
                    SYZT = 0,
                    WZZT = 0,
                    ZYZT = 0,
                    SMTM = "",
                    SYCS = 0,
                    ZCSJ = System.DateTime.Now,
                    ZCWZ = ""
                };
            }

            // 库存信息
            var kc = new IMS_CB_KC()
            {
                KQSL = zpsl,
                KYSL = zpsl,
                PCBH = bizEnt.PCBH,
                KYZT = 0,
                RKRY = userid,
                BZXS = "",
                RKSJ = System.DateTime.Now,
                RQBH = rqbh,
                SDZT = 0,
                SYZ = "",
                SKU = sku,
                ZHXGSJ = System.DateTime.Now,
                ZJZT = 0,
                ZLZT = 0,
            };

            IMS_WT_CWRW op = new IMS_WT_CWRW()
            {
                RWBH = WorkTaskFunction.CreateNextTaskID(),
                CJR = userid,
                CJSJ = System.DateTime.Now,
                DJHID = bizEnt.DJH_ID ?? 0,
                FRWID = bizEnt.ID,
                MDRQ = "",
                RQBH = rqbh,
                RWLX = 0,
                RWSL = zpsl,
                ZT = 0,
            };


            using (var tran = new TransactionScope())
            {
                if (rqEnt.ID == 0)
                    DbContext.IMS_CB_RQ_S.Add(rqEnt);

                DbContext.IMS_CB_KC_S.Add(kc);
                DbContext.SaveChanges();

                //  更新入库存ID
                op.CCID = kc.ID;

                DbContext.IMS_WT_CWRW_S.Add(op);

                DbContext.SaveChanges();

                // 没有异常提交退出
                tran.Complete();
            }
        }


        public void ZPOut(long id, string userid, long sku, decimal zpsl, string rqbh)
        {
            var bizEnt = GetEntity(id);

            if (bizEnt == null)
                throw new JsMiracleException("业务任务不存在无法处理");

            if (!DbContext.IMS_CB_KC_S.Any(n => n.RQBH.Equals(rqbh, StringComparison.CurrentCultureIgnoreCase)))
                throw new JsMiracleException("容器编号不存在库存中，请重新输入容器编号");

            if (!DbContext.IMS_CB_WL_S.Any(n => n.ID == sku))
                throw new JsMiracleException(string.Format("物料信息不存在sku({0})", sku));

            // 已生成单据的操作数
            var buildTaskNumber =
                DbContext.IMS_WT_CWRW_S.Where(n => n.FRWID == bizEnt.ID).Sum(n => n.RWSL) ?? 0;

            // 生成单据数据加本次组盘数量大于 业务单据最大数量，报错
            if ((buildTaskNumber + zpsl) > bizEnt.DJSL)
                throw new JsMiracleException(string.Format("本次组盘数量大于本笔业务的剩余可用数量", zpsl));

            var rqEnt = DbContext.IMS_CB_RQ_S.Where(n =>
                n.RQBH.Equals(rqbh, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();





            throw new NotImplementedException();
        }
    }


    public class IMS_WT_YWRW_WCF : WcfDataServiceBase<IMS_WT_YWRW>, IWcfBusinessTasks
    {
        IMS_WT_YWRW_Dal dal = new IMS_WT_YWRW_Dal();
        protected override Entities.WCF.WcfResponse RequestFun(Entities.WCF.WcfRequest req)
        {
            var res = new WcfResponse();

            object[] objs;
            switch (req.Head.RequestMethodName)
            {
                case "GetTaskByDJHID":
                    objs = (object[])req.Body.Parameters;
                    res.Body.Data = dal.GetTaskByDJHID((long)objs[0]);
                    break;

                case "ZPIn":
                    objs = (object[])req.Body.Parameters;
                    dal.ZPIn((long)objs[0]
                        , (string)objs[1]
                        , (long)objs[2]
                        , (decimal)objs[3]
                        , (string)objs[4]
                       );
                    break;

                case "ZPOut":
                    objs = (object[])req.Body.Parameters;
                    dal.ZPOut((long)objs[0]
                        , (string)objs[1]
                        , (long)objs[2]
                        , (decimal)objs[3]
                        , (string)objs[4]
                       );
                    break;

                default:
                    return null;
            }
            return res;
        }

        protected override Entities.IDataLayer<IMS_WT_YWRW> DataLayer
        {
            get { return dal; }
        }
    }
}
