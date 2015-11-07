using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Concrete
{
    public class IMS_TB_Module_Dal : DataLayerBase<IMS_TB_Module>
    {
 
        public override IMS_TB_Module Update(IMS_TB_Module entity)
        {
            IMS_TB_Module ent = null;
            if (entity.ID == 0)
            {
                ent = base.Insert(entity);
            }
            else
            {
                ent = Find(entity.ID);
                if (ent != null)
                {
                    ent.ModuleID  = entity.ModuleID;
                    ent.ModuleName = entity.ModuleName;
                    ent.ParentID = entity.ParentID;
                    ent.URL = entity.URL;
                    ent.SortID = entity.SortID;
                    ent.Description = entity.Description;
                    ent = base.Update(ent);
                }
            }
            return ent;
        }
    }
}
