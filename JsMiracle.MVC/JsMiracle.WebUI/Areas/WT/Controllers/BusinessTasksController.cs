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
    /// <summary>
    /// 业务类任务
    /// </summary>
    public class BusinessTasksController : BaseController
    {
        IBusinessTasks dalBusinessTasks;

        public BusinessTasksController(IBusinessTasks repoBusinessTasks)
        {
            dalBusinessTasks = repoBusinessTasks;
        }

        //
        // GET: /WT/BusinessTasks/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetBusinessTaskList(int? rows, int? page)
        {

            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            var dataList = dalBusinessTasks.GetDataByPageDynamic(
                pageIndex, pageSize, out totalCount, " DJBH ", "");

            //数据组装到viewModel
            var info = new PaginationModel(dataList, totalCount);

            return this.JsonFormat(info);
        }

        public ActionResult Edit(long id)
        {
            var ent = dalBusinessTasks.GetEntity(id);
            return View(ent);
        }

        public ActionResult Create()
        {
            var ent = new IMS_WT_YWRW();
            return View("Edit", ent);
        }

        public ActionResult Save(IMS_WT_YWRW ent)
        {
            Func<ExtResult> fun = () =>
            {
                if (ent.ID == 0)
                {
                    ent.CJR = CurrentUser.GetCurrentUser().UserInfo.YHID;
                    ent.CJSJ = System.DateTime.Now;
                }
                ExtResult ret = new ExtResult();
                dalBusinessTasks.SaveOrUpdate(ent);
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
                dalBusinessTasks.Delete(id);
                ret.success = true;
                return ret;
            };

            return base.Remove(fun);
        }

        /// <summary>
        /// 根据单据行主键，得业务任务数据集
        /// </summary>
        /// <param name="DJH_ID">单据行主键</param>
        /// <returns></returns>
        public ActionResult GetListByDJHID(long DJH_ID)
        {
            var data = dalBusinessTasks.GetAllEntites(string.Format(" DJH_ID == {0} ", DJH_ID));

            return this.JsonFormat(data);
        }

    }
}
