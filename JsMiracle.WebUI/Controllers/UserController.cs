using JsMiracle.Dal.Abstract;
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
    public class UserController : Controller
    {
        private IUser dal;

        public UserController(IUser repo)
        {
            this.dal = repo;
        }

        //
        // GET: /User/

        public JsonResult GetAllUserList(bool userNameFormatter = false)
        {
            var usrList = dal.GetAllUserList(userNameFormatter);
            return Json(usrList);
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

             var dataList = dal.GetUserList(pageIndex, pageSize, txt, out totalCount);

            //数据组装到viewModel
            var info = new PaginationModel<IMS_TB_UserInfo>();
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

        public JsonResult Save(IMS_TB_UserInfo user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dal.Save(user);
                }
                catch (Exception ex)
                {
                    return Json(new ExtResult { success = true, msg = "操作失败" + ex.Message });
                }

                return Json(new ExtResult { success = true, msg = "操作成功" });
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
