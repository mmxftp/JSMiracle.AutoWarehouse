using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Framework.Log
{
    public class Net4Log : ILog
    {
        log4net.ILog log = null;

        public void WriteLog(LogEnum name, ILogEntity logEntity)
        {
            WriteLog(name, logEntity.Level, logEntity.GetLogContext());            
        }

        public void WriteLog(LogEnum name, LogLevel level, string logContent)
        {
            log = log4net.LogManager.GetLogger(name.ToString());
            //log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            switch (level)
            {
                case LogLevel.DEBUG:
                    log.Debug(logContent);
                    break;
                case LogLevel.ERROR:
                    log.Error(logContent);
                    break;
                case LogLevel.FATAL:
                    log.Fatal(logContent);
                    break;
                case LogLevel.INFO:
                    log.Info(logContent);
                    break;
                case LogLevel.WARN:
                    log.Warn(logContent);
                    break;
                default:
                    log.Debug(logContent);
                    break;
            }
        }
    }
}
