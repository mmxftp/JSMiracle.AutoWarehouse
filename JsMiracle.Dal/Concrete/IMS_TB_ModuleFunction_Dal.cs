using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Concrete
{
    public class IMS_TB_ModuleFunction_Dal : DataLayerBase, IModuleFunction
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
                ent = IMS_TB_ModuleFunction.Find(entity.ID);
                if (ent != null)
                {
                    ent.ModuleID = entity.ModuleID;
                    ent.FunctionID = entity.FunctionID;
                    ent.ControlID = entity.ControlID;
                    ent.Description = entity.Description;
                    ent = DataLayerBase.Update(this, ent);
                }
            }
            return ent;
        }


        public IList<IMS_TB_ModuleFunction> GetModuleFunctionList(int moduleid)
        {
            var data = IMS_TB_ModuleFunction.Where(n => n.ModuleID == moduleid);
            return data.ToList();
        }

        public IMS_TB_ModuleFunction GetEntity(int id)
        {
            return IMS_TB_ModuleFunction.Find(id);
        }

        public IMS_TB_ModuleFunction Save(IMS_TB_ModuleFunction module)
        {
            if (module.FunctionID == 0)
            {
                var itemCount = IMS_TB_ModuleFunction.Where(n => n.ModuleID == module.ModuleID).Count();
                // 得到同类的子项的记数加1
                module.FunctionID = module.ModuleID * 100 + itemCount + 1;
            }

            return Update(module);
        }

        public int Remove(int id)
        {
            var ent = IMS_TB_ModuleFunction.Find(id);

            if (ent == null)
                return 0;

            return DataLayerBase.Delete(this, ent);
        }
    }
}
