﻿using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JsMiracle.WebUI.Controllers
{
    public class RoleUserController : Controller
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
    
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetRoleUserList(string roleid)
        {
            var info = new PaginationModel<IMS_TB_UserInfo>();

            if (string.IsNullOrEmpty(roleid))
            {
                info.total = 0;
                info.rows = new List<IMS_TB_UserInfo>();   // 解决easyui length的问题
                return Json(info);
            }

            var data = dalRoleUser.GetUserList(roleid);

           
            info.total = data.Count;
            info.rows = data;

            return Json(info, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveRoleUser(bool isAdd, string roleid , string userid)
        {
            if (string.IsNullOrEmpty(roleid) || string.IsNullOrEmpty(userid))
                return Json(new { success= false, err="roleid 或 userid不得为空" });

            if ( isAdd)
            {
                dalRoleUser.SaveRoleUser(roleid, userid); 
            }
            else
            {
                dalRoleUser.RemoveRoleUser(roleid, userid);            
            }

            return Json(new { success = true });
        }

        #region IMS_TB_RoleInfoSet 表操作

        public JsonResult GetRolesAll()
        {
            var data = dalRole.GetAllRole();
            return Json(data);
        }

        public JsonResult GetRoleList(int? rows, int? page)
        {
            //var data = moduleInfo.FindWhere(n => n.ParentID == parentid);
            var info = new PaginationModel<IMS_TB_RoleInfo>();

            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            var dataList = dalRole.GetRoleList(pageIndex, pageSize);

            //数据组装到viewModel
            info.total = totalCount;
            info.rows = dataList;

            //var json = Json(info);
            return Json(info);
        }

        public ViewResult CreateRole()
        {
            return View("EditRole", new IMS_TB_RoleInfo());
        }

        public ViewResult EditRole(int id)
        {
            var role = dalRole.GetEntity(id);
            return View(role);
        }

        public JsonResult SaveRole(IMS_TB_RoleInfo module)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dalRole.Save(module);
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


        public JsonResult RemoveRole(int id)
        {
            try
            {
                dalRole.Remove(id);

                return Json(new ExtResult { success = true });
            }
            catch (Exception ex)
            {
                return Json(new ExtResult { success = false, msg = ex.Message });
            }
        }
        #endregion

        #region IMS_TB_PermissionSet 表操作

        public JsonResult PermissionInfo(string roleid)
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

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SavePermission(bool check, int parentid, int moduleid, int functionid, string roleid)
        {
            try
            {
                var effectRowCount = dalPermission.SavePermission(check, parentid, moduleid, functionid, roleid);

                return Json(new ExtResult { success = true, effectRowCount = effectRowCount });
            }
            catch (Exception ex)
            {
                return Json(new ExtResult { success = false, msg = ex.Message });
            }
        }
        #endregion

    }
}