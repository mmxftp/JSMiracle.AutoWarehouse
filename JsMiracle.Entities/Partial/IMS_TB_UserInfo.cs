using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

// 扩展属性,命名空间必须一致
namespace JsMiracle.Entities
{
    public partial class IMS_TB_UserInfo
    {
        public static string GetPwdMD5(string pwd)
        {            
            return FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
        }
    }
}
