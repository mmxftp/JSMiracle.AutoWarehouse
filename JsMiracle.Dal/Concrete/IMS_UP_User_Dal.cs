using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using JsMiracle.Entities.Enum;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsMiracle.Dal.Concrete
{
    public class IMS_UP_User_Dal : DataLayer<IMS_UP_User>, IUser, IMembershipService
    {
        public override void SaveOrUpdate(IMS_UP_User entity)
        {
            if (entity.ID == 0)
            {
                entity.State = (int)UserStateEnum.Normal;
                entity.CreateDate = DateTime.Now;
                // 密码逻辑由外部定义,此处只做保存
                //entity.Password = IMS_UP_User.GetPwdMD5(entity.Password);

                if (this.DbContext.IMS_UP_UserT.Any(
                    n => n.UserID.Equals(entity.UserID, StringComparison.CurrentCultureIgnoreCase)))
                    throw new JsMiracleException("员工编号已存在不得重复添加");

                base.SaveOrUpdate(entity);
            }
            else
            {
                var ent = this.GetEntity(entity.ID);

                if (ent == null)
                    throw new JsMiracleException("需修改的员工信息不存在");

                if (this.DbContext.IMS_UP_UserT.Any(
                        n => n.UserID.Equals(entity.UserID, StringComparison.CurrentCultureIgnoreCase)
                            && n.ID != entity.ID))
                    throw new JsMiracleException("员工编号已存在不得重复添加");

                if (entity.Password != ent.Password)
                {
                    if (IMS_UP_User.GetPwdMD5(entity.Password) != ent.Password)
                        entity.Password = IMS_UP_User.GetPwdMD5(entity.Password);
                }

                base.SaveOrUpdate(entity);
            }
        }

        public IList<IMS_UP_User> GetAllUserList(bool userNameFormatter = false)
        {
            var data = this.DbContext.IMS_UP_UserT.Where(n => n.State == (int)UserStateEnum.Normal).ToList();

            if (userNameFormatter)
            {
                data = data.Select(u => new IMS_UP_User
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

        public IMS_UP_User GetEntity(string userid)
        {
            if (string.IsNullOrEmpty(userid))
                throw new JsMiracleException(" userid 不得为空");

            var data = this.DbContext.IMS_UP_UserT.Where(n => n.UserID.Equals(userid, StringComparison.CurrentCultureIgnoreCase));

            return data.FirstOrDefault();
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

            var ents = this.DbContext.IMS_UP_UserT.Where(n => n.UserID == userID);

            if (ents.Count() == 0)
                return false;

            var user = ents.First();

            // md5 验证是否密码相同
            return IMS_UP_User.GetPwdMD5(password) == user.Password;
        }

        public void CreateUser(
            string userID, string userName, string password, string email)
        {
            IMS_UP_User entity = new IMS_UP_User();

            entity.Password = IMS_UP_User.GetPwdMD5(entity.Password);
            entity.UserID = userID;
            entity.UserName = userName;
            entity.CreateDate = System.DateTime.Now;
            entity.State = (int)UserStateEnum.Normal;

            this.SaveOrUpdate(entity);

            //this.Insert(entity);
        }

        public bool ChangePassword(string userID, string oldPassword, string newPassword)
        {
            var ent = this.DbContext.IMS_UP_UserT.Where(
                n => n.UserID.Equals(userID, StringComparison.CurrentCultureIgnoreCase));

            var user = ent.FirstOrDefault();

            if (user == null)
                throw new JsMiracleException("用户不存在");

            if (IMS_UP_User.GetPwdMD5(oldPassword) != user.Password)
                throw new JsMiracleException("旧密码不正确");

            user.Password = IMS_UP_User.GetPwdMD5(newPassword);
            SaveOrUpdate(user);
            return true;
        }
        #endregion
    }
}
