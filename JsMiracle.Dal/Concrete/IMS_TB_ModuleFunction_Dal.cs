using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Concrete
{
    public class IMS_TB_ModuleFunction_Dal : IIMS_ORGEntities, IModuleFunction
    {
        private IMS_TB_ModuleFunction Update(IMS_TB_ModuleFunction entity)
        {
            IMS_TB_ModuleFunction ent = null;
            if (entity.ID == 0)
            {
                ent = DataLayerBase.Insert(this, entity);
            }
            else
            {
                ent = IMS_TB_ModuleFunctionSet.Find(entity.ID);
                if (ent != null)
                {
                    ent.ModuleID = entity.ModuleID;
                    ent.FunctionID = entity.FunctionID;
                    ent.FunctionName = entity.FunctionName;
                    ent.ControlID = entity.ControlID;
                    ent.Description = entity.Description;
                    ent = DataLayerBase.Update(this, ent);
                }
            }
            return ent;
        }


        public IList<IMS_TB_ModuleFunction> GetModuleFunctionList(int moduleid)
        {
            var data = IMS_TB_ModuleFunctionSet.Where(n => n.ModuleID == moduleid);
            return data.ToList();
        }

        public IMS_TB_ModuleFunction GetEntity(int id)
        {
            return IMS_TB_ModuleFunctionSet.Find(id);
        }

        public IMS_TB_ModuleFunction Save(IMS_TB_ModuleFunction function)
        {
            if (function.FunctionID == 0)
            {
                //var itemCount = IMS_TB_ModuleFunctionSet.Where(n => n.ModuleID == module.ModuleID).Count();
                // 得到同类的子项的记数加1
                //module.FunctionID = module.ModuleID * 100 + itemCount + 1;

                var moduleGroupNumber = function.ModuleID * 100;

                var defaultFunctionID = moduleGroupNumber + 1;

                // 得到当前序号中断开的最小号码
                // 如:   1,2,3,4,6 , => 5
                //       1,2,3       => 4 
                var nextFunctionIDQueryable =
                    from a in IMS_TB_ModuleFunctionSet.Where(n => n.ModuleID == function.ModuleID)
                    join b in IMS_TB_ModuleFunctionSet.Where(n => n.ModuleID == function.ModuleID)
                    on (a.FunctionID + 1) % moduleGroupNumber + moduleGroupNumber equals b.FunctionID into left_Join
                    from v in left_Join.DefaultIfEmpty()
                    where v.ID == null && a.FunctionID >= defaultFunctionID
                    select a.FunctionID;

                if (nextFunctionIDQueryable.Count() > 0)
                    function.FunctionID = (nextFunctionIDQueryable.Min() + 1) % moduleGroupNumber + moduleGroupNumber;
                else
                    function.FunctionID = defaultFunctionID;
            }

            return Update(function);
        }

        public int Remove(int id)
        {
            var ent = IMS_TB_ModuleFunctionSet.Find(id);

            if (ent == null)
                return 0;

            return DataLayerBase.Delete(this, ent);
        }
    }
}
