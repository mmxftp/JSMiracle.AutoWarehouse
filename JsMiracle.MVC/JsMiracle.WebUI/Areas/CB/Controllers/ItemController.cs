﻿using JsMiracle.Entities;
using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Entities.Enum;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Framework;
using JsMiracle.WCF.CB.ICoreBussiness;
using JsMiracle.WCF.CM.ICommonMng;
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
            Func<ExtResult> saveFun = () =>
            {
                if (entity.ID == 0)
                {
                    string filter = string.Format("WLBH = \"{0}\" ", entity.WLBH);

                    //if (dalItem.Exists(f => f.WLBH.Equals(entity.WLBH, StringComparison.CurrentCultureIgnoreCase)))

                    if (dalItem.Exists(filter))
                        throw new JsMiracleException("物料编号不得重覆");

                    // 创建人
                    entity.CJR = CurrentUser.GetCurrentUser().UserInfo.YHID;
                    entity.CJSJ = System.DateTime.Now;
                }
                else
                {
                    string filter = string.Format("WLBH = \"{0}\" && ID != {1} ", entity.WLBH, entity.ID);

                    if (dalItem.Exists(filter))
                        throw new JsMiracleException("物料编号不得重覆");
                }

                // 修改人
                entity.XGR = CurrentUser.GetCurrentUser().UserInfo.YHID;
                entity.XGSJ = System.DateTime.Now;

                dalItem.SaveOrUpdate(entity);
                ExtResult ret = new ExtResult();
                ret.success = true;
                return ret;
            };

            return base.Save(saveFun);
        }

        public ActionResult RemoveItem(long id)
        {
            Func<ExtResult> removeFun = () =>
            {
                dalItem.Delete(id);
                var ret = new ExtResult();
                ret.success = true;
                return ret;
            };

            return base.Remove(removeFun);
        }

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
            var data = dalCode.GetCodeList(CodeTypeEnum.ItemType);
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

            //Expression<Func<IMS_CB_WL, bool>> filter = null;
            //System.Linq.Dynamic.DynamicExpression.ParseLambda()

            //var dataList = dalItem.GetDataByPage(
            //    o => o.WLMC,
            //    filter,
            //    pageIndex, pageSize, out totalCount);

            var dataList = dalItem.GetDataByPageDynamic(pageIndex, pageSize, out totalCount, "item.WLMC", "");

            //数据组装到viewModel
            var info = new PaginationModel(dataList, totalCount);
            

            return this.JsonFormat(info);
        }



        public ActionResult GetAllItemList()
        {
            var data = dalItem.GetAllList();
            data.Insert(0, new IMS_CB_WL() { ID = -1, WLMC = "--请选择--" });
            return this.JsonFormat(data);
        }


    }
}
