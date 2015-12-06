
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
    public interface IPermission : IDataLayer<IMS_UP_JSMK>
    {
        IList<TreeModel> GetPermissionInfo(string roleid);

        int SavePermission(bool check, int parentid, int moduleid, int functionid, string roleid);

        PermissionViewModule GetPermissionListByUserID(string userid);

        PermissionViewModule GetAllPermission();

        List<IMS_UP_JSMK> GetPermissionListByRoleID(string roleid);

    }
}
