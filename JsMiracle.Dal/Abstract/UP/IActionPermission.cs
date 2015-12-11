using JsMiracle.Entities.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.UP
{
    /// <summary>
    /// 处理权限的接口
    /// </summary>
    public interface IActionPermission
    {
        /// <summary>
        /// 得到所有需处理的权限集合
        /// </summary>
        /// <returns></returns>
        PermissionViewModule GetAllPermission();
        
        /// <summary>
        /// 刷新权限缓存
        /// </summary>
        void ResetCache();
    }
}
