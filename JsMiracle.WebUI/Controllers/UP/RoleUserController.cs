using JsMiracle.Dal.Abstract;
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
using JsMiracle.Dal.Abstract.UP;

namespace JsMiracle.WebUI.Controllers.UP
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
            var info = new PaginationModel<IMS_UP_YH>();

            if (string.IsNullOrEmpty(roleid))
            {
                info.total = 0;
                info.rows = new List<IMS_UP_YH>();   // 解决easyui length的问题
                return this.JsonFormat(info);
            }

            var data = dalRoleUser.GetUserList(roleid);
            info.total = data.Count;
            info.rows = data;

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
            //var data = moduleInfo.FindWhere(n => n.ParentID == parentid);
            var info = new PaginationModel<IMS_UP_JS>();

            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            var dataList = dalRole.GetDataByPage(
              o => o.JSMC,
              null,
              pageIndex, pageSize, out totalCount);

            //数据组装到viewModel
            info.total = totalCount;
            info.rows = dataList;

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
            if (ModelState.IsValid)
            {
                try
                {

                    if (string.IsNullOrEmpty(module.JSID))
                    {
                        module.JSID = Guid.NewGuid().ToString();
                    }

                    dalRole.SaveOrUpdate(module);
                }
                catch (Exception ex)
                {
                    return this.JsonFormat(new ExtResult { success = true, msg = "操作失败" + ex.Message });
                }

                return this.JsonFormat(new ExtResult { success = true, msg = "操作成功" });
            }
            else
            {
                return this.JsonFormat(module);
            }
        }


        public ActionResult RemoveRole(long id)
        {
            try
            {
                //var ent = dalRole.GetEntity(id);

                //if (ent != null )
                //{
                //    dalRoleUser.GetUserList(ent.RoleID);

                //    var permissionlist = dalPermission.GetPermissionListByRoleID(ent.RoleID);
                //}

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
