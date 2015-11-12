using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Models
{
    public enum UserStateEnum:int
    {
        /// <summary>
        /// 已删除
        /// </summary>
        None = 0,
        Delete = 0,

        /// <summary>
        /// 可用的
        /// </summary>
        Normal = 1,
    }

}
