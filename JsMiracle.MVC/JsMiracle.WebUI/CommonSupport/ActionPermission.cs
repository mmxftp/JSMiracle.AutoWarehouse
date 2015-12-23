using JsMiracle.Entities.View;
using JsMiracle.Framework.Cache;
using JsMiracle.InversionOfControl;
using JsMiracle.WCF.UP.IAuthMng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JsMiracle.WebUI.CommonSupport
{
    public static class ActionPermission //: IActionPermission
    {
        /// <summary>
        /// 所有权限信息的ApplicationCache名称
        /// </summary>
        private const string CacheAllPermission = "allPermission";

        //private readonly ICache cache;
        //private readonly IPermission dalPermission;

        //public ActionPermission(ICache repoCache, IPermission repoPermission)
        //{
        //    cache = repoCache;
        //    dalPermission = repoPermission;
        //}

        //public IList<ActionPermission> GetAllActionPermissions()
        //{
        //    if (cache.GetApplicationCache("actionpermission") == null)
        //    {
        //        cache.SetApplicationCache("actionpermission", base.GetAll());
        //    }
        //    return (IList<ActionPermission>)cache.GetApplicationCache("actionpermission");
        //}

        public static PermissionViewModule GetAllPermission()
        {
            var cache = WindsorContaineFactory.GetContainer().Resolve<ICache>();

            if (cache.GetApplicationCache(CacheAllPermission) == null)
            {
                var dalPermission = WindsorContaineFactory.GetContainer().Resolve<IPermission>();

                var allPer = dalPermission.GetAllPermission();
                cache.SetApplicationCache(CacheAllPermission, dalPermission.GetAllPermission());
            }
            return (PermissionViewModule)cache.GetApplicationCache(CacheAllPermission);

        }


        public static void ResetCache()
        {
            var cache = WindsorContaineFactory.GetContainer().Resolve<ICache>();
            cache.RemoveApplicationCache(CacheAllPermission);
            GetAllPermission();
        }
    }
}