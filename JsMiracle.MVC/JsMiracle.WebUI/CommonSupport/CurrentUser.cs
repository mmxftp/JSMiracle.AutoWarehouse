using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.View;
using JsMiracle.Framework;
using JsMiracle.Framework.Cache;
using JsMiracle.InversionOfControl;
using JsMiracle.WCF.UP.IAuthMng;
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
        public IMS_UP_YH UserInfo { get; private set; }

        /// <summary>
        /// 用户权限信息
        /// </summary>
        public PermissionViewModule Permissions { get; set; }

        public static CurrentUser GetCurrentUser()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated && !IsAdmin)
                throw new JsMiracleException("用户未登录");//必须要用户Form验证后才能使用CurrentUser

            string userid = HttpContext.Current.User.Identity.Name;
            var cache = WindsorContaineFactory.GetContainer().Resolve<ICache>();

            if (cache.GetSessionCache(userid) == null)
            {
                // 管理员的操作
                if (IsAdmin)
                {
                    userid = "admin";

                    CurrentUser cu = new CurrentUser();
                    cu.UserInfo = new IMS_UP_YH() { YHID = "admin", YHM = "admin" };

                    cu.Permissions = ActionPermission.GetAllPermission();
                    cache.SetSessionCache(userid, cu);
                }
                else
                {
                    CurrentUser cu = new CurrentUser();
                    var user = WindsorContaineFactory.GetContainer().Resolve<IUser>();
                    cu.UserInfo = user.GetEntityByYHBH(HttpContext.Current.User.Identity.Name);
                    var per = WindsorContaineFactory.GetContainer().Resolve<IPermission>();
                    cu.Permissions = per.GetPermissionListByUserID(userid);
                    cache.SetSessionCache(userid, cu);
                }
            }

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