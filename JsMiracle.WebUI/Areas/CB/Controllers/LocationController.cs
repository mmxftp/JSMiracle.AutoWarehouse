using JsMiracle.Dal.Abstract.CB;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using JsMiracle.Framework;
using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.WebUI.Controllers.Filter;
using JsMiracle.WebUI.Controllers;
using JsMiracle.WebUI.CommonSupport;
using JsMiracle.Entities.TabelEntities;
using System.Text;
using Newtonsoft.Json;

namespace JsMiracle.WebUI.Areas.CB.Controllers
{
    public class LocationController : BaseController
    {
        ILocation dalLocation;
        ILocationType dalLocationType;
        ILocationRelationship dalLocationRelationship;
        IContainer dalContainer;
        

        public LocationController(ILocation repoLocation
            , ILocationType repoLocationType
            , ILocationRelationship repoLocationRelationship
            , IContainer repoContainer)
        {
            this.dalLocation = repoLocation;
            this.dalLocationType = repoLocationType;
            this.dalLocationRelationship = repoLocationRelationship;
            this.dalContainer = repoContainer;
        }

        #region IMS_CB_WZ
        //
        // GET: /CB/Location/
        [AuthViewPage]
        public ActionResult IndexLocation()
        {
            return View();
        }

        /// <summary>
        /// 得到储位信息的javascript代码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLocationJS()
        {
            var frozenColumns = new DataGridColumn[1];
            frozenColumns[0] = new DataGridColumn()
            {
                field = "floor",
                title = "层号",
                width = "40px",
                align = "center"
            };

            var frozenColumnsString =  JsonConvert.SerializeObject(frozenColumns);

            var columns = new DataGridColumn[dalLocation.MaxL];
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < dalLocation.MaxL; i++)
            {
                columns[i] = new DataGridColumn()
                {
                    field = (i + 1).ToString(),
                    title = (i + 1).ToString(),
                    width = "40px",
                    align = "center",
                    styler= "cblocation.grdStyle",
                    formatter = "cblocation.grdFormatter",
                    //onDblClickCell = "cblocation.grdOnDblClickCell"
                };
                sb.Append(columns[i].ToString() + ',');
            }

            var colStr = '[' + sb.ToString().TrimEnd(',') + ']';

            string getLocationListUrl = Url.Action("GetLocationList");
            StringBuilder scriptBuilder = new StringBuilder();

