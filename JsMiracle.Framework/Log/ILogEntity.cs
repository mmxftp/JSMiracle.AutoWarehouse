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
            return string.Format("{0},{1},{2},{3}",Level, ClassName, MethodName, LogDetail);
        }
    }
}
