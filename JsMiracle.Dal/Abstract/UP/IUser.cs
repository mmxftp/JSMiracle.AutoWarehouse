using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.UP
{
    public interface IUser : IDataLayer<IMS_UP_YH>
    {
        IList<IMS_UP_YH> GetAllUserList(bool userNameFormatter = false);

        /// <summary>
        /// 得到数据实例
        /// </summary>
        /// <param name="userid">员工号</param>
        /// <returns></returns>
        IMS_UP_YH GetEntityByYHBH(string userid);

        //IList<IMS_UP_User> GetUserList(int pageIndex, int pageSize, string txt, out int totalCount);

        //int Save(IMS_UP_User user);

        //int Remove(int id);
    }
}
