
using JsMiracle.Entities;
using JsMiracle.Entities.EasyUI_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract
{
    public interface IPermission
    {
        IList<TreeModel> GetPermissionInfo(string roleid);

        int SavePermission(bool check, int moduleid, int functionid, string roleid);
    }
}
