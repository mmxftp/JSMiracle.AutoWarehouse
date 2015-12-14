using JsMiracle.Dal.Abstract.UP;
using JsMiracle.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JsMiracle.Framework;
using JsMiracle.WebUI.Controllers.Filter;
using System.Linq.Expressions;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.Enum;
using JsMiracle.Entities.EasyUI_Model;

namespace JsMiracle.WebUI.Areas.UP.Controllers
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

        //[AuthViewPage]
        //public ActionResult List()
        //{
        //    return View();
        //}

        [AuthViewPage]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUserList(int? rows, int? page, string txt)
        {
            throw new Exception("test");

            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            //if (!int.TryParse(Request.Params["rows"], out pageSize))
            //    pageSize = 10;

            //if (!int.TryParse(Request.Params["page"], out pageIndex))
            //    pageIndex = 1;

            Expression<Func<IMS_UP_YH, bool>> filter = null;

            if (!string.IsNullOrEmpty(txt))
            {
                filter =
                    f => (f.YHID == txt || f.YHM.Contains(txt)) && f.ZT == (int)UserStateEnum.Normal;
            }
            else
            {
                filter =
                    f => f.ZT == (int)UserStateEnum.Normal;
            }

            var dataList = dalUser.GetDataByPage(
                o => o.YHID,
                filter,
                pageIndex, pageSize, out totalCount);

            //数据组装到viewModel
            var info = new PaginationModel(dataList);
            //info.total = totalCount;
            //info.rows = dataList;

            //var json = Json(info);
            //return json;

            return this.JsonFormat(info);
        }


        public ViewResult Edit(long id)
        {
            var user = dalUser.GetEntity(id);
            return View(user);
        }

        public ActionResult Save(IMS_UP_YH user)
        {
            Func<ExtResult> saveFun = () =>
            {
                // 新增用户,密码需要md5一下
                if (user.ID == 0)
                    user.MM = IMS_UP_YH.GetPwdMD5(user.MM);

                dalUser.SaveOrUpdate(user);

                ExtResult ret = new ExtResult();
                ret.success = true;
                return ret;
            };

            return base.Save(saveFun);
        }

        public ViewResult Create()
        {
            return View("Edit", new IMS_UP_YH());
        }


        public ActionResult Remove(long id)
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

        [AllowAnonymous]
        public ActionResult Registion()
        {
            return View(new IMS_UP_YH());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registion(IMS_UP_YH user)
        {
            Func<ExtResult> saveFun = () =>
            {
                // 新增用户,密码需要md5一下
                if (user.ID == 0)
                    user.MM = IMS_UP_YH.GetPwdMD5(user.MM);

                dalUser.SaveOrUpdate(user);

                ExtResult ret = new ExtResult();
                ret.success = true;
                return ret;
            };

            return base.Save(saveFun);
        }

        public ActionResult ChangePassword()
        {

            if (!User.Identity.IsAuthenticated)
            {

            }

            //var user =  CurrentUser.GetCurrentUser();

            return View(new IMS_UP_YH());
        }



    }
}
