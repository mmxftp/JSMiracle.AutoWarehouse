
using JsMiracle.Entities;
using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.UP
{
    /// <summary>
    /// 权限验证
    /// </summary>
    public interface IPermission : IDataLayer<IMS_UP_JSMK>
    {
        /// <summary>
        /// 按角色得到权限信息
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        IList<TreeModel> GetPermissionInfo(string roleid);

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="check">加入(check==true)/移除(check==false) 权限</param>
        /// <param name="parentid">父节点 没有值填(-1)</param>
        /// <param name="moduleid">模块节点 没有值填(-1)</param>
        /// <param name="functionid">功能节点 没有值填(-1)</param>
        /// <param name="roleid">角色编号</param>
        /// <returns></returns>
        int SavePermission(bool check, int parentid, int moduleid, int functionid, string roleid);

        /// <summary>
        /// 按用户id得权限
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        PermissionViewModule GetPermissionListByUserID(string userid);

        /// <summary>
        /// 得所有权限信息
        /// </summary>
        /// <returns></returns>
        PermissionViewModule GetAllPermission();

        /// <summary>
        /// 按角色得权限
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        List<IMS_UP_JSMK> GetPermissionListByRoleID(string roleid);

    }
}
