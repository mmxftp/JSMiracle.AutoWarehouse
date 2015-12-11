using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.UP
{
    /// <summary>
    /// 角色用户关系接口
    /// </summary>
    public interface IRoleUser : IDataLayer<IMS_UP_JSYH>
    {
        /// <summary>
        /// 根据角色得用户信息列表
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        IList<IMS_UP_YH> GetUserList(string roleid);

        /// <summary>
        /// 保存用户角色关系
        /// </summary>
        /// <param name="roleid">角色</param>
        /// <param name="userid">用户</param>
        /// <returns></returns>
        int SaveRoleUser( string roleid , string userid);

        /// <summary>
        /// 移除用户角色关系
        /// </summary>
        /// <param name="roleid">角色</param>
        /// <param name="userid">用户</param>
        /// <returns>移除数量</returns>
        int RemoveRoleUser(string roleid , string userid);
    }
}
