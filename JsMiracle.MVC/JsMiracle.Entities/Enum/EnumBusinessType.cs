using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace JsMiracle.Entities.Enum
{
    /// <summary>
    /// 业务类型
    /// </summary>
    public enum EnumBusinessType
    {
        /// <summary>
        /// 未知类型
        /// </summary>   
        None = 0,

        /// <summary>
        /// 入库
        /// </summary>
        InStorage = 1,

        /// <summary>
        /// 出库
        /// </summary>
        OutputStorage = 2 ,
    }
}
