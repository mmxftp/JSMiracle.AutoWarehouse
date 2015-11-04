using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Concrete
{
    public class IMS_TB_ModuleFunction_Dal:DataLayerBase<IMS_TB_ModuleFunction>
    {

        public IMS_TB_ModuleFunction_Dal(IIMS_ORGEntities context)
            : base(context)
        {
        }

        public override IMS_TB_ModuleFunction Update(IMS_TB_ModuleFunction entity)
        {
            IMS_TB_ModuleFunction ent = null;
            if (entity.ID == 0)
            {
                ent = base.Insert(entity);
            }
            else
            {
                ent = Find(entity.ID);
                if (ent != null)
                {
                    ent.ModuleID = entity.ModuleID;
                    ent.FunctionID = entity.FunctionID;
                    ent.ControlID = entity.ControlID;
                    ent.Description = entity.Description;
                    ent = base.Update(ent);
                }
            }
            return ent;
        }

    }
}
