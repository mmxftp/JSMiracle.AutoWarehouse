using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JsMiracle.Dal.Concrete
{
    public class IMS_TB_RoleInfo_Dal : DataLayer<IMS_TB_RoleInfo>, IRole
    {
        //public IMS_TB_RoleInfo_Dal(IIMS_ORGEntities context)
        //    : base(context)
        //{
        //}

        //private IMS_TB_RoleInfo Update(IMS_TB_RoleInfo entity)
        //{

        //    IMS_TB_RoleInfo ent = null;
        //    if (entity.ID == 0)
        //    {
        //        ent = DataLayerBase.Insert(this, entity);
        //    }
        //    else
        //    {
        //        ent = IMS_TB_RoleInfoSet.Find(entity.ID);
        //        if (ent != null)
        //        {
        //            ent.ID = entity.ID;
        //            ent.RoleID = entity.RoleID;
        //            ent.RoleName = entity.RoleName;
        //            ent.Description = entity.Description;
        //            ent = DataLayerBase.Update(this, ent);
        //        }
        //    }
        //    return ent;
        //}

        //public IList<IMS_TB_RoleInfo> GetRoleList(int pageIndex, int pageSize)
        //{
        //    int totalCount = 0;

        //    Expression<Func<IMS_TB_RoleInfo, bool>> filter = null;

        //    var dataList = DataLayerBase.GetDataByPage(
        //        this,
        //        p => p.RoleName,
        //        filter, pageIndex, pageSize, out totalCount);

        //    return dataList;
        //}

        //public int Save(IMS_TB_RoleInfo module)
        //{
        //    if (string.IsNullOrEmpty(module.RoleID))
        //    {
        //        module.RoleID = Guid.NewGuid().ToString();
        //    }

        //    Update(module);
        //    return 1;
        //}

        //public int Remove(int id)
        //{
        //    var ent = IMS_TB_RoleInfoSet.Find(id);

        //    if (ent == null)
        //        return 0;

        //    DataLayerBase.Delete(this, ent);
        //    return 1;
        //}


        //public IMS_TB_RoleInfo GetEntity(int id)
        //{
        //    return IMS_TB_RoleInfoSet.Find(id);
        //}


        public IList<IMS_TB_RoleInfo> GetAllRole()
        {
            return this.DbContext.IMS_TB_RoleInfoSet.ToList();
        }

        public override void Delete(long id)
        {
            var role = GetEntity(id);
            if (role != null)
            {
                // 得用户角色集合
                var userRoleList = this.DbContext.IMS_TB_RoleUserSet.Where(
                    n => n.RoleID.Equals(role.RoleID, StringComparison.CurrentCultureIgnoreCase));

                // 得角色权集合
                var rolePermissionList = this.DbContext.IMS_TB_PermissionSet.Where(
                    n => n.RoleId.Equals(role.RoleID, StringComparison.CurrentCultureIgnoreCase));

                int effRowCount = 0;

                using (var tran = this.DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        // 删除用户角色关联
                        this.DbContext.IMS_TB_RoleUserSet.RemoveRange(userRoleList);
                        // 删除角色权限关联
                        this.DbContext.IMS_TB_PermissionSet.RemoveRange(rolePermissionList);
                        // 删除角色
                        this.DbContext.IMS_TB_RoleInfoSet.Remove(role);
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
