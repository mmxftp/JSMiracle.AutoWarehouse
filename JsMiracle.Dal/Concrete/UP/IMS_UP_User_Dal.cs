using JsMiracle.Dal.Abstract;
using JsMiracle.Dal.Abstract.UP;
using JsMiracle.Entities;
using JsMiracle.Entities.Enum;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsMiracle.Dal.Concrete.UP
{
    public class IMS_UP_User_Dal : DataLayerBase<IMS_UP_YH>, IUser, IMembershipService
    {
        public override void SaveOrUpdate(IMS_UP_YH entity)
        {
            if (entity.ID == 0)
            {
                entity.ZT = (int)UserStateEnum.Normal;
                entity.CJRQ = DateTime.Now;
                // 密码逻辑由外部定义,此处只做保存
                //entity.Password = IMS_UP_User.GetPwdMD5(entity.Password);

                if (this.DbContext.IMS_UP_YH_S.Any(
                    n => n.YHID.Equals(entity.YHID, StringComparison.CurrentCultureIgnoreCase)))
                    throw new JsMiracleException("员工编号已存在不得重复添加");

                base.SaveOrUpdate(entity);
            }
            else
            {
                var ent = this.GetEntity(entity.ID);

                if (ent == null)
                    throw new JsMiracleException("需修改的员工信息不存在");

                if (this.DbContext.IMS_UP_YH_S.Any(
                        n => n.YHID.Equals(entity.YHID, StringComparison.CurrentCultureIgnoreCase)
                            && n.ID != entity.ID))
                    throw new JsMiracleException("员工编号已存在不得重复添加");

                if (entity.MM != ent.MM)
                {
                    if (IMS_UP_YH.GetPwdMD5(entity.MM) != ent.MM)
                        entity.MM = IMS_UP_YH.GetPwdMD5(entity.MM);
                }

                base.SaveOrUpdate(entity);
            }
        }

        public IList<IMS_UP_YH> GetAllUserList(bool userNameFormatter = false)
        {
            var data = this.DbContext.IMS_UP_YH_S.Where(n => n.ZT == (int)UserStateEnum.Normal).ToList();

            if (userNameFormatter)
            {
                data = data.Select(u => new IMS_UP_YH
                {
                    YHM = string.Format("{0}({1})", u.YHM, u.YHID),
                    YHID = u.YHID,
                    ID = u.ID,
                    CJRQ = u.CJRQ,
                    XGRQ = u.XGRQ,
                    DY = u.DY,
                    MM = u.MM,
                    ZT = u.ZT
                }).ToList();
            }


            return data;
        }

        public IMS_UP_YH GetEntityByYHBH(string userid)
        {
            if (string.IsNullOrEmpty(userid))
                throw new JsMiracleException(" userid 不得为空");

            var data = this.DbContext.IMS_UP_YH_S.Where(n => n.YHID.Equals(userid, StringComparison.CurrentCultureIgnoreCase));

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

            var ents = this.DbContext.IMS_UP_YH_S.Where(n => n.YHID == userID);

            if (ents.Count() == 0)
                return false;

            var user = ents.First();

            // md5 验证是否密码相同
            return IMS_UP_YH.GetPwdMD5(password) == user.MM;
        }

        public void CreateUser(
            string userID, string userName, string password, string email)
        {
            IMS_UP_YH entity = new IMS_UP_YH();

            entity.MM = IMS_UP_YH.GetPwdMD5(entity.MM);
            entity.YHID = userID;
            entity.YHM = userName;
            entity.CJRQ = System.DateTime.Now;
            entity.ZT = (int)UserStateEnum.Normal;

            this.SaveOrUpdate(entity);

            //this.Insert(entity);
        }

        public bool ChangePassword(string userID, string oldPassword, string newPassword)
        {
            var ent = this.DbContext.IMS_UP_YH_S.Where(
                n => n.YHID.Equals(userID, StringComparison.CurrentCultureIgnoreCase));

            var user = ent.FirstOrDefault();

            if (user == null)
                throw new JsMiracleException("用户不存在");

            if (IMS_UP_YH.GetPwdMD5(oldPassword) != user.MM)
                throw new JsMiracleException("旧密码不正确");

            user.MM = IMS_UP_YH.GetPwdMD5(newPassword);
            SaveOrUpdate(user);
            return true;
        }
        #endregion

        public override void Delete(object id)
        {
            var ent = GetEntity(id);

            if (ent != null)
            {
                ent.ZT = (int)UserStateEnum.Delete;
                base.SaveOrUpdate(ent);
            }
        }
    }
}
