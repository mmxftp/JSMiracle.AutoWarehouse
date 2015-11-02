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
        public IMS_TB_UserInfo_Dal(IIMS_ORGEntities entity) : base(entity) 
        { 
        }

        //public IMS_TB_UserInfo Save(IMS_TB_UserInfo entity)
        public override IMS_TB_UserInfo Update(IMS_TB_UserInfo entity)
        {
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
