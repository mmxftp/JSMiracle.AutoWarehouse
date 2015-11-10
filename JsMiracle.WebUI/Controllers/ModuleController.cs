using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JsMiracle.Entities;
using System.Linq.Expressions;
using JsMiracle.Dal.Abstract;
using JsMiracle.WebUI.Models;

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

        private IModule dal;

        public ModuleController(IModule repo)
        {
            this.dal = repo;
        }

        public JsonResult GetModuleList()
        {
            var data = dal.GetRootModule();
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

            var dataList = dal.GetChildModuleList(pageIndex, pageSize, parentid.Value, out totalCount);

            //数据组装到viewModel

            info.total = totalCount;
            info.rows = dataList;

            //var json = Json(info);
            return Json(info);
        }

        public ViewResult Edit(int id)
        {
            var user = dal.GetEntity(id);
            return View(user);
        }

        public JsonResult Save(IMS_TB_Module module)
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

        public ViewResult Create(int parentid = -1)
        {
            // parentid 与 moduleid 是主从关系, 数据表中的id只是主键没有业务关系
            if (parentid != -1)
            {
                var ent = dal.GetEntity(parentid);
                if (ent != null)
                    parentid = ent.ModuleID;
            }

            var newEnt = new IMS_TB_Module() { ParentID = parentid };

            return View("Edit", newEnt);
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
