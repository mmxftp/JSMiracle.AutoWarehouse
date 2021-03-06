﻿
using JsMiracle.Entities;
using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.WebUI.Controllers.Filter;
using JsMiracle.WebUI.Models;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WebUI.Controllers;
using JsMiracle.WCF.UP.IAuthMng;

namespace JsMiracle.WebUI.Areas.UP.Controllers
{
    public class RoleUserController : BaseController
    {
        IRoleUser dalRoleUser;
        IRole dalRole;
        IPermission dalPermission;

        public RoleUserController(IRoleUser repoRoleUser
            , IRole repoRole, IPermission repoPer)
        {
            dalRoleUser = repoRoleUser;
            dalRole = repoRole;
            dalPermission = repoPer;
        }

        //
        // GET: /RoleUser/
        [AuthViewPage]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetRoleUserList(string roleid)
        {
            //var info = new PaginationModel<IMS_UP_YH>();
            //var info = new PaginationModel();
            //PaginationModel info;

            if (string.IsNullOrEmpty(roleid))
            {
                //info.total = 0;
                //info.rows = new List<IMS_UP_YH>();   // 解决easyui length的问题
                //info.SetRows( new List<IMS_UP_YH>());
                var nullInfo = new PaginationModel(new List<IMS_UP_YH>(),0);
                return this.JsonFormat(nullInfo);
            }

            var data = dalRoleUser.GetUserList(roleid);
            //info.total = data.Count;
            //info.SetRows(data);
            //info.rows = data;

            var info = new PaginationModel(data,0);

            return this.JsonFormat(info);
        }


        public ActionResult SaveRoleUser(bool isAdd, string roleid, string userid)
        {
            if (string.IsNullOrEmpty(roleid) || string.IsNullOrEmpty(userid))
                return Json(new { success = false, err = "roleid 或 userid不得为空" });

            if (isAdd)
            {
                dalRoleUser.SaveRoleUser(roleid, userid);
            }
            else
            {
                dalRoleUser.RemoveRoleUser(roleid, userid);
            }

            return this.JsonFormat(new { success = true });
        }

        #region IMS_UP_RoleSet 表操作

        public ActionResult GetRolesAll()
        {
            var data = dalRole.GetAllRole();
            return this.JsonFormat(data);
        }

        public ActionResult GetRoleList(int? rows, int? page)
        {
            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            //var dataList = dalRole.GetDataByPage(
            //  o => o.JSMC,
            //  null,
            //  pageIndex, pageSize, out totalCount);
            var dataList = dalRole.GetDataByPageDynamic(pageIndex, pageSize, out totalCount,
                "JSMC", "");

            //数据组装到viewModel
            //info.total = totalCount;
            //info.rows = dataList;
            var info = new PaginationModel(dataList, totalCount);

            //var json = Json(info);
            return this.JsonFormat(info);
        }

        public ViewResult CreateRole()
        {
            return View("EditRole", new IMS_UP_JS());
        }

        public ViewResult EditRole(long id)
        {
            var role = dalRole.GetEntity(id);
            return View(role);
        }

        public ActionResult SaveRole(IMS_UP_JS module)
        {
            Func<ExtResult> saveFun = () =>
            {
                if (string.IsNullOrEmpty(module.JSID))
                {
                    module.JSID = Guid.NewGuid().ToString();
                }

                dalRole.SaveOrUpdate(module);
                ExtResult ret = new ExtResult();
                ret.success = true;
                return ret;
            };

            return base.Save(saveFun);            
        }


        public ActionResult RemoveRole(long id)
        {
            try
            {
                dalRole.Delete(id);

                return this.JsonFormat(new ExtResult { success = true });
            }
            catch (Exception ex)
            {
                return this.JsonFormat(new ExtResult { success = false, msg = ex.Message });
            }
        }
        #endregion

        #region IMS_UP_RoMoSet 表操作

        public ActionResult PermissionInfo(string roleid)
        {
            var data = dalPermission.GetPermissionInfo(roleid);

            if (data == null)
            {
                // 防止前端页面显示问题
                data = new List<TreeModel>();
                TreeModel tm = new TreeModel();
                data.Add(tm);
                tm.id = -1;
            }

            return this.JsonFormat(data);
        }

        [HttpPost]
        public ActionResult SavePermission(bool check, int parentid, int moduleid, int functionid, string roleid)
        {
            try
            {
                var effectRowCount = dalPermission.SavePermission(check, parentid, moduleid, functionid, roleid);

                return Json(new ExtResult { success = true, effectRowCount = effectRowCount });
            }
            catch (Exception ex)
            {
                return this.JsonFormat(new ExtResult { success = false, msg = ex.Message });
            }
        }
        #endregion

    }
}
