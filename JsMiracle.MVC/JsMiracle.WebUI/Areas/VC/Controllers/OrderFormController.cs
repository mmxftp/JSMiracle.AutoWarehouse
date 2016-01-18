using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.WCF.VC.IOrderForm;
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
using JsMiracle.Entities.Enum;
using JsMiracle.WCF.CM.ICommonMng;
using JsMiracle.WCF.CB.ICoreBussiness;

namespace JsMiracle.WebUI.Areas.VC.Controllers
{
    public class OrderFormController : BaseController
    {
        IOrderForm dalorderform;
        IOrderData dalorderdata;
        ICode dalCode;
        IItem dalItem;

        public OrderFormController(IOrderForm repoOrderForm
            , IOrderData repoOrderData
            , ICode repoCode
            , IItem repoItem)
        {
            this.dalorderform = repoOrderForm;
            this.dalorderdata = repoOrderData;
            this.dalCode = repoCode;
            this.dalItem = repoItem;
        }

        //
        // GET: /VC/OrderForm/

        [AuthViewPage]
        public ActionResult Index()
        {
            return View();
        }

        [AuthViewPage]
        public ActionResult InStorageIndex()
        {
            ViewBag.BizType = EnumBusinessType.InStorage;
            return View("Index");
        }

        [AuthViewPage]
        public ActionResult OutputStorageIndex()
        {
            ViewBag.BizType = EnumBusinessType.OutputStorage;
            return View("Index");
        }

        public ActionResult GetOrderList(int? rows, int? page
            , EnumBusinessType bizType = EnumBusinessType.None)
        {
            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            string filter = null;

            if (bizType != EnumBusinessType.None)
            {
                filter = string.Format(" YWLX = {0} ", (int)bizType);
            }

            var dataList = dalorderform.GetDataByPageDynamic(
                pageIndex, pageSize, out totalCount
                , "CJSJ", filter);


            //数据组装到viewModel
            var info = new PaginationModel(dataList, totalCount);

            return this.JsonFormat(info);
        }

        private void GetDJT(EnumBusinessType bizType, ref IMS_VC_DJT module)
        {

            ViewBag.BizType = "";

            if (bizType == EnumBusinessType.InStorage)
            {
                module.YWLX = (int)EnumBusinessType.InStorage;
                ViewBag.ReturnUrl = Url.Action("InStorageIndex");
                ViewBag.BizType = EnumBusinessType.InStorage;

                //GetBizTypeList();
            }
            else if (bizType == EnumBusinessType.OutputStorage)
            {
                module.YWLX = (int)EnumBusinessType.OutputStorage;
                ViewBag.ReturnUrl = Url.Action("OutputStorageIndex");
                ViewBag.BizType = EnumBusinessType.OutputStorage;
            }

            module.CJSJ = System.DateTime.Now;

            ViewBag.DocumentStatus = new SelectList(
              GetDocumentStatusList(), "SZ", "MC", -1);

            ViewBag.DJStatus = GetStatus(module.DJZT);
        }


        public ActionResult CreateOrderForm(EnumBusinessType bizType)
        {
            var module = new IMS_VC_DJT();
            module.DJZT = 0;
            GetDJT(bizType, ref module);

            module.CJSJ = System.DateTime.Now;

            return View("EditOrderForm", module);
        }

        [AuthViewPage]
        public ActionResult EditOrderForm(long id)
        {
            var order = dalorderform.GetEntity(id);

            ViewBag.BizType = "";

            ViewBag.DocumentStatus = new SelectList(
                GetDocumentStatusList(), "SZ", "MC", order.DJZT);

            if (order == null)
                throw new JsMiracleException("单据不存在");

            var code = dalCode.GetCode(CodeTypeEnum.BusinessType, order.YWLX);
            if (code == null)
                throw new JsMiracleException("业务类型不存在");

            var bizType = FunctionHelp.GetEnum<EnumBusinessType>(code.SZ);
            GetDJT(bizType, ref order);

            return View(order);
        }

        /// <summary>
        /// 单据状态
        /// </summary>
        /// <returns></returns>
        private IList<IMS_CM_DM> GetDocumentStatusList()
        {
            var data = dalCode.GetCodeList(CodeTypeEnum.VH_STS);
            data.Insert(0, new IMS_CM_DM() { SZ = -1, MC = "--请选择--" });
            return data;
        }

        ///// <summary>
        ///// 业务类型
        ///// </summary>
        ///// <returns></returns>
        //private IList<IMS_CM_DM> GetBizTypeList(string startNumber)
        //{
        //    var data = dalCode.GetCodeList(CodeTypeEnum.BusinessType);
        //    data.Insert(0, new IMS_CM_DM() { SZ = -1, MC = "--请选择--" });
        //    return data;
        //}


        private IList<IMS_CB_WL> GetItemList()
        {
            var data = dalItem.GetAllList();
            data.Insert(0, new IMS_CB_WL() { ID = -1, WLBH = "--请选择--" });
            return data;
        }

        [AuthViewPage]
        public ActionResult EditOrderData(long id)
        {
            ViewBag.Items = new SelectList(
                GetItemList(), "ID", "WLBH", -1);

            var ent = dalorderdata.GetEntity(id);
            return View(ent);
        }


        [AuthViewPage]
        public ActionResult CreateOrderData(string djbh)
        {
            ViewBag.Items = new SelectList(
                GetItemList(), "ID", "WLBH", -1);

            return View("EditOrderData", new IMS_VC_DJH() { DJBH = djbh });
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
            Func<ExtResult> fun = () =>
            {
                ExtResult ret = new ExtResult();
                dalorderdata.Delete(id);
                ret.success = true;
                return ret;
            };

            return base.Remove(fun);
        }

        public ActionResult RemoveOrderForm(long id)
        {
            Func<ExtResult> fun = () =>
            {
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

        /// <summary>
        /// 得到状态名称
        /// </summary>
        /// <param name="djzt"></param>
        /// <returns></returns>
        private MvcHtmlString GetStatus(int djzt)
        {
            var code = dalCode.GetCode(CodeTypeEnum.VH_STS, djzt);
            if (code != null)
                return MvcHtmlString.Create(code.MC);

            return MvcHtmlString.Create("");
        }


    }
}
