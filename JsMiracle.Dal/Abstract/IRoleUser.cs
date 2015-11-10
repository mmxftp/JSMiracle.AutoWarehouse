using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract
{
    public interface IRoleUser
    {
        IList<IMS_TB_UserInfo> GetUserList(string roleid);

        int SaveRoleUser( string roleid , string userid);

        int RemoveRoleUser(string roleid , string userid);
    }
}
