using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.UP
{
    public interface IRoleUser : IDataLayer<IMS_UP_JSYH>
    {
        IList<IMS_UP_YH> GetUserList(string roleid);

        int SaveRoleUser( string roleid , string userid);

        int RemoveRoleUser(string roleid , string userid);
    }
}
