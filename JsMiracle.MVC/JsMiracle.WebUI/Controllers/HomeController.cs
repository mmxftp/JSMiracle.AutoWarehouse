using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.View;
using JsMiracle.Framework;
using JsMiracle.Framework.Cache;
using JsMiracle.WCF.UP.IAuthMng;
using JsMiracle.WebUI.CommonSupport;
using JsMiracle.WebUI.Controllers.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JsMiracle.WebUI.Controllers.UP
{

    public class HomeController : BaseController
    {
        private IModule dalModule;


        public HomeController(IModule repoModule)
        {
            dalModule = repoModule;
        }

        //
        // GET: /Home/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public PartialViewResult _MenuPartial()
        {
            var menuList = new List<IMS_UP_MK>();

            var rootModuleList = dalModule.GetRootModule();

            if (rootModuleList == null)
                return PartialView(null);

            var rootMenus = rootModuleList.OrderBy(n => n.PXID);

            PermissionViewModule permissionModule = null;

            if (CurrentUser.IsAdmin)
            {
                //dalPermission.GetAllEntites

                permissionModule = ActionPermission.GetAllPermission();
            }
            else
            {
                if (!User.Identity.IsAuthenticated)
                {
                    permissionModule = null;
                }
                else
                {
                    var user = CurrentUser.GetCurrentUser();
                    permissionModule = user.Permissions;
                }
            }


            if (permissionModule == null)
                return PartialView(menuList);

            foreach (var m in rootMenus)
            {
                var perList = permissionModule.Modules.Where(n => n.FMKID == m.MKID).OrderBy(n => n.PXID);

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

            foreach (var m in menuList)
            {
                // 根节点不需要url地址
                if (m.FMKID == -1)
                    continue;

                if (string.IsNullOrEmpty(m.QY))
                    m.URL = Url.Action(m.HDMC, m.KZMC);
                else
                    m.URL = Url.Action(m.HDMC, m.KZMC, new { area = m.QY });
            }

            return PartialView(menuList);
        }

    }
}
