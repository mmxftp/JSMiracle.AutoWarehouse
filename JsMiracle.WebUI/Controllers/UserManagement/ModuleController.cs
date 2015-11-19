﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JsMiracle.Entities;
using System.Linq.Expressions;
using JsMiracle.Dal.Abstract;
using JsMiracle.WebUI.Models;
using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.WebUI.Controllers.Filter;
using JsMiracle.Framework.Cache;
using JsMiracle.Framework;

namespace JsMiracle.WebUI.Controllers.UserManagement
{
    public class ModuleController : BaseController
    {
        //
        // GET: /Module/
        [AuthViewPage]
        public ActionResult Index()
        {
            return View();
        }

        private IModule dalModule;
        private IModuleFunction dalFunction;
        private IActionPermission dalAction;


        public ModuleController(IModule repoModule, IModuleFunction repoFun, IActionPermission repoAction)
        {
            dalModule = repoModule;
            dalFunction = repoFun;
            dalAction = repoAction;

        }

        public ActionResult ResetCache()
        {
            dalAction.ResetCache();
            return this.JsonFormat(new ExtResult { success = true });
        }

        #region IMS_UP_ModuT 表操作

        public ActionResult GetModuleList()
        {
            var data = dalModule.GetRootModule();
            return this.JsonFormat(data);
        }

        public ActionResult GetChildModuleList(int? rows, int? page, int? parentid)
        {
            //var data = moduleInfo.FindWhere(n => n.ParentID == parentid);
            var info = new PaginationModel<IMS_UP_Modu>();

            if (parentid == null)
            {
                info.total = 0;
                info.rows = new List<IMS_UP_Modu>();   // 解决easyui length的问题
                return Json(info);
            }


            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            var dataList = dalModule.GetDataByPage(
                o => o.ModuleName
                , f => f.ParentID == parentid.Value
                , pageIndex, pageSize, out totalCount);

            //数据组装到viewModel

            info.total = totalCount;
            info.rows = dataList;

            //var json = Json(info);
            return this.JsonFormat(info);
            //return Json(info, ContextType);
        }

        public ViewResult EditModule(int id)
        {
            var user = dalModule.GetEntity(id);
            return View(user);
        }

        public ActionResult SaveModule(IMS_UP_Modu module)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //if (!string.IsNullOrEmpty(module.Action_Name) )
                    module.URL = module.GetUrl();

                    dalModule.SaveOrUpdate(module);

                    var parentModule = dalModule.GetEntityByModuleID(module.ParentID);
                    long pid = -1;
                    if (parentModule != null)
                        pid = parentModule.ID;

                    return this.JsonFormat(new { success = true, msg = "操作成功", id = pid, parentid = module.ParentID });
                }
                catch (Exception ex)
                {
                    return this.JsonFormat(new ExtResult { success = true, msg = "操作失败" + ex.Message });
                }

            }
            else
            {
                return this.JsonFormat(module);
            }
        }

        public ViewResult CreateModule(int parentid = -1)
        {
            // parentid 与 moduleid 是主从关系, 数据表中的id只是主键没有业务关系
            if (parentid != -1)
            {
                var ent = dalModule.GetEntity(parentid);
                if (ent != null)
                    parentid = ent.ModuleID;
            }

            var newEnt = new IMS_UP_Modu() { ParentID = parentid };

            return View("EditModule", newEnt);
        }


        public ActionResult RemoveModule(long id)
        {
            try
            {
                dalModule.Delete(id);
                return this.JsonFormat(new { success = true });
            }
            catch (Exception ex)
            {
                return this.JsonFormat(new ExtResult { success = false, msg = ex.Message });
            }
        }

        #endregion

        #region IMS_UP_MoFnTT 表操作


        public ActionResult IndexFunction(int moduleid)
        {
            ViewData["moduleid"] = moduleid;
            return View();
        }

        public ActionResult GetModuleFunctionList(int moduleid)
        {
            var data = dalFunction.GetModuleFunctionList(moduleid);

            var info = new PaginationModel<IMS_UP_MoFn>();

            info.total = (data != null) ? data.Count : 0;
            info.rows = data;

            return Json(data);
        }

        public ViewResult CreateFunction(int moduleid)
        {
            var mod = dalModule.GetEntityByModuleID( moduleid);
            if (mod != null)
                ViewBag.ModName = mod.ModuleName;

            return View("EditFunction", new IMS_UP_MoFn() { ModuleID = moduleid });
        }

        public ViewResult EditFunction(int id)
        {
            var fun = dalFunction.GetEntity(id);

            var mod = dalModule.GetEntityByModuleID( fun.ModuleID);
            if (mod != null)
                ViewBag.ModName = mod.ModuleName;

            return View(fun);
        }

        public ActionResult SaveFunction(IMS_UP_MoFn module)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dalFunction.SaveOrUpdate(module);
                }
                catch (Exception ex)
                {
                    return this.JsonFormat(new { success = true, msg = "操作失败" + ex.Message });
                }

                return this.JsonFormat(new { success = true, msg = "操作成功" });
            }
            else
            {
                return this.JsonFormat(module);
            }
        }


        public ActionResult RemoveFunction(int id)
        {
            try
            {
                dalFunction.Delete(id);

                return Json(new ExtResult { success = true });
            }
            catch (Exception ex)
            {
                return Json(new ExtResult { success = false, msg = ex.Message });
            }
        }

        #endregion
    }
}
