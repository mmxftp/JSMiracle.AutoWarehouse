using JsMiracle.Dal.Abstract;
using JsMiracle.Entities.View;
using JsMiracle.Framework.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JsMiracle.WebUI.CommonSupport
{
    public class ActionPermission : IActionPermission
    {
        /// <summary>
        /// 所有权限信息的ApplicationCache名称
        /// </summary>
        private const string CacheAllPermission = "allPermission";

        private readonly ICache cache;
        private readonly IPermission dalPermission;

        public ActionPermission(ICache repoCache,IPermission repoPermission)
        {
            cache = repoCache;
            dalPermission = repoPermission;
        }

        //public IList<ActionPermission> GetAllActionPermissions()
        //{
        //    if (cache.GetApplicationCache("actionpermission") == null)
        //    {
        //        cache.SetApplicationCache("actionpermission", base.GetAll());
        //    }
        //    return (IList<ActionPermission>)cache.GetApplicationCache("actionpermission");
        //}

        public PermissionViewModule GetAllPermission()
        {
            if (cache.GetApplicationCache(CacheAllPermission) == null)
            {
                var allPer = dalPermission.GetAllPermission();
                cache.SetApplicationCache(CacheAllPermission, dalPermission.GetAllPermission());
            }
            return (PermissionViewModule)cache.GetApplicationCache(CacheAllPermission);

        }


        public void ResetCache()
        {
            cache.RemoveApplicationCache(CacheAllPermission);
            GetAllPermission();
        }
    }
}