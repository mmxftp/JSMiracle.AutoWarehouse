using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JsMiracle.Entities;
using System.Linq.Expressions;
using JsMiracle.WebUI.Models;
using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.WebUI.Controllers.Filter;
using JsMiracle.Framework.Cache;
using JsMiracle.Framework;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WebUI.Controllers;
using JsMiracle.WCF.UP.IAuthMng;
using JsMiracle.WebUI.CommonSupport;

namespace JsMiracle.WebUI.Areas.UP.Controllers
{
    public class ModuleController : BaseController
    {
        //
        // GET: /Module/
        [AuthViewPage]
        public ActionResult Index()
        {
            return View();
        }

        private IModule dalModule;
        private IModuleFunction dalFunction;
        //private IActionPermission dalAction;
        private IPermission dalPermission;


        public ModuleController(IModule repoModule, IModuleFunction repoFun, IPermission repoAction)
        {
            dalModule = repoModule;
            dalFunction = repoFun;
            //dalAction = repoAction;
            dalPermission = repoAction;

        }

        public ActionResult ResetCache()
        {
            ActionPermission.ResetCache();
            return this.JsonFormat(new ExtResult { success = true });
        }

        #region IMS_UP_ModuT 表操作

        public ActionResult GetModuleList()
        {
            var data = dalModule.GetRootModule();
            return this.JsonFormat(data);
        }

        public ActionResult GetChildModuleList(int? rows, int? page, int? parentid)
        {
            //var data = moduleInfo.FindWhere(n => n.ParentID == parentid);
            //var info = new PaginationModel<IMS_UP_MK>();
            //var info = new PaginationModel();

            if (parentid == null)
            {
                // 解决easyui length的问题
                var nullInfo = new PaginationModel(new List<IMS_UP_MK>(),0);
                return Json(nullInfo);
            }


            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            //var dataList = dalModule.GetDataByPage(
            //    o => o.MKMC
            //    , f => f.FMKID == parentid.Value
            //    , pageIndex, pageSize, out totalCount);

            string filter = string.Format( "FMKID = {0} ",parentid.Value) ;
            var dataList = dalModule.GetDataByPageDynamic(pageIndex, pageSize, out totalCount, " PXID ", filter);

            //数据组装到viewModel
            var info = new PaginationModel(dataList, totalCount);

            //var json = Json(info);
            return this.JsonFormat(info);
            //return Json(info, ContextType);
        }

        public ViewResult EditModule(long id)
        {
            var user = dalModule.GetEntity(id);
            return View(user);
        }

        public ActionResult SaveModule(IMS_UP_MK module)
        {
            Func<ExtResult> saveFun = () =>
            {
                module.URL = module.GetUrl();



                dalModule.SaveOrUpdate(module);

                var parentModule = dalModule.GetEntityByModuleID(module.FMKID);
                long pid = -1;
                if (parentModule != null)
                    pid = parentModule.ID;

                ActionPermission.ResetCache();

                var ret = new ExtResult();
                ret.success = true;
                ret.id = pid;
                ret.parentid = module.FMKID;
                return ret;
            };

            return base.Save(saveFun);     
        }

        public ViewResult CreateModule(int parentid = -1)
        {
            // parentid 与 moduleid 是主从关系, 数据表中的id只是主键没有业务关系
            if (parentid != -1)
            {
                var ent = dalModule.GetEntity(parentid);
                if (ent != null)
                    parentid = ent.MKID;
            }

            var newEnt = new IMS_UP_MK() { FMKID = parentid };

            return View("EditModule", newEnt);
        }


        public ActionResult RemoveModule(long id)
        {
            try
            {
                dalModule.Delete(id);

                ActionPermission.ResetCache();
                return this.JsonFormat(new { success = true });
            }
            catch (Exception ex)
            {
                return this.JsonFormat(new ExtResult { success = false, msg = ex.Message });
            }
        }

        #endregion

        #region IMS_UP_MoFnTT 表操作


        public ActionResult IndexFunction(int moduleid)
        {
            ViewData["moduleid"] = moduleid;
            return View();
        }

        public ActionResult GetModuleFunctionList(int moduleid)
        {
            var data = dalFunction.GetModuleFunctionList(moduleid);

            var info = new PaginationModel(data,0);
            //var info = new PaginationModel();


            //info.total = (data != null) ? data.Count : 0;
            //info.rows = data;

            //info.SetRows(data);

            return this.JsonFormat(info);
        }

        public ViewResult CreateFunction(int moduleid)
        {
            var mod = dalModule.GetEntityByModuleID( moduleid);
            if (mod != null)
                ViewBag.ModName = mod.MKMC;

            return View("EditFunction", new IMS_UP_MKGN() { MKID = moduleid });
        }

        public ViewResult EditFunction(long id)
        {
            var fun = dalFunction.GetEntity(id);

            var mod = dalModule.GetEntityByModuleID( fun.MKID);
            if (mod != null)
                ViewBag.ModName = mod.MKMC;

            return View(fun);
        }

        public ActionResult SaveFunction(IMS_UP_MKGN module)
        {
            Func<ExtResult> saveFun = () => 
            {
                dalFunction.SaveOrUpdate(module);

                ActionPermission.ResetCache();
                ExtResult ret = new ExtResult();
                ret.success = true;
                return ret;
            };

            return base.Save(saveFun);         
        }


        public ActionResult RemoveFunction(long id)
        {
            try
            {
                dalFunction.Delete(id);

                ActionPermission.ResetCache();
                return Json(new ExtResult { success = true });
            }
            catch (Exception ex)
            {
                return Json(new ExtResult { success = false, msg = ex.Message });
            }
        }

        #endregion
    }
}
