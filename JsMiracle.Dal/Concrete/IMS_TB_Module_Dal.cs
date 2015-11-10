using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JsMiracle.Dal.Concrete
{
    public class IMS_TB_Module_Dal : DataLayerBase, IModule
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
                ent = this.IMS_TB_Module.Find(entity.ID);
                if (ent != null)
                {
                    ent.ModuleID = entity.ModuleID;
                    ent.ModuleName = entity.ModuleName;
                    ent.ParentID = entity.ParentID;
                    ent.URL = entity.URL;
                    ent.SortID = entity.SortID;
                    ent.Description = entity.Description;
                    ent = DataLayerBase.Update(this, ent);
                }
            }
            return ent;
        }


        public IList<IMS_TB_Module> GetRootModule()
        {
            var data = this.IMS_TB_Module.Where(n => n.ParentID == -1).OrderBy(n=>n.SortID);
            return data.ToList();
        }


        public IList<IMS_TB_Module> GetChildModuleList(int parentid)
        {
            var dataList = IMS_TB_Module.Where(n => n.ParentID== parentid).OrderBy(n=>n.SortID);
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
                var itemCount = IMS_TB_Module.Where(n => n.ParentID == module.ParentID).Count();
                // 得到同类的子项的记数加1
                module.ModuleID = module.ParentID * 1000 + itemCount + 101;
            }

            return Update(module);
        }

        public int Remove(int id)
        {
            var ent = IMS_TB_Module.Find(id);

            if (ent == null)
                return 0;

            if (ent.ParentID == -1)
            {
                var childModule = IMS_TB_Module.Where(f => f.ParentID == ent.ModuleID);
                if (childModule != null && childModule.Count() > 0)
                    throw new Exception("请先删除子模块数据");
            }

            return DataLayerBase.Delete(this,ent);
        }


        public IMS_TB_Module GetEntity(int id)
        {
            return IMS_TB_Module.Find(id);
        }


    }
}
