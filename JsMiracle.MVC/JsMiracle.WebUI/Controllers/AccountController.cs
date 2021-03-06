﻿using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Framework.Cache;
using JsMiracle.WebUI.Controllers.Filter;
using JsMiracle.WebUI.Models;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JsMiracle.WCF.UP.IAuthMng;
using JsMiracle.Framework.FormAuth;
using JsMiracle.Entities.TabelEntities;

namespace JsMiracle.WebUI.Controllers.UP
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

        [AllowAnonymous]
        public ActionResult LogInPop()
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
                var md5Pwd = IMS_UP_YH.GetPwdMD5(model.Password);

                if (membership.ValidateUser(model.UserID, md5Pwd))
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

            //return RedirectToAction("LogIn", "Account");
            return RedirectToAction("Index", "Home");
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
