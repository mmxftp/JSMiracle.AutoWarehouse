using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.UP
{
    /// <summary>
    /// 用户信息接口
    /// </summary>
    public interface IUser : IDataLayer<IMS_UP_YH>
    {
        /// <summary>
        /// 得所有用户信息
        /// </summary>
        /// <param name="userNameFormatter">是否对用户名有特定格式要用</param>
        /// <returns></returns>
        IList<IMS_UP_YH> GetAllUserList(bool userNameFormatter = false);

        /// <summary>
        /// 得到数据实例
        /// </summary>
        /// <param name="userid">员工号</param>
        /// <returns></returns>
        IMS_UP_YH GetEntityByYHBH(string userid);
    }
}
