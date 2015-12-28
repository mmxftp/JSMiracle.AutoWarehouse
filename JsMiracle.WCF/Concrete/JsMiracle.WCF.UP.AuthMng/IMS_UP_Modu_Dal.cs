using JsMiracle.Entities.TabelEntities;
using JsMiracle.Framework;
using JsMiracle.WCF.UP.IAuthMng;
using System.Collections.Generic;
using System.Linq.Dynamic;
using System.Linq;
using System.ServiceModel;
using JsMiracle.Entities;
using JsMiracle.WCF.WcfBaseService;
using System;
using JsMiracle.WCF.Interface;
using System.Runtime.Serialization;
using JsMiracle.Entities.WCF;

namespace JsMiracle.WCF.UP.AuthMng
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class IMS_UP_Modu_Dal : WcfDataLayerBase<IMS_UP_MK>, IModule
    {

        public List<IMS_UP_MK> GetRootModule()
        {
            var data = this.DbContext.IMS_UP_MK_S.Where(n => n.FMKID == -1).OrderBy(n => n.PXID);
            return data.ToList();
        }


        public List<IMS_UP_MK> GetChildModuleList(int parentid)
        {
            var dataList = this.DbContext.IMS_UP_MK_S.Where(n => n.FMKID == parentid).OrderBy(n => n.PXID);
            return dataList.ToList();
        }

        public override void SaveOrUpdate(IMS_UP_MK entity)
        {

            // 新增时自动计算模块号
            if (entity.FMKID != -1 && entity.MKID == 0)
            {

                //var d = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this).ObjectContext;

                // CreateQuery 中 返回一个值时必须要把此值定义为VALUE才能得到数据
                // 如select VALUE a.ModuleID from  IMS_UP_ModuT
                //                string sql = string.Format(
                //                    @"select VALUE  (min(a.ModuleID)+ 1) % {0} + {0} 
                //                        from IMS_UP_ModuT as a
                //                        where a.parentid = {1} 
                //                            and not exists (select 'f' from IMS_UP_ModuT as b
                //	                        where b.parentid =  {1}  and b.ModuleID  =  (a.ModuleID+ 1) % {0} + {0}
                //                        	AND A.ModuleID >= {2} )	"
                //                    , parentGroupNumber, module.ParentID, parentGroupNumber + 101);


                // 得到可用的最小号码
                //var nextModuleId = d.CreateQuery<int>(sql);
                //module.ModuleID = nextModuleId.First();

                int parentGroupNumber = entity.FMKID * 1000;

                // 默认序号
                int defaultModuleID = parentGroupNumber + 101;

                // 得到当前序号中断开的最小号码
                // 如:   1,2,3,4,6 , => 5
                //       1,2,3       => 4 
                var nextModuleIDQueryable =
                    from a in this.DbContext.IMS_UP_MK_S.Where(n => n.FMKID == entity.FMKID)
                    join b in this.DbContext.IMS_UP_MK_S.Where(n => n.FMKID == entity.FMKID)
                    on (a.MKID + 1) % parentGroupNumber + parentGroupNumber equals b.MKID into left_Join
                    from v in left_Join.DefaultIfEmpty()
                    where v.ID == null && a.MKID >= defaultModuleID
                    select a.MKID;

                if (nextModuleIDQueryable.Count() > 0)
                    entity.MKID = (nextModuleIDQueryable.Min() + 1) % parentGroupNumber + parentGroupNumber;
                else
                    entity.MKID = defaultModuleID;


            }

            base.SaveEntity(entity);

        }

        public override void Delete(object id)
        {

            var ent = FindEntityByKey(id);

            if (ent == null)
                return;

            if (ent.FMKID == -1)
            {
                var childModule = this.DbContext.IMS_UP_MK_S.Where(f => f.FMKID == ent.MKID);
                if (childModule != null && childModule.Count() > 0)
                    throw new JsMiracleException("请先删除子模块数据");
            }
            else
            {
                var gnList = this.DbContext.IMS_UP_MKGN_S.Where(m => m.MKID == ent.MKID);
                if (gnList.Count() > 0)
                    throw new JsMiracleException("请先删除子功能数据");
            }

            var jsmkList = this.DbContext.IMS_UP_JSMK_S.Where(n => n.MKID == ent.MKID);

            using (var tran = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    this.DbContext.IMS_UP_JSMK_S.RemoveRange(jsmkList);
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


        public IMS_UP_MK GetEntityByModuleID(int moduleid)
        {
            var data = this.DbContext.IMS_UP_MK_S.Where(n => n.MKID == moduleid);

            return data.FirstOrDefault();
        }



    }

    public class IMS_UP_Modu_WCF : WcfService<IMS_UP_MK>, IWcfModule
    {
        IMS_UP_Modu_Dal dal = new IMS_UP_Modu_Dal();

        protected override IDataLayer<IMS_UP_MK> DataLayer
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
                case "GetRootModule":
                    res.Body.Data= dal.GetRootModule();
                    break;
                    
                case "GetEntityByModuleID":
                    objs = (object[])req.Body.Parameters;
                    res.Body.Data = dal.GetEntityByModuleID((int)objs[0]);
                    break;

                case "GetChildModuleList":
                    objs = (object[])req.Body.Parameters;
                    res.Body.Data = dal.GetChildModuleList((int)objs[0]);
                    break;

                default :
                    return null;
            }

            res.Head.IsSuccess = true;
            return res;
        }


    }
}
