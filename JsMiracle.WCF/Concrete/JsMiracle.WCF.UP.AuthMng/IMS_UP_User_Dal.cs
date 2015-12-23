using JsMiracle.Entities;
using JsMiracle.Entities.Enum;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Framework;
using JsMiracle.WCF.Interface;
using JsMiracle.WCF.UP.IAuthMng;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.ServiceModel;

namespace JsMiracle.WCF.UP.AuthMng
{
    public class IMS_UP_User_Dal : WcfDataLayerBase<IMS_UP_YH>, IUser, IMembershipService
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

                base.SaveEntity(entity);
            }
            else
            {
                var ent = base.FindEntityByKey(entity.ID);

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

                base.SaveEntity(entity);
            }

        }

        public List<IMS_UP_YH> GetAllUserList(bool userNameFormatter = false)
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

        public int MinPasswordLength()
        {
            return 1;
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

            var ent = FindEntityByKey(id);

            if (ent != null)
            {
                ent.ZT = (int)UserStateEnum.Delete;
                base.SaveOrUpdate(ent);
            }

        }

    }
   
    public class IMS_UP_User_WCF : WcfService<IMS_UP_YH>,IWcfService
    {
        IMS_UP_User_Dal dal = new IMS_UP_User_Dal();

        protected override IDataLayer<IMS_UP_YH> DataLayer
        {
            get { return dal; }
        }
        //public WcfResponse RequestOverride(WcfRequest req)
        //{
        //    return this.RequestFun(req);
        //}


        protected override WcfResponse RequestFun(WcfRequest req)
        {
            WcfResponse res = new WcfResponse();

            IMS_UP_YH ent;
            List<IMS_UP_YH> dataList;
            object id;
            string userid;
            int len;
            bool hasPermission;
            object[] objParams;

            switch (req.Head.RequestMethodName)
            {
                case "SaveOrUpdate":
                    ent = req.Body.GetParameters<IMS_UP_YH>();
                    dal.SaveOrUpdate(ent);
                    break;

                case "GetAllUserList":
                    bool isFormatter = (bool)req.Body.GetParameters<object>();
                    dataList = dal.GetAllUserList(isFormatter);
                    res.Body.SetBody(dataList);
                    break;

                case "GetEntityByYHBH":
                    userid = (string)req.Body.GetParameters<object>();
                    ent = dal.GetEntityByYHBH(userid);
                    res.Body.SetBody(ent);
                    break;

                case "Delete":
                    id = req.Body.GetParameters<object>();
                    dal.Delete(id);
                    break;

                case "MinPasswordLength":
                    len = dal.MinPasswordLength();
                    res.Body.SetBody(len);
                    break;

                case "ValidateUser":
                    objParams = req.Body.GetParameters<object[]>();
                    hasPermission = dal.ValidateUser((string)objParams[0], (string)objParams[1]);
                    res.Body.SetBody(hasPermission);
                    break;

                case "CreateUser":
                    objParams = req.Body.GetParameters<object[]>();
                    dal.CreateUser(
                        (string)objParams[0],
                        (string)objParams[1],
                        (string)objParams[2],
                        (string)objParams[3]);
                    break;

                case "ChangePassword":
                    objParams = req.Body.GetParameters<object[]>();
                    dal.ChangePassword(
                        (string)objParams[0],
                        (string)objParams[1],
                        (string)objParams[2]);
                    break;

                // 没有方法交给父类处理
                default:
                    return null;
            }

            res.Head.IsSuccess = true;
            return res;
        }

     
    }


}
