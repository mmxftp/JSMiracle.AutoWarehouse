using JsMiracle.Dal.Abstract.UP;
using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsMiracle.Dal.Concrete.UP
{
    public class IMS_UP_Role_Dal : DataLayerBase<IMS_UP_JS>, IRole
    {

        public IList<IMS_UP_JS> GetAllRole()
        {
            return this.DbContext.IMS_UP_JS_S.ToList();
        }

        public override void Delete(object id)
        {
            var role = GetEntity(id);
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
}
