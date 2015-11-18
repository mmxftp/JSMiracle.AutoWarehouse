using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Concrete
{
    public class IMS_TB_ModuleFunction_Dal : DataLayer<IMS_TB_ModuleFunction>, IModuleFunction
    {
        //private IMS_TB_ModuleFunction Update(IMS_TB_ModuleFunction entity)
        //{
        //    IMS_TB_ModuleFunction ent = null;
        //    if (entity.ID == 0)
        //    {
        //        ent = DataLayerBase.Insert(this, entity);
        //    }
        //    else
        //    {
        //        ent = IMS_TB_ModuleFunctionSet.Find(entity.ID);
        //        if (ent != null)
        //        {
        //            ent.ModuleID = entity.ModuleID;
        //            ent.FunctionID = entity.FunctionID;
        //            ent.FunctionName = entity.FunctionName;
        //            ent.ControlID = entity.ControlID;
        //            ent.Controller_Name = entity.Controller_Name;
        //            ent.Action_Name = entity.Action_Name;
        //            ent.Description = entity.Description;
        //            ent = DataLayerBase.Update(this, ent);
        //        }
        //    }
        //    return ent;
        //}


        public IList<IMS_TB_ModuleFunction> GetModuleFunctionList(int moduleid)
        {
            var data = this.DbContext.IMS_TB_ModuleFunctionSet.Where(n => n.ModuleID == moduleid);
            return data.ToList();
        }

        //public IMS_TB_ModuleFunction GetEntity(int id)
        //{
        //    return IMS_TB_ModuleFunctionSet.Find(id);
        //}

        public override void SaveOrUpdate(IMS_TB_ModuleFunction entity)
        {
            if (entity.FunctionID == 0)
            {
                //var itemCount = IMS_TB_ModuleFunctionSet.Where(n => n.ModuleID == module.ModuleID).Count();
                // 得到同类的子项的记数加1
                //module.FunctionID = module.ModuleID * 100 + itemCount + 1;

                var moduleGroupNumber = entity.ModuleID * 100;

                var defaultFunctionID = moduleGroupNumber + 1;

                // 得到当前序号中断开的最小号码
                // 如:   1,2,3,4,6 , => 5
                //       1,2,3       => 4 
                var nextFunctionIDQueryable =
                    from a in this.DbContext.IMS_TB_ModuleFunctionSet.Where(n => n.ModuleID == entity.ModuleID)
                    join b in this.DbContext.IMS_TB_ModuleFunctionSet.Where(n => n.ModuleID == entity.ModuleID)
                    on (a.FunctionID + 1) % moduleGroupNumber + moduleGroupNumber equals b.FunctionID into left_Join
                    from v in left_Join.DefaultIfEmpty()
                    where v.ID == null && a.FunctionID >= defaultFunctionID
                    select a.FunctionID;

                if (nextFunctionIDQueryable.Count() > 0)
                    entity.FunctionID = (nextFunctionIDQueryable.Min() + 1) % moduleGroupNumber + moduleGroupNumber;
                else
                    entity.FunctionID = defaultFunctionID;
            }

            base.SaveOrUpdate(entity);
        }

        //public int Remove(int id)
        //{
        //    var ent = IMS_TB_ModuleFunctionSet.Find(id);

        //    if (ent == null)
        //        return 0;

        //    return DataLayerBase.Delete(this, ent);
        //}
    }
}
