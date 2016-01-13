using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WebUI.Controllers;
using JsMiracle.WebUI.Controllers.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using JsMiracle.Framework;
using JsMiracle.Entities.Enum;
using JsMiracle.WebUI.CommonSupport;
using JsMiracle.WCF.CM.ICommonMng;
using JsMiracle.Framework.Serialized;

namespace JsMiracle.WebUI.Areas.CM.Controllers
{
    public class ObjectDataController : BaseController
    {
        IObjectDataDictionary dalObjectData;
        ICode dalCode;
        IUserObject dalUserObj;

        public ObjectDataController(IObjectDataDictionary repoDic, ICode repoCode, IUserObject repoUserObj)
        {
            this.dalObjectData = repoDic;
            this.dalCode = repoCode;
            this.dalUserObj = repoUserObj;
        }

        //
        // GET: /CM/ObjectData/
        [AuthViewPage]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTables()
        {
            var data = dalCode.GetCodeList(CodeTypeEnum.TableName);
            if (data != null)
            {
                foreach (var d in data)
                {
                    d.MC = string.Format("{0}({1})", d.MC, d.DM);
                }
            }
            return this.JsonFormat(data);
        }

        /// <summary>
        /// 得数据库中实际存在的数据
        /// </summary>
        /// <param name="tablename">表名称</param>
        /// <returns></returns>
        public ActionResult GetTableInfo(string tablename)
        {
            string filter = string.Format(" DM = \"{0}\" &&  LXDM = \"{1}\" "
                , tablename, CodeTypeEnum.TableName.ToString());

            //var code = dalCode.GetAllEntites(n => n.MC.Equals(tablename)
            //   && n.LXDM == CodeTypeEnum.TableName.ToString()).FirstOrDefault();
            var code = dalCode.GetAllEntites(filter).FirstOrDefault();

            var dm = "";
            if (code != null)
                dm = code.DM;

            filter = string.Format("DXDM = \"{0}\" ", dm);

            var dataList =
                dalObjectData.GetAllEntites(filter);

            return this.JsonFormat(dataList);
        }

        /// <summary>
        /// 重新从数据库的系统表载入数据更新对象表
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <returns></returns>
        public ActionResult ReSaveObjectDat(string tablename)
        {
            ExtResult ret = new ExtResult();

            try
            {
                dalObjectData.ReSaveObjectData(tablename
                            , CurrentUser.GetCurrentUser().UserInfo.YHID);
                ret.success = true;
                ret.msg = "处理成功";
            }
            catch (Exception ex)
            {
                ret.success = false;
                if (ex is JsMiracleException)
                    ret.msg = ex.Message;
                else
                {
                    ret.msg = string.Format("{0}:{1}", ex.Message, ex.StackTrace);
                }
            }

            return this.JsonFormat(ret);
        }

        [AuthViewPage]
        public ActionResult EditObjectData(long id)
        {
            var ent = dalObjectData.GetEntity(id);

            string filter = string.Format("LXDM = \"{0}\" && DM =\"{1}\" "
                , CodeTypeEnum.TableName.ToString(), ent.DXDM);

            var code = dalCode.GetAllEntites(filter).FirstOrDefault();

            ViewBag.DXMC = string.Format("{0}({1})", code.MC, ent.DXDM);

            //var ent = dalObjectData.GetEntity(id);
            return View(ent);
        }


        public ActionResult SaveObjectData(IMS_CM_DXXX entity)
        {
            Func<ExtResult> saveFun = () =>
            {
                //entity.XGR = CurrentUser.GetCurrentUser().UserInfo.YHID;
                entity.XGRQ = System.DateTime.Now;
                dalObjectData.SaveOrUpdate(entity);
                ExtResult ret = new ExtResult();
                ret.success = true;
                ret.msg = "保存成功";

                return ret;
            };

            return base.Save(saveFun);
        }

        public ActionResult IndexUserConditions()
        {
            return View();
        }



        public ActionResult GetConditionList(int? rows, int? page, string tablename)
        {
            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            string filter = string.Format(" YHID =\"{0}\" ", CurrentUser.GetCurrentUser().UserInfo.YHID);

            if (!string.IsNullOrEmpty(tablename))
            {
                filter += string.Format(" and DXDM =\"{0}\" ", tablename);
            }


            var dataList = dalUserObj.GetDataByPageDynamic(pageIndex, pageSize, out totalCount
                , "CXMC", filter);

            //数据组装到viewModel
            var info = new PaginationModel(dataList, totalCount);

            return this.JsonFormat(info);
        }


        public ActionResult EditCondition(long id)
        {
            ViewBag.OnlyFind = false;
            var ent = dalUserObj.GetEntity(id);

            ViewBag.fieldJson = "[]";
            ViewBag.Express = MvcHtmlString.Create("null");
            if (ent != null)
            {
                ViewBag.fieldJson = GetFieldList(ent.DXDM);
                ViewBag.Express = MvcHtmlString.Create(ent.DXBDS);
            }
            return View(ent);
        }

        public ActionResult CreateCondition(string tablename, bool onlyFind=false )
        {
            ViewBag.OnlyFind = onlyFind;

            var ent = new IMS_CM_YHDX()
            {
                DXDM = tablename,
                YHID = CurrentUser.GetCurrentUser().UserInfo.YHID
            };
            ViewBag.fieldJson = GetFieldList(ent.DXDM);
            ViewBag.Express = MvcHtmlString.Create("null");
        
            return View("EditCondition", ent);
        }

        public ActionResult RemoveCondition(long id)
        {
            Func<ExtResult> removeFun = () =>
            {
                dalUserObj.Delete(id);
                var ret = new ExtResult();
                ret.success = true;
                return ret;
            };

            return base.Remove(removeFun);
        }

        public MvcHtmlString GetFieldList(string tablename)
        {
            if (string.IsNullOrEmpty(tablename))
                return MvcHtmlString.Create(null);

            var dataList =  dalObjectData.GetObjectColumnList(tablename);

            var str = JsonSerialization.Serialize(dataList);
            return MvcHtmlString.Create(str);
        }

        public ActionResult SaveCondition(IMS_CM_YHDX entity)
        {
            Func<ExtResult> saveFun = () =>
            {
                entity.XGRQ = System.DateTime.Now;
                entity.YHID = CurrentUser.GetCurrentUser().UserInfo.YHID;
                dalUserObj.SaveOrUpdate(entity);
                ExtResult ret = new ExtResult();
                ret.success = true;
                ret.msg = "保存成功";
                ret.retString = entity.DXTJ;
                return ret;
            };

            return base.Save(saveFun);
        }

    }
}
