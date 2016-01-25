using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.Enum
{
    /// <summary>
    /// 工作任务状态
    /// </summary>
    public enum EnumWorkTaskState
    {
        /// <summary>
        /// 创建
        /// </summary>
        WTSTS_New = 0,

        /// <summary>
        /// 已激活
        /// </summary>
        WTSTS_Actived = 1,

        /// <summary>
        /// 执行中
        /// </summary>
        WTSTS_Running = 2,

        /// <summary>
        /// 暂停
        /// </summary>
        WTSTS_Pause = 3,

        /// <summary>
        /// 异常
        /// </summary>
        WTSTS_Abnormal = 4,

        /// <summary>
        /// 已完成
        /// </summary>
        WTSTS_Completed = 5,

        /// <summary>
        /// 取消
        /// </summary>
        WTSTS_Cancel = 6


    }
}
