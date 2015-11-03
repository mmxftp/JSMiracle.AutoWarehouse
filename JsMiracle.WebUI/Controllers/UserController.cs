using JsMiracle.Dal.Abstract;
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
    public class UserController : Controller
    {
         private IDataLayer<IMS_TB_UserInfo> userInfo;

        public UserController(IDataLayer<IMS_TB_UserInfo> repo){
            this.userInfo = repo;
        }

        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public JsonResult GetUserList(int? rows, int? page, string txt)
        {
            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            //if (!int.TryParse(Request.Params["rows"], out pageSize))
            //    pageSize = 10;

            //if (!int.TryParse(Request.Params["page"], out pageIndex))
            //    pageIndex = 1;

            Expression<Func<IMS_TB_UserInfo, bool>> filter = null;
            //var txt = Request.Params["txt"];
            if (!string.IsNullOrEmpty(txt)) {
                filter = 
                    f => f.UserID == txt || f.UserName.Contains(txt);
            }


            var dataList = userInfo.GetDataByPage(
                p => p.UserID,
                filter, pageIndex, pageSize, out totalCount);

            //数据组装到viewModel
            var info = new PaginationModel<IMS_TB_UserInfo>();
            info.total = totalCount;
            info.rows = dataList;

            //var json = Json(info);
            return Json(info);
        }


        public ViewResult Edit(int id)
        {
            var user = userInfo.Find(id);
            return View(user);
        }

        public JsonResult Save(IMS_TB_UserInfo user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userInfo.Update(user);
                }
                //catch (System.Data.Entity.Validation.DbEntityValidationException ve)
                //{
                //    return Json(new { success = false, message = "操作失败" + ve.Messgage });
                //}
                catch (Exception ex)
                {
                    return Json(new { success = true, message = "操作失败" + ex.Message });
                }

                return Json(new { success = true, message = "操作成功" });
            }
            else
            {
                return Json(user);
            }

        }

        public ViewResult Create()
        {
            return View("Edit", new IMS_TB_UserInfo());
        }


        public JsonResult Remove(int id)
        {
            var ent = userInfo.Find(id);

            try
            {
                if (ent != null)
                    userInfo.Delete(ent);

                return Json(new { success = true });
            }
            catch(Exception ex )
            {
                return Json(new { success = false, errorMsg = ex.Message });
            }
        }
    }
}