            scriptBuilder.AppendFormat(
              @" $('#grdLocation').datagrid({{
                    url: '{0}',
                    singleSelect: true,
                    loadMsg: '数据载入中...',
                    height: '700px',
                    toolbar: '#locationToolbar',
                    frozenColumns: [{1}],
                    columns: [{2}] ,
                    onDblClickCell:cblocation.grdOnDblClickCell
                }});  ", getLocationListUrl, frozenColumnsString, colStr
              );

            return JavaScript(scriptBuilder.ToString());
        }

        /// <summary>
        /// 得到储位信息
        /// </summary>
        /// <param name="p">第几排,默认第1排</param>
        /// <returns></returns>
        public ActionResult GetLocationList(int p=1)
        {
            var dt = dalLocation.GetLocationState(p);

            var info = new PaginationModel(dt);
            return this.JsonFormat(info);
        }

        [AuthViewPage]
        public ActionResult EditLocation(string wxbh)
        {
            var ent = dalLocation.GetEntity(wxbh);
            if (ent == null)
                throw new JsMiracleException(string.Format("不存在位置'{0}',无法修改 ", wxbh));

            return View(ent);
        }

        //[AuthViewPage]
        //public ActionResult ShowLocation(string wxbh)
        //{
        //    var ent = dalContainer.GetAllEntites(
        //        n => n.DQWZ.Equals(wxbh, StringComparison.CurrentCultureIgnoreCase))
        //        .FirstOrDefault();



        //    return View(ent);
        //}

        public ActionResult SaveLocation(IMS_CB_WZ entity)
        {
            Func<ExtResult> saveFun = () =>
            {
                if (!string.IsNullOrEmpty(entity.WXBH))
                {
                    if (dalLocation.Exists(f =>
                        f.WXBH.Equals(entity.WXBH, StringComparison.CurrentCultureIgnoreCase)
                        && f.ID != entity.ID))
                        throw new JsMiracleException("位置编号不得重覆");
                }

                if (entity.ID == 0)
                {
                    entity.CJR = CurrentUser.GetCurrentUser().UserInfo.YHID;
                    entity.CJSJ = System.DateTime.Now;
                }

                dalLocation.SaveOrUpdate(entity);
                var ret = new ExtResult();
                ret.success = true;
                return ret;
            };

            return base.Save(saveFun);
        }

        /// <summary>
        /// 初始化位置信息
        /// </summary>
        /// <returns></returns>
        public ActionResult InitLocation()
        {
            return View();
        }


        #endregion

        #region IMS_CB_WZLX

        [AuthViewPage]
        public ActionResult IndexLocationType()
        {
            return View();
        }

        public ActionResult GetLocationTypeList(int? rows, int? page)
        {
            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            //Expression<Func<IMS_CB_WZLX, bool>> filter = null;
            //var dataList = dalLocationType.GetDataByPage(
            //    n => n.LXDM, filter, pageIndex, pageSize, out totalCount);

            var dataList = dalLocationType.GetDataByPage(pageIndex, pageSize, out totalCount, "LXDM", "");

            //数据组装到viewModel
            var info = new PaginationModel(dataList);
            return this.JsonFormat(info, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveLocationType(string id)
        {
            // id ==> lxdm
            Func<ExtResult> removeFun = () =>
                {
                    dalLocationType.Delete(id);
                    var ret = new ExtResult();
                    ret.success = true;
                    return ret;
                };

            return base.Remove(removeFun);
        }

        [AuthViewPage]
        public ActionResult EditLocationType(string lxdm)
        {
            var ent = dalLocationType.GetEntity(lxdm);
            if (ent == null)
                throw new JsMiracleException(string.Format("不存在位置类型'{0}',无法修改 ", lxdm));

            return View(ent);
        }



        public ActionResult CreateLocationType()
        {
            return View("EditLocationType", new IMS_CB_WZLX());
        }


        public ActionResult SaveLocationType(IMS_CB_WZLX entity)
        {
            //if (entity.ID == 0)
            //    entity.CJR = CurrentUser.GetCurrentUser().UserInfo.YHID;

            Func<ExtResult> saveFun = () =>
            {
                if (!string.IsNullOrEmpty(entity.LXDM))
                {
                    if (dalLocationType.Exists(f =>
                        f.LXDM.Equals(entity.LXDM, StringComparison.CurrentCultureIgnoreCase)
                        && f.ID != entity.ID))
                        throw new JsMiracleException("位置类型编号不得重覆");
                }

                //entity.CJR = CurrentUser.GetCurrentUser().UserInfo.YHID;
                dalLocationType.SaveOrUpdate(entity);
                var ret = new ExtResult();
                ret.success = true;
                return ret;
            };

            return base.Save(saveFun);
        }

        #endregion

        #region IMS_CB_WZGX
        [AuthViewPage]
        public ActionResult IndexLocationRelationship()
        {
            return View();
        }

        public ActionResult GetLocationRelationshipList(int? rows, int? page)
        {
            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            var dataList = dalLocationRelationship.GetDataByPage(pageSize, pageIndex, out totalCount, " LX ", "");

            //数据组装到viewModel
            var info = new PaginationModel(dataList);
            return this.JsonFormat(info, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateRelationship()
        {
            return View("EditRelationship", new IMS_CB_WZGX());
        }

        [AuthViewPage]
        public ActionResult EditRelationship(long id)
        {
            var ent = dalLocationRelationship.GetEntity(id);
            return View(ent);
        }

        public ActionResult RemoveRelationship(long id)
        {
            Func<ExtResult> removeFun = () =>
            {
                dalLocationRelationship.Delete(id);
                var ret = new ExtResult();
                ret.success = true;
                return ret;
            };

            return base.Remove(removeFun);
        }

        public ActionResult SaveRelationship(IMS_CB_WZGX entity)
        {
            Func<ExtResult> saveFun = () =>
            {

                dalLocationRelationship.SaveOrUpdate(entity);
                var ret = new ExtResult();
                ret.success = true;
                return ret;
            };

            return base.Save(saveFun);
        }
        #endregion

    }
}
