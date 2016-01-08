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
    public class OperationalTasksController : BaseController
    {
        private IOperationalTasks dalOperationalTasks;

        public OperationalTasksController(IOperationalTasks repoOperationalTasks)
        {
            dalOperationalTasks = repoOperationalTasks;
        }

        //
        // GET: /WT/HandingTask/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetOperationalTaskList(int? rows, int? page)
        {

            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            var dataList = dalOperationalTasks.GetDataByPageDynamic(
                pageIndex, pageSize, out totalCount, " RWBH ", "");

            //数据组装到viewModel
            var info = new PaginationModel(dataList, totalCount);

            return this.JsonFormat(info);
        }

        public ActionResult Edit(long id)
        {
            var ent = dalOperationalTasks.GetEntity(id);
            return View(ent);
        }

        public ActionResult Create()
        {
            var ent = new IMS_WT_CWRW();
            return View("Edit", ent);
        }

        public ActionResult Save(IMS_WT_CWRW ent)
        {
            Func<ExtResult> fun = () =>
            {
                if (ent.ID == 0)
                {
                    ent.CJR = CurrentUser.GetCurrentUser().UserInfo.YHID;
                    ent.CJSJ = System.DateTime.Now;
                }
                ExtResult ret = new ExtResult();
                dalOperationalTasks.SaveOrUpdate(ent);
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
                dalOperationalTasks.Delete(id);
                ret.success = true;
                return ret;
            };

            return base.Remove(fun);
        }

    }
}
