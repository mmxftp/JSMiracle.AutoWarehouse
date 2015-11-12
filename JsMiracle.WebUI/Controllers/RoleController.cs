using JsMiracle.Dal.Abstract;
using JsMiracle.Dal.Concrete;
using JsMiracle.Entities;
using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace JsMiracle.WebUI.Controllers
{
    public class RoleController : Controller
    {
        //
        // GET: /Role/

        public ActionResult Index()
        {
            return View();
        }

        private IRole dal;

        public RoleController(IRole repo)
        {
            dal = repo;
        }

        public JsonResult GetRolesAll()
        {
            var data = dal.GetAllRole();
            return Json(data);
        }
     
        public JsonResult GetRoleList(int? rows, int? page)
        {
            //var data = moduleInfo.FindWhere(n => n.ParentID == parentid);
            var info = new PaginationModel<IMS_TB_RoleInfo>();

            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            var dataList = dal.GetRoleList(pageIndex, pageSize);

            //数据组装到viewModel
            info.total = totalCount;
            info.rows = dataList;

            //var json = Json(info);
            return Json(info);
        }

        public ViewResult Create()
        {
            return View("Edit", new IMS_TB_RoleInfo() );
        }

        public ViewResult Edit(int id)
        {
            var role = dal.GetEntity(id);
            return View(role);
        }

        public JsonResult Save(IMS_TB_RoleInfo module)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dal.Save(module);
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


        public JsonResult Remove(int id)
        {
            try
            {
                dal.Remove(id);

                return Json(new ExtResult { success = true });
            }
            catch (Exception ex)
            {
                return Json(new ExtResult { success = false, msg = ex.Message });
            }
        }
    }
}
