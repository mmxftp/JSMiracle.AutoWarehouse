using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using JsMiracle.Entities.Enum;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JsMiracle.Dal.Concrete
{
    public class IMS_TB_UserInfo_Dal : IIMS_ORGEntities, IUser, IMembershipService
    {
        private void Update(IMS_TB_UserInfo entity)
        {

            IMS_TB_UserInfo ent = null;
            if (entity.ID == 0)
            {
                CreateUser(entity.UserID,entity.UserName, entity.Password, entity.Email);            
            }
            else
            {
                ent = IMS_TB_UserInfoSet.Find(entity.ID);
                if (ent != null)
                {
                    var oldPwd = IMS_TB_UserInfo.GetPwdMD5(ent.Password);
                    if (!string.Equals(oldPwd, entity.Password, StringComparison.CurrentCultureIgnoreCase))
                    {
                        ent.Password = IMS_TB_UserInfo.GetPwdMD5(entity.Password);
                    }

                    ent.UserID = entity.UserID;
                    ent.UserName = entity.UserName;
                    ent.Email = entity.Email;
                    ent.LastModDate = System.DateTime.Now;
                    ent.State = entity.State;
                    DataLayerBase.Update(this, ent);
                }
            }            
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

        public IMS_TB_UserInfo GetEntity(int? id,string userid = null)
        {
            if (id.HasValue)
                return IMS_TB_UserInfoSet.Find(id);

            if (string.IsNullOrEmpty(userid))
                throw new JsMiracleException("id 和 userid 中至少有一个参数有内容");

            var data = IMS_TB_UserInfoSet.Where(n => n.UserID.ToLower() == userid.ToLower());

            return data.FirstOrDefault();
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

        #region IMembershipService

        public int MinPasswordLength
        {
            get { return 1; }
        }

        public bool ValidateUser(string userID, string password)
        {
            if (string.IsNullOrEmpty(userID))
                throw new JsMiracleException("员工号不得为空");

            if (string.IsNullOrEmpty(password))
                throw new JsMiracleException("密码不得为空");

            var ents = IMS_TB_UserInfoSet.Where(n => n.UserID == userID);

            if (ents.Count() == 0)
                return false;

            var user = ents.First();

            // md5 验证是否密码相同
            return IMS_TB_UserInfo.GetPwdMD5(password) == user.Password;
        }

        public void CreateUser(
            string userID, string userName, string password, string email)
        {
            IMS_TB_UserInfo entity = new IMS_TB_UserInfo();

            if (IMS_TB_UserInfoSet.Any(n => n.UserID.Equals(userID, StringComparison.CurrentCultureIgnoreCase)))
                throw new JsMiracleException("员工编号已存在不得重复添加");

            entity.Password = IMS_TB_UserInfo.GetPwdMD5(password);
            entity.UserID = userID;
            entity.UserName = userName;
            entity.CreateDate = System.DateTime.Now;
            entity.State = (int)UserStateEnum.Normal;
            DataLayerBase.Insert(this, entity);
        }

        public bool ChangePassword(string userID, string oldPassword, string newPassword)
        {
            var ent = IMS_TB_UserInfoSet.Where(n => n.UserID.ToLowerInvariant() == userID.ToLowerInvariant());

            if (ent.Count() == 0)
                throw new JsMiracleException("用户不存在");

            var user = ent.First();

            if (IMS_TB_UserInfo.GetPwdMD5(oldPassword) != user.Password)
                throw new JsMiracleException("旧密码不正确");

            user.Password = IMS_TB_UserInfo.GetPwdMD5(newPassword);
            DataLayerBase.Update(this, user);
            return true;
        }
        #endregion
    }


}
