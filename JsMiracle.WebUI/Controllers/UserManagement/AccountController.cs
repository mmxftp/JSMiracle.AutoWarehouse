using JsMiracle.Dal.Abstract;
using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Framework.Cache;
using JsMiracle.WebUI.Controllers.Filter;
using JsMiracle.WebUI.Models;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JsMiracle.WebUI.Controllers.UserManagement
{

    public class AccountController : BaseController
    {
        IFormsAuthentication formAuth;
        IMembershipService membership;
        ICache cache;

        public AccountController(IFormsAuthentication iFormAuth, IMembershipService iMembership, ICache iCache)
        {
            this.formAuth = iFormAuth;
            this.membership = iMembership;
            this.cache =iCache;
        }

        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LoginModel model, string returnUrl)
        {
            try
            {
                if (membership.ValidateUser(model.UserID, model.Password))
                {
                    formAuth.SignIn(model.UserID, false);

                    string url = RedirectToLocal(returnUrl);
                    return this.JsonFormat(new ExtResult { success = true, msg = url });
                }

                return this.JsonFormat(new ExtResult { success = false, msg = "提供的用户名或密码不正确" });
            }
            catch (Exception ex)
            {
                return this.JsonFormat(new ExtResult { success = false, msg = ex.Message }); 
            }
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            formAuth.SignOut();
            cache.RemoveSessionCache(User.Identity.Name);

            return RedirectToAction("LogIn", "Account");
        }

        private string RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return returnUrl;
            }
            else
            {
                // 登录成功的默认页
                return Url.Action("Index", "Home");
            }
        }

    }
}
