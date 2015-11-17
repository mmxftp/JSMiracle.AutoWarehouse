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

        /// <summary>
        /// 得到数据实例
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="userid">员工号</param>
        /// <returns></returns>
        IMS_TB_UserInfo GetEntity(int? id,string userid = null);

        IList<IMS_TB_UserInfo> GetUserList(int pageIndex, int pageSize, string txt, out int totalCount);

        int Save(IMS_TB_UserInfo user);

        int Remove(int id);
    }
}
