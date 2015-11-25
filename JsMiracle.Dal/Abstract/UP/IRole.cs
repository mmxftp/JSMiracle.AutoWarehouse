using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.UP
{
    public interface IRole : IDataLayer<IMS_UP_JS>
    {
        //IList<IMS_UP_Role> GetRoleList(int pageIndex, int pageSize);

        //int Save(IMS_UP_Role module);

        //int Remove(int id);

        //IMS_UP_Role GetEntity(int id);

        IList<IMS_UP_JS> GetAllRole();

    }
}
