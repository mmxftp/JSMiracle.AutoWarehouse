using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JsMiracle.Dal.Concrete
{
    public class IMS_UP_Modu_Dal : DataLayer<IMS_UP_Modu>, IModule
    {

        //private IMS_UP_Modu Update(IMS_UP_Modu entity)
        //{
        //    IMS_UP_Modu ent = null;
        //    if (entity.ID == 0)
        //    {
        //        ent = DataLayerBase.Insert(this, entity);
        //    }
        //    else
        //    {
        //        ent = this.IMS_UP_ModuT.Find(entity.ID);
        //        if (ent != null)
        //        {
        //            ent.ModuleID = entity.ModuleID;
        //            ent.ModuleName = entity.ModuleName;
        //            ent.ParentID = entity.ParentID;
        //            ent.URL = entity.URL;
        //            ent.SortID = entity.SortID;
        //            ent.Description = entity.Description;
        //            ent.Controller_Name = entity.Controller_Name;
        //            ent.Action_Name = entity.Action_Name;
        //            ent = DataLayerBase.Update(this, ent);
        //        }
        //    }
        //    return ent;
        //}


        public IList<IMS_UP_Modu> GetRootModule()
        {
            var data = this.DbContext.IMS_UP_ModuT.Where(n => n.ParentID == -1).OrderBy(n => n.SortID);
            return data.ToList();
        }


        public IList<IMS_UP_Modu> GetChildModuleList(int parentid)
        {
            var dataList = this.DbContext.IMS_UP_ModuT.Where(n => n.ParentID == parentid).OrderBy(n => n.SortID);
            return dataList.ToList();
        }

        //public IList<IMS_UP_Modu> GetChildModuleList(int pageIndex,
        //    int pageSize, int parentid, out int totalCount)
        //{
        //    totalCount = 0;

        //    Expression<Func<IMS_UP_Modu, bool>> filter =
        //        f => f.ParentID == parentid;

        //    var dataList = DataLayerBase.GetDataByPage(
        //        this,
        //        p => p.SortID,
        //        filter, pageIndex, pageSize, out totalCount);

        //    return dataList;
        //}

        //public IMS_UP_Modu Save(IMS_UP_Modu module)

        public override void SaveOrUpdate(IMS_UP_Modu entity)
        {
            // 新增时自动计算模块号
            if (entity.ParentID != -1 && entity.ModuleID == 0)
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

                int parentGroupNumber = entity.ParentID * 1000;

                // 默认序号
                int defaultModuleID = parentGroupNumber + 101;

                // 得到当前序号中断开的最小号码
                // 如:   1,2,3,4,6 , => 5
                //       1,2,3       => 4 
                var nextModuleIDQueryable =
                    from a in this.DbContext.IMS_UP_ModuT.Where(n => n.ParentID == entity.ParentID)
                    join b in this.DbContext.IMS_UP_ModuT.Where(n => n.ParentID == entity.ParentID)
                    on (a.ModuleID + 1) % parentGroupNumber + parentGroupNumber equals b.ModuleID into left_Join
                    from v in left_Join.DefaultIfEmpty()
                    where v.ID == null && a.ModuleID >= defaultModuleID
                    select a.ModuleID;

                if (nextModuleIDQueryable.Count() > 0)
                    entity.ModuleID = (nextModuleIDQueryable.Min() + 1) % parentGroupNumber + parentGroupNumber;
                else
                    entity.ModuleID = defaultModuleID;


            }

            base.SaveOrUpdate(entity);
        }

        public override void Delete(long id)
        {
            var ent = GetEntity(id);

            if (ent == null)
                return;

            if (ent.ParentID == -1)
            {
                var childModule = this.DbContext.IMS_UP_ModuT.Where(f => f.ParentID == ent.ModuleID);
                if (childModule != null && childModule.Count() > 0)
                    throw new Exception("请先删除子模块数据");
            }

            base.Delete(id);
        }

        //public int Remove(int id)
        //{
        //    var ent = IMS_UP_ModuT.Find(id);

        //    if (ent == null)
        //        return 0;

        //    if (ent.ParentID == -1)
        //    {
        //        var childModule = IMS_UP_ModuT.Where(f => f.ParentID == ent.ModuleID);
        //        if (childModule != null && childModule.Count() > 0)
        //            throw new Exception("请先删除子模块数据");
        //    }

        //    return DataLayerBase.Delete(this, ent);
        //}


        public IMS_UP_Modu GetEntityByModuleID(int moduleid)
        {
            var data = this.DbContext.IMS_UP_ModuT.Where(n => n.ModuleID == moduleid);

            return data.FirstOrDefault();
        }


    }
}
