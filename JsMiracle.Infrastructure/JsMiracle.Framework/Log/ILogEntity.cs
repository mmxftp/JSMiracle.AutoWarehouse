using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Framework.Log
{
    public interface ILogEntity
    {
        /// <summary>
        /// 异常级别
        /// </summary>
        LogLevel Level { get; set; }

        string GetLogContext();
    }


    public class LogEntity:ILogEntity
    {
        public LogLevel Level { get; set; }

        public string ClassName { get; set; }

        public string MethodName { get; set; }


        public string LogDetail { get; set; }


        public string GetLogContext()
        {
            if (!string.IsNullOrEmpty(LogDetail))
            {
                // 逗号改为分号,使得cvs数据对应格式不会错误
                LogDetail = LogDetail.Replace(',', ';').Replace('\r',' ').Replace('\n',' ');
            }

            return string.Format("{0},{1},{2},{3}",Level, ClassName, MethodName, LogDetail);
        }
    }
}
