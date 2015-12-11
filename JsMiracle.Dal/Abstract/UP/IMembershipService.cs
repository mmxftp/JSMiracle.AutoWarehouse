using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace JsMiracle.Dal.Abstract.UP
{
    /// <summary>
    /// 用户管理的接口
    /// </summary>
    public interface IMembershipService
    {
        /// <summary>
        /// 最小密码长度
        /// </summary>
        int MinPasswordLength { get; }

        /// <summary>
        /// 验证用户登录是否成功
        /// </summary>
        /// <param name="userID">用户id</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        bool ValidateUser(string userID, string password);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="userID">用户id</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="email">电子邮箱</param>
        void CreateUser(string userID, string userName,string password, string email);

        /// <summary>
        /// 改变密码
        /// </summary>
        /// <param name="userID">用户id</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        bool ChangePassword(string userID , string oldPassword, string newPassword);
    }
}
