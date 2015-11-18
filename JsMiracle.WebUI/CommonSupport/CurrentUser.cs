﻿using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using JsMiracle.Entities.View;
using JsMiracle.Framework;
using JsMiracle.Framework.Cache;
using JsMiracle.WebUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JsMiracle.WebUI.CommonSupport
{
    public class CurrentUser
    {
        public static bool IsAdmin = false;
        static CurrentUser()
        {
            IsAdmin = true;
        }
        
        /// <summary>
        /// 用户信息
        /// </summary>
        public IMS_TB_UserInfo UserInfo { get; private set; }

        /// <summary>
        /// 用户权限信息
        /// </summary>
        public PermissionViewModule Permissions { get; set; }

        public static CurrentUser GetCurrentUser()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                throw new JsMiracleException("用户未登录");//必须要用户Form验证后才能使用CurrentUser

            string userid = HttpContext.Current.User.Identity.Name;
            var cache = WindsorContaineFactory.GetContainer().Resolve<ICache>();

            if (cache.GetSessionCache(userid) == null)
            {
                CurrentUser cu = new CurrentUser();
                var user = WindsorContaineFactory.GetContainer().Resolve<IUser>();
                cu.UserInfo = user.GetEntity(HttpContext.Current.User.Identity.Name);
                var per = WindsorContaineFactory.GetContainer().Resolve<IPermission>();
                cu.Permissions = per.GetPermissionListByUserID(userid);
                cache.SetSessionCache(userid, cu);
            }
            //var u = (CurrentUser)cache.GetCache(userid);
            //this.UserInfo = u.UserInfo;
            return (CurrentUser)cache.GetSessionCache(userid);
        }

        private CurrentUser() { }

        //public CurrentUser()
        //{
        //    if (!HttpContext.Current.User.Identity.IsAuthenticated)
        //        throw new JsMiracleException("用户未登录");//必须要用户Form验证后才能使用CurrentUser

        //    string userid = HttpContext.Current.User.Identity.Name;
        //    var cache = WindsorContaineFactory.GetContainer().Resolve<ICache>();

        //    if (cache.GetCache(userid) == null)
        //    {
        //        var user = WindsorContaineFactory.GetContainer().Resolve<IUser>();
        //        UserInfo = user.GetEntity(null, userid: HttpContext.Current.User.Identity.Name);
        //        cache.SetCache(userid, this);
        //    }
        //    else
        //    {
        //        var u = (CurrentUser)cache.GetCache(userid);
        //        this.UserInfo = u.UserInfo;
        //    }



        //    //var cache = ContainerFactory.GetContainer().Resolve<ICache>();
        //    //if (cache.GetSessionCache("currentuser") == null)
        //    //{
        //    //    var authorityFacade = ContainerFactory.GetContainer().Resolve<IAuthorityFacade>();
        //    //    UserInfo = authorityFacade.GetHospUserByName(HttpContext.Current.User.Identity.Name);
        //    //    MenuPermission = authorityFacade.GetMenusByUserId(UserInfo.Id);
        //    //    ActionPermission = authorityFacade.GetActionsByUserId(UserInfo.Id);
        //    //    cache.SetSessionCache("currentuser", this);
        //    //}
        //    //else
        //    //{
        //    //    var u = (CurrentUser)cache.GetSessionCache("currentuser");
        //    //    UserInfo = u.UserInfo;
        //    //    MenuPermission = u.MenuPermission;
        //    //    ActionPermission = u.ActionPermission;
        //    //}
        //}
    }
}