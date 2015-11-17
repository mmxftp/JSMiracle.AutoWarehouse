using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.WebUI.Controllers.Filter;
using JsMiracle.WebUI.Models;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JsMiracle.WebUI.Controllers.UserManagement
{
    public class UserController : BaseController
    {
        private IUser dalUser;

        public UserController(IUser repo)
        {
            this.dalUser = repo;
        }

        //
        // GET: /User/

        public ActionResult GetAllUserList(bool userNameFormatter = false)
        {
            var usrList = dalUser.GetAllUserList(userNameFormatter);
            return this.JsonFormat(usrList);
            
        }

       [AuthViewPage]
        public ActionResult List()
        {
            return View();
        }

       public ActionResult GetUserList(int? rows, int? page, string txt)
        {
            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            //if (!int.TryParse(Request.Params["rows"], out pageSize))
            //    pageSize = 10;

            //if (!int.TryParse(Request.Params["page"], out pageIndex))
            //    pageIndex = 1;

             var dataList = dalUser.GetUserList(pageIndex, pageSize, txt, out totalCount);

            //数据组装到viewModel
            var info = new PaginationModel<IMS_TB_UserInfo>();
            info.total = totalCount;
            info.rows = dataList;

            var json = Json(info);
            //return json;

            return this.JsonFormat(info);
        }


        public ViewResult Edit(int id)
        {
            var user = dalUser.GetEntity(id);
            return View(user);
        }

        public ActionResult Save(IMS_TB_UserInfo user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dalUser.Save(user);
                }
                catch (Exception ex)
                {
                    return this.JsonFormat(new ExtResult { success = true, msg = "操作失败" + ex.Message });
                }

                return this.JsonFormat(new ExtResult { success = true, msg = "操作成功" });
            }
            else
            {
                return this.JsonFormat(user);
            }

        }

        public ViewResult Create()
        {
            return View("Edit", new IMS_TB_UserInfo());
        }


        public ActionResult Remove(int id)
        {
            try
            {
                dalUser.Remove(id);
                return this.JsonFormat(new ExtResult { success = true });
            }
            catch (Exception ex)
            {
                return this.JsonFormat(new ExtResult { success = false, msg = ex.Message });
            }
        }



    }
}
