using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.Enum
{
    public enum EnumOrderDataState
    {
        /// <summary>
        /// 默认
        /// </summary>
        None = -1,
        /// <summary>
        /// 新建
        /// </summary>
        VLSTS_New = 0,

        /// <summary>
        /// 就绪
        /// </summary>
        VLSTS_Ready = 1,

        /// <summary>
        /// 已释放
        /// </summary>
        VLSTS_Released = 2,

        /// <summary>
        /// 已完成
        /// </summary>
        VLSTS_Completed = 3,

        /// <summary>
        /// 已取消
        /// </summary>
        VLSTS_Cancel = 4,



    }
}
