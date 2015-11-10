using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace JsMiracle.WebUI.Controllers
{
    public class PermissionController : Controller
    {
        //
        // GET: /Permission/

        public ActionResult Index()
        {
            return View();
        }

        IPermission dalPermission;

        public PermissionController(IPermission repoPer)
        {
            dalPermission = repoPer;
        }

        public JsonResult PermissionInfo(string roleid)
        {
            var data = dalPermission.GetPermissionInfo(roleid);

            if (data == null)
            {
                // 防止前端页面显示问题
                data = new List<TreeModel>();
                TreeModel tm = new TreeModel();
                data.Add(tm);
                tm.id = -1;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SavePermission(bool check, int moduleid, int functionid, string roleid)
        {
            try
            {
                dalPermission.SavePermission(check, moduleid, functionid, roleid);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, err = ex.Message });
            }
        }

    }
}
