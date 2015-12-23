using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Framework.Log
{
    /// <summary>
    /// 日志操作
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// 写日志 
        /// </summary>
        /// <param name="name">名称(日志处理的标识或文件名)</param>
        /// <param name="logEntity">日志明细</param>
        void WriteLog(LogEnum name, ILogEntity logEntity);

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="name">名称(日志处理的标识或文件名)</param>
        /// <param name="level">日志级别</param>
        /// <param name="logContent">日志内容</param>
        void WriteLog(LogEnum name, LogLevel level, string logContent);
    }





}
