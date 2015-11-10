using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using JsMiracle.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JsMiracle.WebUI.Controllers
{
    public class ModuleFunctionController : Controller
    {
        //
        // GET: /ModuleFunction/

        public ActionResult Index(int moduleid)
        {
            ViewData["moduleid"] = moduleid;
            return View();
        }

        private IModuleFunction dal;

        public ModuleFunctionController(IModuleFunction repo)
        {
            dal = repo;
        }

        public JsonResult GetModuleFunctionList(int moduleid)
        {
            var data = dal.GetModuleFunctionList(moduleid);

            var info = new PaginationModel<IMS_TB_ModuleFunction>();

            info.total = (data != null) ? data.Count : 0;
            info.rows = data;

            return Json(data);
        }

        public ViewResult Create(int moduleid)
        {
            return View("Edit", new IMS_TB_ModuleFunction() { ModuleID = moduleid });
        }

        public ViewResult Edit(int id)
        {
            var fun = dal.GetEntity(id);
            return View(fun);
        }

        public JsonResult Save(IMS_TB_ModuleFunction module)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dal.Save(module);
                }
                catch (Exception ex)
                {
                    return Json(new { success = true, message = "操作失败" + ex.Message });
                }

                return Json(new { success = true, message = "操作成功" });
            }
            else
            {
                return Json(module);
            }
        }


        public JsonResult Remove(int id)
        {
            try
            {
                dal.Remove(id);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMsg = ex.Message });
            }
        }

    }
}
