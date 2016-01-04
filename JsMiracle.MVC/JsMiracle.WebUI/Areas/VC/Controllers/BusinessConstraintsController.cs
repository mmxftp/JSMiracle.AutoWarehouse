using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.VC.IOrderForm;
using JsMiracle.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JsMiracle.Framework;

namespace JsMiracle.WebUI.Areas.VC.Controllers
{
    public class BusinessConstraintsController : BaseController
    {
        private IBusinessConstraints dalBusinessConstraints;

        public BusinessConstraintsController(IBusinessConstraints repoBusinessContraints)
        {
            dalBusinessConstraints = repoBusinessContraints;
        }

        //
        // GET: /VC/BusinessConstraints/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetBusinessConstraintsList(int? rows, int? page)
        {

            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            var dataList = dalBusinessConstraints.GetDataByPageDynamic(
                pageIndex, pageSize, out totalCount, " YWLX ", "");

            //数据组装到viewModel
            var info = new PaginationModel(dataList, totalCount);

            return this.JsonFormat(info);
        }


        public ActionResult Edit(long id)
        {
            var ent = dalBusinessConstraints.GetEntity(id);
            return View(ent);
        }

        public ActionResult Create()
        {
            var ent = new IMS_VC_YWYS();
            return View("Edit", ent);
        }

        public ActionResult Save(IMS_VC_YWYS ent)
        {
            Func<ExtResult> fun = () =>
            {
                ExtResult ret = new ExtResult();
                ent.XGSJ = System.DateTime.Now;
                dalBusinessConstraints.SaveOrUpdate(ent);
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
                dalBusinessConstraints.Delete(id);
                ret.success = true;
                return ret;
            };

            return base.Remove(fun);
        }

    }
}
