//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;


//namespace JsMiracle.WebUI.Controllers.Filter
//{
//    public class AuthorizeFilterAttribute : AuthorizeAttribute
//    {
//        public override void OnAuthorization(AuthorizationContext filterContext)
//        {
//            //filterContext.ActionDescriptor 
//            //base.OnAuthorization(filterContext);

//            if (filterContext == null)
//                throw new ArgumentNullException("filterContext");

//            var path = filterContext.HttpContext.Request.Path.ToLower();

//            if (path == "/" || string.Equals(path, "/Home/Login", StringComparison.CurrentCultureIgnoreCase))
//                return;//忽略对Login登录页的权限判定

//            object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(ViewPageAttribute), true);
//            var isViewPage = attrs.Length == 1;//当前Action请求是否为具体的功能页

//            if (this.AuthorizeCore(filterContext, isViewPage) == false)//根据验证判断进行处理
//            {
//                //注：如果未登录直接在URL输入功能权限地址提示不是很友好；如果登录后输入未维护的功能权限地址，那么也可以访问，这个可能会有安全问题
//                if (isViewPage == true)
//                {
//                    filterContext.Result = new HttpUnauthorizedResult();//直接URL输入的页面地址跳转到登陆页
//                }
//                else
//                {
//                    filterContext.Result = new ContentResult 
//                        {
//                            Content = @"$.messager.alert('系统信息', '抱歉,你不具有当前操作的权限!', 'info')" 
//                        };
//                }
//            }
//        }

//        //权限判断业务逻辑
//        protected virtual bool AuthorizeCore(AuthorizationContext filterContext, bool isViewPage)
//        {
//            if (filterContext.HttpContext == null)
//            {
//                throw new ArgumentNullException("httpContext");
//            }

//            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
//            {
//                return false;//判定用户是否登录
//            }

//            var user = new CurrentUser();//获取当前用户信息
//            var controllerName = filterContext.RouteData.Values["controller"].ToString();
//            var actionName = filterContext.RouteData.Values["action"].ToString();
//            if (isViewPage && (controllerName.ToLower() != "main" && actionName.ToLower() != "masterpage"))//如果当前Action请求为具体的功能页并且不是MasterPage页
//            {
//                if (user.MenuPermission.Count(m => m.ControllerName == controllerName && m.ActionName == actionName) == 0)
//                    return false;
//            }
//            else
//            {
//                var actions = ContainerFactory.GetContainer().Resolve<IAuthorityFacade>().GetAllActionPermission();//所有被维护的Action权限
//                if (actions.Count(a => a.ControllerName == controllerName && a.ActionName == actionName) != 0)//如果当前Action属于被维护的Action权限
//                {
//                    if (user.ActionPermission.Count(a => a.ControllerName == controllerName && a.ActionName == actionName) == 0)
//                        return false;
//                }
//            }
//            return true;
//        }
//    }

//}