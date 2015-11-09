using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Concrete
{
    public class IMS_TB_RoleInfo_Dal : DataLayerBase<IMS_TB_RoleInfo>
    {
        //public IMS_TB_RoleInfo_Dal(IIMS_ORGEntities context)
        //    : base(context)
        //{
        //}

        public override IMS_TB_RoleInfo Update(IMS_TB_RoleInfo entity)
        {
            
            IMS_TB_RoleInfo ent = null;
            if (entity.ID == 0)
            {
                ent = base.Insert(entity);
            }
            else
            {
                ent = Find(entity.ID);
                if (ent != null)
                {
                    ent.ID = entity.ID;
                    ent.RoleID = entity.RoleID;
                    ent.RoleName = entity.RoleName;
                    ent.Description = entity.Description;
                    ent = base.Update(ent);
                }
            }
            return ent;
        }
    }
}
