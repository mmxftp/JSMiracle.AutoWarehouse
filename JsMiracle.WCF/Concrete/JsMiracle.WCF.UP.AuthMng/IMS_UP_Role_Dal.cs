using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Framework;
using JsMiracle.WCF.Interface;
using JsMiracle.WCF.UP.IAuthMng;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsMiracle.WCF.UP.AuthMng
{
    public class IMS_UP_Role_Dal : WcfDataLayerBase<IMS_UP_JS>, IRole
    {

        public List<IMS_UP_JS> GetAllRole()
        {
            return this.DbContext.IMS_UP_JS_S.ToList();
        }


        public override void Delete(object id)
        {

            var role = FindEntityByKey(id);
            if (role != null)
            {
                // 得用户角色集合
                var userRoleList = this.DbContext.IMS_UP_JSYH_S.Where(
                    n => n.JSID.Equals(role.JSID, StringComparison.CurrentCultureIgnoreCase));

                // 得角色权集合
                var rolePermissionList = this.DbContext.IMS_UP_JSMK_S.Where(
                    n => n.JSID.Equals(role.JSID, StringComparison.CurrentCultureIgnoreCase));

                int effRowCount = 0;

                using (var tran = this.DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        // 删除用户角色关联
                        this.DbContext.IMS_UP_JSYH_S.RemoveRange(userRoleList);
                        // 删除角色权限关联
                        this.DbContext.IMS_UP_JSMK_S.RemoveRange(rolePermissionList);
                        // 删除角色
                        this.DbContext.IMS_UP_JS_S.Remove(role);
                        effRowCount = this.DbContext.SaveChanges();

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                    }
                }
            }

        }
    }

    public class IMS_UP_Role_WCF : WcfService<IMS_UP_JS>, IWcfService
    {
        IMS_UP_Role_Dal dal = new IMS_UP_Role_Dal();

        protected override IDataLayer<IMS_UP_JS> DataLayer
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

            object id;
            List<IMS_UP_JS> dataList;

            switch (req.Head.RequestMethodName)
            {
                case "GetAllRole":
                    dataList = dal.GetAllRole();
                    res.Body.SetBody(dataList);
                    break;

                case "Delete":
                    id = req.Body.GetParameters<object>();
                    dal.Delete(id);
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
