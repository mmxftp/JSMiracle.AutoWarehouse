using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.WT.IWorkTasks;
using JsMiracle.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JsMiracle.WebUI.Areas.WT.Controllers
{
    public class HandingTaskController : BaseController
    {
        private IHandlingTasks dalHandlingTasks;

        public HandingTaskController(IHandlingTasks repoHandlingTasks)
        {
            dalHandlingTasks = repoHandlingTasks;
        }

        //
        // GET: /WT/HandingTask/

        public ActionResult Index()
        {
            return View();
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
