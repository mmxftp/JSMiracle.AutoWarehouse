using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
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
        IUser dalUser;

        public RoleUserController(IRoleUser repoRoleUser
            , IRole repoRole
            , IUser repoUser)
        {
            dalRoleUser = repoRoleUser;
            dalRole = repoRole;
            dalUser = repoUser;
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

    }
}
