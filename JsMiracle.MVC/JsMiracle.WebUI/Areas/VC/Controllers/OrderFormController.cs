using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Wcf.VC.IOrderForm;
using JsMiracle.WebUI.Controllers;
using JsMiracle.WebUI.Controllers.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JsMiracle.Framework;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WebUI.CommonSupport;
using JsMiracle.Framework.Serialized;

namespace JsMiracle.WebUI.Areas.VC.Controllers
{
    public class OrderFormController : BaseController
    {
        IOrderForm dalorderform;
        IOrderData dalorderdata;

        public OrderFormController(IOrderForm repoOrderForm, IOrderData repoOrderData)
        {
            this.dalorderform = repoOrderForm;
            this.dalorderdata = repoOrderData;
        }

        //
        // GET: /VC/OrderForm/

        [AuthViewPage]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetOrderList(int? rows, int? page)
        {
            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            string filter = null;

            var dataList = dalorderform.GetDataByPageDynamic(
                pageIndex, pageSize, out totalCount
                , "CJSJ", filter);


            //数据组装到viewModel
            var info = new PaginationModel(dataList, totalCount);

            return this.JsonFormat(info);
        }

        public ActionResult EditOrderForm(long id)
        {
            var module = dalorderform.GetEntity(id);
            return View(module);
        }

        public ActionResult CreateOrderForm()
        {
            var module = new IMS_VC_DJT();
            return View("EditOrderForm", module);
        }


        public ActionResult EditOrderData(long id)
        {
            var ent = dalorderdata.GetEntity(id);
            return View(ent);
        }

        public ActionResult CreateOrderData(string djbh)
        {
            return View("EditOrderData", new IMS_VC_DJH() { DJBH = djbh});
        }

        public ActionResult SaveOrderForm(IMS_VC_DJT ent)
        {
            Func<ExtResult> fun = () =>
            {
                ExtResult ret = new ExtResult();
                ent.CJR = CurrentUser.GetCurrentUser().UserInfo.YHID;
                ent.CJSJ = System.DateTime.Now;
                dalorderform.SaveOrUpdate(ent);
                ret.id = ent.ID;
                ret.success = true;
                return ret;
            };

            return base.Save(fun);
        }

        public ActionResult GetOrderData(string djbh)
        {
            List<IMS_VC_DJH> dataList;
            if (string.IsNullOrEmpty(djbh))
            {
                dataList = new List<IMS_VC_DJH>();
                return this.JsonFormat(dataList);
            }

            string filter = string.Format(" djbh == \"{0}\" ", djbh);

            dataList = dalorderdata.GetAllEntites(filter);

            return this.JsonFormat(dataList);
        }

        public ActionResult RemoveOrderData(long id)
        {
            Func<ExtResult> fun=()=>{
                ExtResult ret = new ExtResult();
                dalorderdata.Delete(id);
                ret.success = true;
                return ret;
            } ;

            return base.Remove(fun);
        }

        public ActionResult RemoveOrderForm(long id)
        {
            Func<ExtResult> fun = () => {
                ExtResult ret = new ExtResult();
                dalorderform.Delete(id);
                ret.success = true;
                return ret;
            };

            return base.Remove(fun);
        }

        public ActionResult SaveOrderData(IMS_VC_DJH ent)
        {
            Func<ExtResult> fun = () =>
            {
                ExtResult ret = new ExtResult();
                ent.CJR = CurrentUser.GetCurrentUser().UserInfo.YHID;
                ent.CJSJ = System.DateTime.Now;
                dalorderdata.SaveOrUpdate(ent);
                ret.success = true;
                return ret;
            };

            return base.Save(fun);
        }

    }
}
