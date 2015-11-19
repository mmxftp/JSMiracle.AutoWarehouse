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
using JsMiracle.Entities.Enum;

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

            Expression<Func<IMS_UP_User, bool>> filter = null;

            if (!string.IsNullOrEmpty(txt))
            {
                filter =
                    f => (f.UserID == txt || f.UserName.Contains(txt)) && f.State == (int)UserStateEnum.Normal;
            }
            else
            {
                filter =
                    f => f.State == (int)UserStateEnum.Normal;
            }

             var dataList = dalUser.GetDataByPage(
                 o=>o.UserID,
                 filter,
                 pageIndex, pageSize, out totalCount);

            //数据组装到viewModel
            var info = new PaginationModel<IMS_UP_User>();
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

        public ActionResult Save(IMS_UP_User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // 新增用户,密码需要md5一下
                    if (user.ID == 0)
                        user.Password = IMS_UP_User.GetPwdMD5(user.Password);

                    dalUser.SaveOrUpdate(user);
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
            return View("Edit", new IMS_UP_User());
        }


        public ActionResult Remove(int id)
        {
            try
            {
                dalUser.Delete(id);
                return this.JsonFormat(new ExtResult { success = true });
            }
            catch (Exception ex)
            {
                return this.JsonFormat(new ExtResult { success = false, msg = ex.Message });
            }
        }



    }
}
