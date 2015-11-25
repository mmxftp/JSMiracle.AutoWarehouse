using JsMiracle.Entities.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.UP
{
    public interface IActionPermission
    {
        PermissionViewModule GetAllPermission();
        
        /// <summary>
        /// 刷新权限缓存
        /// </summary>
        void ResetCache();
    }
}
