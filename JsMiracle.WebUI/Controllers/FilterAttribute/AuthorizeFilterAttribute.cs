using JsMiracle.Dal.Abstract;
using JsMiracle.Dal.Abstract.UP;
using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Framework.Cache;
using JsMiracle.WebUI.CommonSupport;
using JsMiracle.WebUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace JsMiracle.WebUI.Controllers.Filter
{
    /// <summary>
    /// 权限验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeFilterAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (CurrentUser.IsAdmin)
                return true;

            try
            {
                CurrentUser user = CurrentUser.GetCurrentUser();
                // 得所有配置的权限
                var permissions = WindsorContaineFactory.GetContainer().Resolve<IActionPermission>().GetAllPermission();

                bool validating = true;

                // 只有已配置的权限才是需要验证的权限,否则不验证
                if (permissions.ExistsPermissions(controllerName, actionName))
                {
                    validating = user.Permissions.ExistsPermissions(controllerName, actionName);
                }
            }
            catch
            {
                return false;
            }

            //return validating;
            return true;
        }

        private string controllerName;
        private string actionName;

        // 是否需要权限判断
        private bool isAuthViewPage;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            this.controllerName = filterContext.RouteData.Values["controller"].ToString();
            this.actionName = filterContext.RouteData.Values["action"].ToString();
            object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthViewPageAttribute), true);
            isAuthViewPage = attrs.Length == 1;  //当前Action请求是否为具体的功能页(是否要进行权限判断)

            base.OnAuthorization(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            if (isAuthViewPage)
            {
                var wcFactory = WindsorContaineFactory.GetContainer();
                var formAuth = wcFactory.Resolve<IFormsAuthentication>();
                var cache = wcFactory.Resolve<ICache>();

                // 当前用户登出
                formAuth.SignOut();
                cache.RemoveSessionCache(HttpContext.Current.User.Identity.Name);

                //filterContext.Result = new RedirectResult("/Account/LogIn");
                //filterContext.Result = new HttpUnauthorizedResult();

                string msg = string.Format(
                    @"<script type=""text/javascript"">
                            window.location = '{0}'
                      </script>",  "/Account/LogIn");

                filterContext.Result = new ContentResult { Content = msg };
            }
            else
            {

                string msg = string.Format(@"
                                <script type=""text/javascript"">
                                 $.messager.alert('权限错误','抱歉,你不具有当前操作的权限！controller:{0},action:{1}');
                                </script>", controllerName, actionName);

                //filterContext.Result = new ContentResult { Content = msg };

                filterContext.Result = new ContentResult { Content = msg };


                //filterContext.Result = new JsonResult()
                //{
                //    Data = ret
                //};
            }
            
        }
    }

}