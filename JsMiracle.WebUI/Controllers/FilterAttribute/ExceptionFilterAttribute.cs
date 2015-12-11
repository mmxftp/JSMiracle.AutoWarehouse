using JsMiracle.Framework;
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
//        public override void OnActionExecuted(ActionExecutedContext filterContext)
//        {
//            if (filterContext.Exception != null)
//            {
//                string msgTmp;
//                if (filterContext.Exception is JsMiracleException)//已知异常 ，就不出详细异常信息了
//                    msgTmp = @"<script type=""text/javascript"">
//                                $.messager.alert('系统错误','<b>系统错误</b><br/>{0}','error');
//                            </script>";
//                else
//                    msgTmp = @"<script type=""text/javascript"">
//                                $.messager.alert('未知错误','<b>异常消息:</b>{0}</p><b>触发Action:</b>{1}</p><b>异常类型:</b>{2}','error');
//                            </script>";


//                var excResult = new ContentResult();
//                excResult.Content = string.Format(msgTmp
//                    , filterContext.Exception.GetBaseException().Message
//                    , filterContext.ActionDescriptor.ActionName
//                    , filterContext.Exception.GetBaseException().GetType().ToString());

//                filterContext.Result = excResult;

//                filterContext.ExceptionHandled = true;
//            }
//        }

        //        public override void OnActionExecuting(ActionExecutingContext filterContext)
        //        {
        //            if (filterContext.HttpConte != null)
        //            {
        //                string msgTmp;
        //                if (filterContext.Exception is JsMiracleException)//已知异常 ，就不出详细异常信息了
        //                    msgTmp = @"<script type=""text/javascript"">
        //                                $.messager.alert('','<b>系统错误</b><br/>{0}','error');
        //                            </script>";
        //                else
        //                    msgTmp = @"<script type=""text/javascript"">
        //                                $.messager.alert('','<b>异常消息:</b>{0}</p><b>触发Action:</b>{1}</p><b>异常类型:</b>{2}','error');
        //                            </script>";


        //                var excResult = new ContentResult();
        //                excResult.Content = string.Format(msgTmp, filterContext.Exception.GetBaseException().Message);
        //                filterContext.Result = excResult;

        //                filterContext.ExceptionHandled = true;
        //            }

        //            base.OnActionExecuting(filterContext);
        //        }
    }
}