﻿using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using JsMiracle.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

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
            var info = new UserModel();
            info.total = totalCount;
            info.rows = dataList;

            //var json = Json(info);
            return Json(info);
        }

    }
}
