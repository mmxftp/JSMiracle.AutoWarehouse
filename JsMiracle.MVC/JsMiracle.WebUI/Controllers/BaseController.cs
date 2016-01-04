using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Framework;
using JsMiracle.WebUI.CommonSupport;
using JsMiracle.WebUI.Controllers.Filter;
using JsMiracle.WebUI.Controllers.FilterAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JsMiracle.WebUI.Controllers
{
    [AuthorizeFilter]
    [ExceptionFilter]
    public class BaseController : Controller
    {
        public BaseController() { }

        [AllowAnonymous]
        public virtual ActionResult GetPermissionFunction(string actionName)
        {
            //ViewContext.RouteData.DataTokens["area"].ToString().ToLower();
            //ControllerContext.RouteData.DataTokens["area"]


            var areaName = Convert.ToString(ControllerContext.RouteData.DataTokens["area"]);
            var controllerName = this.RouteData.Values["controller"].ToString();
            //var actionName = this.RouteData.Values["action"].ToString();

            if (string.Equals(controllerName, "Account", StringComparison.CurrentCultureIgnoreCase))
                return JavaScript("");


            var user = CurrentUser.GetCurrentUser();

            var userFunList = user.Permissions.GetFunctionList(areaName, controllerName, actionName);

            if (userFunList == null)
                userFunList = new List<Entities.TabelEntities.IMS_UP_MKGN>();

            // 得所有配置的权限
            var permissions = ActionPermission.GetAllPermission();

            var allFunList = permissions.GetFunctionList(areaName, controllerName, actionName);

            //var controlsDisabledDictionary = new Dictionary<string, bool>();

            if (allFunList == null)
                return JavaScript("");

            StringBuilder scriptBuilder = new StringBuilder();

            foreach (var fun in allFunList)
            {
                // 为true时禁用按钮。
                var disabled = !userFunList.Exists(n => n.GNID == fun.GNID);
                //controlsDisabledDictionary.Add(fun.KJID, disabled);

                if (disabled)
                {
                    scriptBuilder.AppendFormat("$('#{0}').hide();\r\n", fun.KJID);
                }
            }

            return JavaScript(scriptBuilder.ToString());
        }


        /// <summary>
        /// 保存数据实体操作
        /// </summary>
        /// <param name="saveAction">保存的操作方法</param>
        /// <returns>返回操作结果 Json</returns>
        protected virtual ActionResult Save(Func<ExtResult> saveAction)
        {
            //var ret = new ExtResult();
            ExtResult ret;
            try
            {
                if (ModelState.IsValid)
                {
                    ret = saveAction();
                    if (string.IsNullOrEmpty(ret.msg))
                        ret.msg = "保存成功";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    var errs = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var err in errs)
                    {
                        sb.AppendFormat("{0}</br>", err.ErrorMessage);
                    }
                    ret = new ExtResult();
                    ret.success = false;
                    ret.msg = sb.ToString();
                }
            }
            catch (Exception ex)
            {
                ret = new ExtResult();
                ret.success = false;
                if (ex is JsMiracleException)
                {
                    ret.msg = ex.Message;
                }
                else
                {
                    Exception innerExp = ex;

                    while (innerExp.InnerException != null)
                    {
                        innerExp = innerExp.InnerException;
                    }

                    ret.msg = string.Format("{0}-{1}", ex.Message, innerExp.Message);
                }

            }
            return this.JsonFormat(ret);
        }

        protected virtual ActionResult Remove(Func<ExtResult> removeAction)
        {
            ExtResult ret;
            try
            {
                ret = removeAction();
                if (string.IsNullOrEmpty(ret.msg))
                    ret.msg = "删除成功";
            }
            catch (Exception ex)
            {
                ret = new ExtResult();
                ret.success = false;
                if (ex is JsMiracleException)
                    ret.msg = ex.Message;
                else
                    ret.msg = string.Format("{0}-{1}", ex.Message, ex.InnerException);
            }
            return this.JsonFormat(ret);
        }
    }
}