using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.WT.IWorkTasks;
using JsMiracle.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JsMiracle.Framework;
using JsMiracle.WebUI.CommonSupport;

namespace JsMiracle.WebUI.Areas.WT.Controllers
{
    public class ExecutorController : BaseController
    {
        IExecutor dalExecutor;

        public ExecutorController(IExecutor repoExecutor)
        {
            dalExecutor = repoExecutor;
        }

        //
        // GET: /WT/Executor/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetExecutorList(int? rows, int? page)
        {

            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            var dataList = dalExecutor.GetDataByPageDynamic(
                pageIndex, pageSize, out totalCount, " ZXZDM ", "");

            //数据组装到viewModel
            var info = new PaginationModel(dataList, totalCount);

            return this.JsonFormat(info);
        }


        public ActionResult Edit(long id)
        {
            var ent = dalExecutor.GetEntity(id);
            return View(ent);
        }

        public ActionResult Create()
        {
            var ent = new IMS_WT_ZXZ();
            return View("Edit", ent);
        }

        public ActionResult Save(IMS_WT_ZXZ ent)
        {
            Func<ExtResult> fun = () =>
            {
                ExtResult ret = new ExtResult();
                dalExecutor.SaveOrUpdate(ent);
                ret.success = true;
                return ret;
            };

            return base.Save(fun);
        }


        public ActionResult Remove(long id)
        {
            Func<ExtResult> fun = () =>
            {
                ExtResult ret = new ExtResult();
                dalExecutor.Delete(id);
                ret.success = true;
                return ret;
            };

            return base.Remove(fun);
        }

    }
}
