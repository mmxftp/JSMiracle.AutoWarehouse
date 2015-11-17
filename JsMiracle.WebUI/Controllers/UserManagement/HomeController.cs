﻿using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using JsMiracle.Entities.View;
using JsMiracle.Framework;
using JsMiracle.WebUI.CommonSupport;
using JsMiracle.WebUI.Controllers.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JsMiracle.WebUI.Controllers.UserManagement
{

    public class HomeController : BaseController
    {
        private IModule dalModule;
        private IActionPermission actionPerission;

        public HomeController(IModule repoModule,
            IActionPermission repoPermission)
        {
            dalModule = repoModule;
            actionPerission = repoPermission;
        }

        //
        // GET: /Home/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        private IEnumerable<IMS_TB_Module> GetChildModule(PermissionViewModule module ,int parentid)
        {
            return module.Modules.Where(n => n.ParentID == parentid);
        } 

        [AllowAnonymous]
        public PartialViewResult _MenuPartial()
        {
            var menuList = new List<IMS_TB_Module>();

            var rootModuleList = dalModule.GetRootModule();

            if (rootModuleList == null)
                return PartialView(null);

            var rootMenus = rootModuleList.OrderBy(n => n.SortID);

            PermissionViewModule permissionModule = null;

            if (CurrentUser.IsAdmin)
            {
                permissionModule = actionPerission.GetAllPermission();                
            }
            else 
            {
                 var user = CurrentUser.GetCurrentUser();

                permissionModule= user.Permissions;
            }
            

            foreach (var m in rootMenus)
            {
                var perList = permissionModule.Modules.Where(n => n.ParentID == m.ModuleID);

                //var userPermissionList = user.Permissions.Modules.Where(n => n.ParentID == m.ModuleID);

                // 有子项菜单才需要显示
                if (perList.Count() > 0)
                {
                    menuList.Add(m);
                    menuList.AddRange(perList);
                    //foreach (var p in perList)
                    //    menuList.Add(p);
                }

                //var x = dalModule.GetChildModuleList(m.ModuleID);
                //menuList.AddRange(x);
            }

            return PartialView(menuList);
        }

    }
}