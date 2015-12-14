using JsMiracle.Framework;
using JsMiracle.Framework.Log;
using JsMiracle.WebUI.Controllers.Filter;
using JsMiracle.WebUI.Infrastructure;
using System;
using System.Web.Mvc;

namespace JsMiracle.WebUI.Controllers.FilterAttribute
{
    /// <summary>
    /// 用于处理controller中的未解决的异常显示
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ExceptionFilterAttribute : ActionFilterAttribute
    {
        // 对应配置文件中的<loggerToMatch value="LogWebUI" />
        const string logName = "LogWebUI";

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                ILog log = WindsorContaineFactory.GetContainer().Resolve<ILog>();

                var controllerName = filterContext.RouteData.Values["controller"].ToString();
                var actionName = filterContext.ActionDescriptor.ActionName;
                LogEntity logEnt = new LogEntity()
                {
                    ClassName = controllerName,
                    MethodName = actionName,
                    Level = LogLevel.ERROR,
                    LogDetail = filterContext.Exception.Message + '|' + filterContext.Exception.StackTrace
                };
                log.WriteLog(logName, logEnt);


                var attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthViewPageAttribute), true);
                // 是页面请求才处理
                if (attrs.Length > 0)
                {
                    string msgTmp;
                    if (filterContext.Exception is JsMiracleException)//已知异常 ，就不出详细异常信息了
                        msgTmp = @"<script type=""text/javascript"">
                                                    $.messager.alert('系统错误','<b>系统错误</b><br/>{0}','error');
                                                </script>";
                    else
                        msgTmp = @"<script type=""text/javascript"">
                                                    $.messager.alert('未知错误','<b>异常消息:</b>{0}</p><b>触发Action:</b>{1}</p><b>异常类型:</b>{2}','error');
                                                </script>";


                    var excResult = new ContentResult();
                    excResult.Content = string.Format(msgTmp
                        , filterContext.Exception.GetBaseException().Message
                        , filterContext.ActionDescriptor.ActionName
                        , filterContext.Exception.GetBaseException().GetType().ToString());

                    filterContext.Result = excResult;

                    filterContext.ExceptionHandled = true;
                    return;
                }
            }

            base.OnActionExecuted(filterContext);
        }

    }
}