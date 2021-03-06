﻿using JsMiracle.WebUI.Controllers;
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
using JsMiracle.WCF.UP.IAuthMng;
using JsMiracle.WebUI.Areas.UP.Models;
using JsMiracle.WebUI.CommonSupport;

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

        public ActionResult GetUserList(int? rows, int? page, string txt,string where)
        {
            int totalCount = 0;
            System.Web.Http.WebHost.HttpControllerHandler d;
            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            //if (!int.TryParse(Request.Params["rows"], out pageSize))
            //    pageSize = 10;

            //if (!int.TryParse(Request.Params["page"], out pageIndex))
            //    pageIndex = 1;

            string filter = null;

            if (!string.IsNullOrEmpty(where))
            {
                filter = where;
            }
            else if (!string.IsNullOrEmpty(txt))
            {
                filter = string.Format(" (YHID = {0} || YHM.Contains({0})) && ZT = {1} "
                    , txt, (int)UserStateEnum.Normal);
            }
            else
            {
                filter = string.Format(" ZT = {0} " , (int)UserStateEnum.Normal);
            }

            //var dataList = dalUser.GetDataByPage(
            //    o => o.YHID,
            //    filter,
            //    pageIndex, pageSize, out totalCount);
            var dataList = dalUser.GetDataByPageDynamic(
                pageIndex, pageSize, out totalCount
                , "YHID", filter);


            //数据组装到viewModel
            var info = new PaginationModel(dataList, totalCount);
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
                user.MM = IMS_UP_YH.GetPwdMD5(user.MM);

                dalUser.SaveOrUpdate(user);

                ExtResult ret = new ExtResult();
                ret.success = true;
                return ret;
            };

            return base.Save(saveFun);
        }


        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            ChangePasswordModule module = new ChangePasswordModule(); 
            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.Login = 0;
                return View(module);
            }

            var user =  CurrentUser.GetCurrentUser().UserInfo;
            module.ID = user.ID;
            module.UserID = user.YHID;
            module.UserName = user.YHM;
            module.OldPasswordMD5 = user.MM;
            ViewBag.Login = 1;
            return View(module);
        }


        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ChangePassword(ChangePasswordModule module)
        {
            var user = dalUser.GetEntity(module.ID);
            user.MM = IMS_UP_YH.GetPwdMD5(module.NewPassword);

            Func<ExtResult> saveFun = () =>
            {              
                dalUser.SaveOrUpdate(user);

                ExtResult ret = new ExtResult();
                ret.success = true;
                return ret;
            };

            return base.Save(saveFun);
        }
    }
}
