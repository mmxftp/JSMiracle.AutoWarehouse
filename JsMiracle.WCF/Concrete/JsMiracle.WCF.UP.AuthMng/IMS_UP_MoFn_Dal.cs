using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.UP.IAuthMng;
using System.Collections.Generic;
using System.Linq.Dynamic;
using System.Linq;
using JsMiracle.Entities;
using JsMiracle.WCF.WcfBaseService;
using System;
using JsMiracle.WCF.Interface;
using JsMiracle.Entities.WCF;

namespace JsMiracle.WCF.UP.AuthMng
{
    public class IMS_UP_MoFn_Dal : WcfDataLayerBase<IMS_UP_MKGN>, IModuleFunction
    {

        public List<IMS_UP_MKGN> GetModuleFunctionList(int moduleid)
        {
            var data = this.DbContext.IMS_UP_MKGN_S.Where(n => n.MKID == moduleid);
            return data.ToList();
        }


        public override IMS_UP_MKGN SaveOrUpdate(IMS_UP_MKGN entity)
        {

            if (entity.GNID == 0)
            {
                //var itemCount = IMS_UP_MoFnT.Where(n => n.ModuleID == module.ModuleID).Count();
                // 得到同类的子项的记数加1
                //module.FunctionID = module.ModuleID * 100 + itemCount + 1;

                var moduleGroupNumber = entity.MKID * 100;

                var defaultFunctionID = moduleGroupNumber + 1;

                // 得到当前序号中断开的最小号码
                // 如:   1,2,3,4,6 , => 5
                //       1,2,3       => 4 
                var nextFunctionIDQueryable =
                    from a in this.DbContext.IMS_UP_MKGN_S.Where(n => n.MKID == entity.MKID)
                    join b in this.DbContext.IMS_UP_MKGN_S.Where(n => n.MKID == entity.MKID)
                    on (a.GNID + 1) % moduleGroupNumber + moduleGroupNumber equals b.GNID into left_Join
                    from v in left_Join.DefaultIfEmpty()
                    where v.ID == null && a.GNID >= defaultFunctionID
                    select a.GNID;

                if (nextFunctionIDQueryable.Count() > 0)
                    entity.GNID = (nextFunctionIDQueryable.Min() + 1) % moduleGroupNumber + moduleGroupNumber;
                else
                    entity.GNID = defaultFunctionID;
            }

            base.SaveEntity(entity);
            return entity;
        }

        public override void Delete(object id)
        {

            var ent = FindEntityByKey(id);

            var permissionList = base.DbContext.IMS_UP_JSMK_S.Where(n => n.GNID == ent.GNID);

            using (var tran = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    this.DbContext.IMS_UP_JSMK_S.RemoveRange(permissionList);
                    this.DbContext.SaveChanges();
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                }
            }

            base.DeleteByKey(id);
        }
    }

    public class IMS_UP_MoFn_WCF : WcfService<IMS_UP_MKGN>, IWcfModuleFunction
    {
        IMS_UP_MoFn_Dal dal = new IMS_UP_MoFn_Dal();

        protected override IDataLayer<IMS_UP_MKGN> DataLayer
        {
            get { return dal; }
        }
        public WcfResponse RequestOverride(WcfRequest req)
        {
            return this.RequestFun(req);
        }


        protected override WcfResponse RequestFun(WcfRequest req)
        {
            WcfResponse res = new WcfResponse();

            object[] objs;

            switch (req.Head.RequestMethodName)
            {
                case "GetModuleFunctionList":
                    objs = (object[])req.Body.Parameters;
                    res.Body.Data = dal.GetModuleFunctionList((int)objs[0]);
                    break;
                default:
                    return null;
            }

            res.Head.IsSuccess = true;
            return res;
        }
    }
}
