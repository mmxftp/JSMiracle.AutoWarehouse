using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract
{
    public interface IRoleUser : IDataLayer<IMS_UP_RoUr>
    {
        IList<IMS_UP_User> GetUserList(string roleid);

        int SaveRoleUser( string roleid , string userid);

        int RemoveRoleUser(string roleid , string userid);
    }
}
