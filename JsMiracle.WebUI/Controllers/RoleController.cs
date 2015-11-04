using JsMiracle.Dal.Abstract;
using JsMiracle.Dal.Concrete;
using JsMiracle.Entities;
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

        private IDataLayer<IMS_TB_RoleInfo> dal;

        public RoleController(IDataLayer<IMS_TB_RoleInfo> repo)
        {
            dal = repo;
        }

        public JsonResult GetRoleList(int? rows, int? page)
        {
            //var data = moduleInfo.FindWhere(n => n.ParentID == parentid);
            var info = new PaginationModel<IMS_TB_RoleInfo>();

            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            Expression<Func<IMS_TB_RoleInfo, bool>> filter = null;
            
            var dataList = dal.GetDataByPage(
                p => p.ID,
                filter, pageIndex, pageSize, out totalCount);

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
            var role = dal.Find(id);
            return View(role);
        }

        public JsonResult Save(IMS_TB_RoleInfo module)
        {
            if (ModelState.IsValid)
            {
                try
                {    
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
