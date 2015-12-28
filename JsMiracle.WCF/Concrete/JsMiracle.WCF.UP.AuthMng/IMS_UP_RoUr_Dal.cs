
using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using JsMiracle.WCF.UP.IAuthMng;
using JsMiracle.WCF.WcfBaseService;
using JsMiracle.WCF.Interface;
using JsMiracle.Framework;
using JsMiracle.Entities.WCF;

namespace JsMiracle.WCF.UP.AuthMng
{
    public class IMS_UP_RoUr_Dal : WcfDataLayerBase<IMS_UP_JSYH>, IRoleUser
    {

        public List<IMS_UP_YH> GetUserList(string roleid)
        {
            

            var data = from r in this.DbContext.IMS_UP_JSYH_S
                       join u in this.DbContext.IMS_UP_YH_S
                       on r.YHID equals u.YHID
                       where r.JSID == roleid
                       select u;

            // linq 选择数据时候 不能new 已知的对象，只能匿名的。 
            // 但是如果从一个 List 列表 就可以new 已知的类。
            var lst = data.ToList().Select(u => new IMS_UP_YH
                       {
                           ID = u.ID,
                           YHID = u.YHID,
                           YHM = string.Format("{0}({1})", u.YHM, u.YHID),
                           //UserName = u.UserName,
                           CJRQ = u.CJRQ,
                           DY = u.DY,
                           XGRQ = u.XGRQ,
                           MM = u.MM
                       }).ToList();


            return lst;
        }

        public int SaveRoleUser(string roleid, string userid)
        {
            var ents =
                this.DbContext.IMS_UP_JSYH_S.Where(n => n.JSID == roleid
                                && n.YHID == userid);


            if (ents != null && ents.Count() > 0)
                return 0;


            var ent = new IMS_UP_JSYH()
            {
                JSID = roleid,
                YHID = userid
            };

            Insert(ent);
            return 0;
        }

        public int RemoveRoleUser(string roleid, string userid)
        {
            var ents =
                this.DbContext.IMS_UP_JSYH_S.Where(n => n.JSID == roleid
                     && n.YHID == userid).ToList();

            if (ents == null || ents.Count == 0)
                return 0;

            int effectRowCount = 0;
            foreach (var u in ents)
            {
                Delete(u.ID);
                effectRowCount++;
            }
            return effectRowCount;
        }
    }

    public class IMS_UP_RoleUser_WCF : WcfService<IMS_UP_JSYH>, IWcfRoleUser
    {

        IMS_UP_RoUr_Dal dal = new IMS_UP_RoUr_Dal();

        protected override IDataLayer<IMS_UP_JSYH> DataLayer
        {
            get { return dal; }
        }

        public WcfResponse RequestOverride(WcfRequest req)
        {
            return this.RequestFun(req);
        }

        protected override WcfResponse RequestFun(WcfRequest req)
        {
            WcfResponse res = new WcfResponse();
            object[] objs;

            switch (req.Head.RequestMethodName)
            {
                case "GetUserList":
                    objs = (object[])req.Body.Parameters;
                    res.Body.Data = dal.GetUserList((string)objs[0]);
                    break;

                case "SaveRoleUser":
                    objs = (object[])req.Body.Parameters;
                    res.Body.Data = dal.SaveRoleUser((string)objs[0], (string)objs[1]);
                    break;

                case "RemoveRoleUser":
                    objs = (object[])req.Body.Parameters;
                    res.Body.Data = dal.RemoveRoleUser((string)objs[0], (string)objs[1]);
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
