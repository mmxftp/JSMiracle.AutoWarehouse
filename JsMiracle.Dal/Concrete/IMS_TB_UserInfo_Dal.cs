using JsMiracle.Dal.Abstract;
using JsMiracle.Dal.Models;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JsMiracle.Dal.Concrete
{
    public class IMS_TB_UserInfo_Dal : IIMS_ORGEntities, IUser
    {
        private IMS_TB_UserInfo Update(IMS_TB_UserInfo entity)
        {
            entity.Password = IMS_TB_UserInfo.GetPwdMD5(entity.Password);
            
            IMS_TB_UserInfo ent = null;
            if (entity.ID == 0)
            {
                entity.State = (int)UserStateEnum.Normal;
                ent = DataLayerBase.Insert(this, entity);
            }
            else
            {
                ent = IMS_TB_UserInfoSet.Find(entity.ID);
                if (ent != null)
                {
                    ent.UserID = entity.UserID;
                    ent.UserName = entity.UserName;
                    ent.Email = entity.Email;
                    ent.Password = entity.Password;
                    ent.LastModDate = System.DateTime.Now;
                    ent.State = entity.State;
                    ent = DataLayerBase.Update(this, ent);
                }
            }
            return ent;
        }

        public IList<IMS_TB_UserInfo> GetAllUserList(bool userNameFormatter = false)
        {
            var data = IMS_TB_UserInfoSet.Where(n => n.State == (int)UserStateEnum.Normal).ToList();

            if (userNameFormatter)
            {
                data = data.Select(u => new IMS_TB_UserInfo
                {
                    UserName = string.Format("{0}({1})", u.UserName, u.UserID),
                    UserID = u.UserID,
                    ID = u.ID,
                    CreateDate = u.CreateDate,
                    LastModDate = u.LastModDate,
                    Email = u.Email,
                    Password = u.Password,
                    State = u.State
                }).ToList();
            }


            return data;
        }

        public IMS_TB_UserInfo GetEntity(int id)
        {
            return IMS_TB_UserInfoSet.Find(id);
        }

        public IList<IMS_TB_UserInfo> GetUserList(int pageIndex, int pageSize, string txt, out int totalCount)
        {
            Expression<Func<IMS_TB_UserInfo, bool>> filter = null;

            if (!string.IsNullOrEmpty(txt))
            {
                filter =
                    f => (f.UserID == txt || f.UserName.Contains(txt)) && f.State == (int)UserStateEnum.Normal;
            }
            else
            {
                filter =
                    f => f.State == (int)UserStateEnum.Normal;
            }

            var dataList = DataLayerBase.GetDataByPage(
                this,
                p => p.UserID,
                filter, pageIndex, pageSize, out totalCount);

            return dataList;
        }

        public int Save(IMS_TB_UserInfo user)
        {
            Update(user);
            return 1;
        }

        public int Remove(int id)
        {
            var ent = IMS_TB_UserInfoSet.Find(id);

            if (ent == null)
                return 0;

            ent.State = (int)UserStateEnum.Delete;

            DataLayerBase.Update(this, ent);
            return 1;
        }


        public bool Validating(string user, string password)
        {
            throw new NotImplementedException();
        }
    }


}
