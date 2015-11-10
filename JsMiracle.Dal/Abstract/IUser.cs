using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract
{
    public interface IUser
    {
        IList<IMS_TB_UserInfo> GetAllUserList(bool userNameFormatter = false);

        IMS_TB_UserInfo GetEntity(int id);

        IList<IMS_TB_UserInfo> GetUserList(int pageIndex, int pageSize, string txt, out int totalCount);

        int Save(IMS_TB_UserInfo user);

        int Remove(int id);
    }
}
