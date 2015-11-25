using JsMiracle.Dal.Abstract;
using JsMiracle.Dal.Abstract.UP;
using JsMiracle.Entities;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JsMiracle.Dal.Concrete.UP
{
    public class IMS_UP_Modu_Dal : DataLayerBase<IMS_UP_MK>, IModule
    {

        public IList<IMS_UP_MK> GetRootModule()
        {
            var data = this.DbContext.IMS_UP_MK_S.Where(n => n.FMKID == -1).OrderBy(n => n.PXID);
            return data.ToList();
        }


        public IList<IMS_UP_MK> GetChildModuleList(int parentid)
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

            base.SaveOrUpdate(entity);
        }

        public override void Delete(long id)
        {
            var ent = GetEntity(id);

            if (ent == null)
                return;

            if (ent.FMKID == -1)
            {
                var childModule = this.DbContext.IMS_UP_MK_S.Where(f => f.FMKID == ent.MKID);
                if (childModule != null && childModule.Count() > 0)
                    throw new Exception("请先删除子模块数据");
            }

            base.Delete(id);
        }


        public IMS_UP_MK GetEntityByModuleID(int moduleid)
        {
            var data = this.DbContext.IMS_UP_MK_S.Where(n => n.MKID == moduleid);

            return data.FirstOrDefault();
        }


    }
}
