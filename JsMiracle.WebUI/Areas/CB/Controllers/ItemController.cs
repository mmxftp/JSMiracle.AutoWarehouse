using JsMiracle.Dal.Abstract.CB;
using JsMiracle.Dal.Abstract.CM;
using JsMiracle.Entities;
using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Framework;
using JsMiracle.WebUI.CommonSupport;
using JsMiracle.WebUI.Controllers;
using JsMiracle.WebUI.Controllers.Filter;
using JsMiracle.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JsMiracle.WebUI.Areas.CB.Controllers
{
    public class ItemController : BaseController
    {


        IItem dalItem;
        ICode dalCode;

        public ItemController(IItem repoItem, ICode repoCode)
        {
            this.dalItem = repoItem;
            this.dalCode = repoCode;
        }

        //
        // GET: /CB/Item/
        [AuthViewPage]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveItem(IMS_CB_WL entity)
        {
            var ret = new ExtResult();
            try
            {
                if (ModelState.IsValid)
                {
                    if (entity.ID == 0)
                    {
                        if (dalItem.Exists(f => f.WLBH.Equals(entity.WLBH, StringComparison.CurrentCultureIgnoreCase)))
                            throw new JsMiracleException("物料编号不得重覆");

                        // 创建人
                        entity.CJR = CurrentUser.GetCurrentUser().UserInfo.YHID;
                        entity.CJSJ = System.DateTime.Now;
                    }
                    else
                    {
                        if (dalItem.Exists(f => f.WLBH.Equals(entity.WLBH, StringComparison.CurrentCultureIgnoreCase)
                            && f.ID != entity.ID))
                            throw new JsMiracleException("物料编号不得重覆");
                    }

                    // 修改人
                    entity.XGR = CurrentUser.GetCurrentUser().UserInfo.YHID;
                    entity.XGSJ = System.DateTime.Now;

                    dalItem.SaveOrUpdate(entity);
                    ret.success = true;
                    ret.msg = "物料保存成功";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    var errs = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var err in errs)
                    {
                        sb.AppendFormat("{0}</br>", err.ErrorMessage);
                    }
                    ret.success = false;
                    ret.msg = sb.ToString();
                }
            }
            catch (Exception ex)
            {
                ret.success = false;
                if (ex is JsMiracleException)
                    ret.msg = ex.Message;
                else
                    ret.msg = string.Format("{0}-{1}", ex.Message, ex.InnerException);
            }
            return this.JsonFormat(ret);
        }

        public ActionResult RemoveItem(long id)
        {
            try
            {
                dalItem.Delete(id);
                return this.JsonFormat(new ExtResult { success = true, msg = "删除成功" });

            }
            catch (Exception ex)
            {
                var ret = new ExtResult();
                ret.success = false;
                if (ex is JsMiracleException)
                    ret.msg = ex.Message;
                else
                    ret.msg = string.Format("{0}-{1}", ex.Message, ex.InnerException);

                return this.JsonFormat(ret);
            }
        }


        //public ActionResult GetItemTypeList()
        //{
        //    var data = dalCode.GetCodeList(itemTypeName);
        //    return this.JsonFormat(data);
        //}

        public ActionResult EditIem(long id)
        {
            var ent = dalItem.GetEntity(id);
            if (ent == null)
                throw new JsMiracleException(string.Format("不存在物料'{0}' ", id));


            ViewBag.ItemType = new SelectList(
                GetItemTypeList(), "SZ", "MC", ent.WLLX);

            return View(ent);
        }

        private IList<IMS_CM_DM> GetItemTypeList()
        {
            var data = dalCode.GetCodeList(IMS_CB_WL.ItemTypeName);
            data.Insert(0, new IMS_CM_DM() {  SZ = -1, MC = "--请选择--" });
            return data;
        }


        public ActionResult CreateItem()
        {
            ViewBag.ItemType = new SelectList(
                GetItemTypeList(), "SZ", "MC" ,-1);

            return View("EditIem", new IMS_CB_WL());
        }

        public ActionResult GetItemList(int? rows, int? page)
        {
            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            Expression<Func<IMS_CB_WL, bool>> filter = null;
            //System.Linq.Dynamic.DynamicExpression.ParseLambda()

            //var dataList = dalItem.GetDataByPage(
            //    o => o.WLMC,
            //    filter,
            //    pageIndex, pageSize, out totalCount);

            var dataList = dalItem.GetDataByPage(pageIndex, pageSize, out totalCount, "item.WLMC", "");

            //数据组装到viewModel
            var info = new PaginationModel<IMS_CB_WL>();
            info.total = totalCount;
            info.rows = dataList;

            return this.JsonFormat(info);
        }


        //public IEnumerable<SelectListItem> GetItemType()
        //{

        //}

    }
}
