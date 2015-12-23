using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Framework.FormAuth
{
    /// <summary>
    /// 用户登录接口
    /// </summary>
    public interface IFormsAuthentication
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userID">已验证的员工编号</param>
        /// <param name="createPersistentCookie">如果为 true，则创建持久 Cookie（跨浏览器会话保存的 Cookie）</param>
        void SignIn(string userID, bool createPersistentCookie);

        /// <summary>
        /// 登出
        /// </summary>
        void SignOut();
    }
}
