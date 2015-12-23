using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Framework.Log
{
    /// <summary>
    /// 日志名称枚举, 其中每一项必须对应log4net.config中的 &lt;loggerToMatch value="" /&gt; value中的值
    /// </summary>
    public enum LogEnum
    {
        /// <summary>
        /// 界面层日志
        /// </summary>
        LogWebUI,

        /// <summary>
        /// 配置层日志 
        /// </summary>
        WcfConfig



    }
}
