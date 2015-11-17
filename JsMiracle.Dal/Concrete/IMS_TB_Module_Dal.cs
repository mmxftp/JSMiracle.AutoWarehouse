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
    public class IMS_TB_Module_Dal : IIMS_ORGEntities, IModule
    {
        //public override T Update<T>(T entity)
        //{
        //    return base.Update<T>(entity);
        //}

        private IMS_TB_Module Update(IMS_TB_Module entity)
        {
            IMS_TB_Module ent = null;
            if (entity.ID == 0)
            {
                ent = DataLayerBase.Insert(this, entity);
            }
            else
            {
                ent = this.IMS_TB_ModuleSet.Find(entity.ID);
                if (ent != null)
                {
                    ent.ModuleID = entity.ModuleID;
                    ent.ModuleName = entity.ModuleName;
                    ent.ParentID = entity.ParentID;
                    ent.URL = entity.URL;
                    ent.SortID = entity.SortID;
                    ent.Description = entity.Description;
                    ent.Controller_Name = entity.Controller_Name;
                    ent.Action_Name = entity.Action_Name;
                    ent = DataLayerBase.Update(this, ent);
                }
            }
            return ent;
        }


        public IList<IMS_TB_Module> GetRootModule()
        {
            var data = this.IMS_TB_ModuleSet.Where(n => n.ParentID == -1).OrderBy(n => n.SortID);
            return data.ToList();
        }


        public IList<IMS_TB_Module> GetChildModuleList(int parentid)
        {
            var dataList = IMS_TB_ModuleSet.Where(n => n.ParentID == parentid).OrderBy(n => n.SortID);
            return dataList.ToList();
        }

        public IList<IMS_TB_Module> GetChildModuleList(int pageIndex,
            int pageSize, int parentid, out int totalCount)
        {
            totalCount = 0;

            Expression<Func<IMS_TB_Module, bool>> filter =
                f => f.ParentID == parentid;

            var dataList = DataLayerBase.GetDataByPage(
                this,
                p => p.SortID,
                filter, pageIndex, pageSize, out totalCount);

            return dataList;
        }

        public IMS_TB_Module Save(IMS_TB_Module module)
        {
            // 新增时自动计算模块号
            if (module.ParentID != -1 && module.ModuleID == 0)
            {

                //var d = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this).ObjectContext;

                // CreateQuery 中 返回一个值时必须要把此值定义为VALUE才能得到数据
                // 如select VALUE a.ModuleID from  IMS_TB_ModuleSet
                //                string sql = string.Format(
                //                    @"select VALUE  (min(a.ModuleID)+ 1) % {0} + {0} 
                //                        from IMS_TB_ModuleSet as a
                //                        where a.parentid = {1} 
                //                            and not exists (select 'f' from IMS_TB_ModuleSet as b
                //	                        where b.parentid =  {1}  and b.ModuleID  =  (a.ModuleID+ 1) % {0} + {0}
                //                        	AND A.ModuleID >= {2} )	"
                //                    , parentGroupNumber, module.ParentID, parentGroupNumber + 101);


                // 得到可用的最小号码
                //var nextModuleId = d.CreateQuery<int>(sql);
                //module.ModuleID = nextModuleId.First();

                int parentGroupNumber = module.ParentID * 1000;

                // 默认序号
                int defaultModuleID = parentGroupNumber + 101;

                // 得到当前序号中断开的最小号码
                // 如:   1,2,3,4,6 , => 5
                //       1,2,3       => 4 
                var nextModuleIDQueryable =
                    from a in IMS_TB_ModuleSet.Where(n => n.ParentID == module.ParentID)
                    join b in IMS_TB_ModuleSet.Where(n => n.ParentID == module.ParentID)
                    on (a.ModuleID + 1) % parentGroupNumber + parentGroupNumber equals b.ModuleID into left_Join
                    from v in left_Join.DefaultIfEmpty()
                    where v.ID == null && a.ModuleID >= defaultModuleID
                    select a.ModuleID;

                if (nextModuleIDQueryable.Count() > 0)
                    module.ModuleID = (nextModuleIDQueryable.Min() + 1) % parentGroupNumber + parentGroupNumber;
                else
                    module.ModuleID = defaultModuleID;


            }

            return Update(module);
        }

        public int Remove(int id)
        {
            var ent = IMS_TB_ModuleSet.Find(id);

            if (ent == null)
                return 0;

            if (ent.ParentID == -1)
            {
                var childModule = IMS_TB_ModuleSet.Where(f => f.ParentID == ent.ModuleID);
                if (childModule != null && childModule.Count() > 0)
                    throw new Exception("请先删除子模块数据");
            }

            return DataLayerBase.Delete(this, ent);
        }


        public IMS_TB_Module GetEntity(int? id, int moduleid = -1)
        {
            if (id.HasValue)
                return IMS_TB_ModuleSet.Find(id);

            if (moduleid != -1)
                return IMS_TB_ModuleSet.Where(n => n.ModuleID == moduleid).FirstOrDefault();

            throw new JsMiracleException("id或moduleid 必须有一个参数有值");
        }


    }
}
