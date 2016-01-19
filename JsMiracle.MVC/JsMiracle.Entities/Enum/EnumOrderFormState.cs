using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.Enum
{
    public enum EnumOrderFormState
    {
        /// <summary>
        /// 新建
        /// </summary>
        VHSTS_New = 0,

        /// <summary>
        /// 就绪
        /// </summary>
        VHSTS_Submit = 1,

        /// <summary>
        /// 已释放
        /// </summary>
        VHSTS_Ready = 2,

        /// <summary>
        /// 已完成
        /// </summary>
        VHSTS_Completed = 3,

        /// <summary>
        /// 已取消
        /// </summary>
        VHSTS_Cancel = 4,

        /// <summary>
        /// 已关闭
        /// </summary>
        VHSTS_Closed = 5

    }
}
