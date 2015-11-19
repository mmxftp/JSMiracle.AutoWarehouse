using JsMiracle.Entities.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract
{
    public interface IActionPermission
    {
        PermissionViewModule GetAllPermission();

        void ResetCache();
    }
}
