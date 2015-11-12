using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JsMiracle.Entities;
using System.Linq.Expressions;
using JsMiracle.Dal.Abstract;
using JsMiracle.WebUI.Models;
using JsMiracle.Entities.EasyUI_Model;

namespace JsMiracle.WebUI.Controllers
{
    public class ModuleController : Controller
    {
        //
        // GET: /Module/

        public ActionResult Index()
        {
            return View();
        }

        private IModule dalModule;
        private IModuleFunction dalFunction;

        public ModuleController(IModule repoModule,IModuleFunction repoFun)
        {
            dalModule = repoModule;
            dalFunction = repoFun;
        }


        #region IMS_TB_ModuleSet 表操作

        public JsonResult GetModuleList()
        {
            var data = dalModule.GetRootModule();
            return Json(data);
        }

        public JsonResult GetChildModuleList(int? rows, int? page, int? parentid)
        {
            //var data = moduleInfo.FindWhere(n => n.ParentID == parentid);
            var info = new PaginationModel<IMS_TB_Module>();

            if (parentid == null)
            {
                info.total = 0;
                info.rows = new List<IMS_TB_Module>();   // 解决easyui length的问题
                return Json(info);
            }


            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            var dataList = dalModule.GetChildModuleList(pageIndex, pageSize, parentid.Value, out totalCount);

            //数据组装到viewModel

            info.total = totalCount;
            info.rows = dataList;

            //var json = Json(info);
            return Json(info);
        }

        public ViewResult EditModule(int id)
        {
            var user = dalModule.GetEntity(id);
            return View(user);
        }

        public JsonResult SaveModule(IMS_TB_Module module)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dalModule.Save(module);
                }
                catch (Exception ex)
                {
                    return Json(new ExtResult { success = true, msg = "操作失败" + ex.Message });
                }

                return Json(new ExtResult { success = true, msg = "操作成功" });
            }
            else
            {
                return Json(module);
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

            var newEnt = new IMS_TB_Module() { ParentID = parentid };

            return View("EditModule", newEnt);
        }


        public JsonResult RemoveModule(int id)
        {
            try
            {
                dalModule.Remove(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new ExtResult { success = false, msg = ex.Message });
            }
        }

        #endregion

        #region IMS_TB_ModuleFunctionSet 表操作


        public ActionResult IndexFunction(int moduleid)
        {
            ViewData["moduleid"] = moduleid;
            return View();
        }

        public JsonResult GetModuleFunctionList(int moduleid)
        {
            var data = dalFunction.GetModuleFunctionList(moduleid);

            var info = new PaginationModel<IMS_TB_ModuleFunction>();

            info.total = (data != null) ? data.Count : 0;
            info.rows = data;

            return Json(data);
        }

        public ViewResult CreateFunction(int moduleid)
        {
            return View("EditFunction", new IMS_TB_ModuleFunction() { ModuleID = moduleid });
        }

        public ViewResult EditFunction(int id)
        {
            var fun = dalFunction.GetEntity(id);
            return View(fun);
        }

        public JsonResult SaveFunction(IMS_TB_ModuleFunction module)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dalFunction.Save(module);
                }
                catch (Exception ex)
                {
                    return Json(new ExtResult { success = true, msg = "操作失败" + ex.Message });
                }

                return Json(new ExtResult { success = true, msg = "操作成功" });
            }
            else
            {
                return Json(module);
            }
        }


        public JsonResult RemoveFunction(int id)
        {
            try
            {
                dalFunction.Remove(id);

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
