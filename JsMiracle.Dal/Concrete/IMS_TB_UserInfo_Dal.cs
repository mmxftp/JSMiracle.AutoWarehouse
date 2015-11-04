using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Concrete
{
    public class IMS_TB_UserInfo_Dal:DataLayerBase<IMS_TB_UserInfo>
    {
        public IMS_TB_UserInfo_Dal(IIMS_ORGEntities context)
            : base(context)
        {
        }

        public override IMS_TB_UserInfo Update(IMS_TB_UserInfo entity)
        {
            //var data = from m in context.IMS_TB_Module
            //           join f in context.IMS_TB_ModuleFunction
            //           on m.ModuleID equals f.ModelID
            //           select m;

            IMS_TB_UserInfo ent = null;
            if (entity.ID == 0)
            {
                ent = base.Insert(entity);
            }
            else
            {
                ent = Find(entity.ID);
                if (ent != null)
                {
                    ent.UserID = entity.UserID;
                    ent.UserName = entity.UserName;
                    ent.Email = entity.Email;
                    ent.Password = entity.Password;
                    ent.LastModDate = System.DateTime.Now;
                    ent = base.Update(ent);
                }
            }
            return ent;
        }
    }
}
