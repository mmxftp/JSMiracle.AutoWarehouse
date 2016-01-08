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
    public class HandingTasksController : BaseController
    {
        private IHandlingTasks dalHandlingTasks;

        public HandingTasksController(IHandlingTasks repoHandlingTasks)
        {
            dalHandlingTasks = repoHandlingTasks;
        }

        //
        // GET: /WT/HandingTask/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetHandingTaskList(int? rows, int? page)
        {

            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            var dataList = dalHandlingTasks.GetDataByPageDynamic(
                pageIndex, pageSize, out totalCount, " RWBH ", "");

            //数据组装到viewModel
            var info = new PaginationModel(dataList, totalCount);

            return this.JsonFormat(info);
        }


        public ActionResult Edit(long id)
        {
            var ent = dalHandlingTasks.GetEntity(id);
            return View(ent);
        }

        public ActionResult Create()
        {
            var ent = new IMS_WT_BYRW();
            return View("Edit", ent);
        }

        public ActionResult Save(IMS_WT_BYRW ent)
        {
            Func<ExtResult> fun = () =>
            {
                if (ent.ID == 0)
                {
                    ent.CJR = CurrentUser.GetCurrentUser().UserInfo.YHID;
                    ent.CJSJ = System.DateTime.Now;
                }
                ExtResult ret = new ExtResult();
                dalHandlingTasks.SaveOrUpdate(ent);
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
                dalHandlingTasks.Delete(id);
                ret.success = true;
                return ret;
            };

            return base.Remove(fun);
        }

    }
}
