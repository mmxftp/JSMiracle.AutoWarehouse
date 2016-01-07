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
    public class OperationalController : BaseController
    {
        private IOperationalTasks dalOperationalTasks;

        public OperationalController(IOperationalTasks repoOperationalTasks)
        {

        }

        //
        // GET: /WT/HandingTask/

        public ActionResult Index()
        {
            return View();
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
