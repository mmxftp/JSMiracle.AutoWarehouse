﻿using JsMiracle.Dal.Abstract;
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

        private IDataLayer<IMS_TB_ModuleFunction> dal;

        public ModuleFunctionController(IDataLayer<IMS_TB_ModuleFunction> repo)
        {
            dal = repo;
        }

        public JsonResult GetModuleFunctionList(int moduleid)
        {
            var data = dal.FindWhere(n => n.ModuleID == moduleid);

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
            var user = dal.Find(id);
            return View(user);
        }

        public JsonResult Save(IMS_TB_ModuleFunction module)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // 新增时自动计算模块号
                    if ( module.FunctionID == 0)
                    {
                        var itemCount = dal.FindWhere(n => n.ModuleID == module.ModuleID).Count;
                        // 得到同类的子项的记数加1
                        module.FunctionID = module.ModuleID * 100 + itemCount + 1;
                    }

                    dal.Update(module);
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
            var ent = dal.Find(id);

            try
            {
                if (ent != null)
                {
                    dal.Delete(ent);
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMsg = ex.Message });
            }
        }

    }
}
