using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract
{
    public interface IRole
    {
        IList<IMS_TB_RoleInfo> GetRoleList(int pageIndex, int pageSize);

        int Save(IMS_TB_RoleInfo module);

        int Remove(int id);

        IMS_TB_RoleInfo GetEntity(int id);

        IList<IMS_TB_RoleInfo> GetAllRole();

    }
}
