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

namespace JsMiracle.WebUI.Areas.CB.Controllers
{
    public class LocationController : BaseController
    {
        ILocation dalLocation;
        ILocationType dalLocationType;
        ILocationRelationship dalLocationRelationship;

        public LocationController(ILocation repoLocation
            , ILocationType repoLocationType
            , ILocationRelationship repoLocationRelationship)
        {
            this.dalLocation = repoLocation;
            this.dalLocationType = repoLocationType;
            this.dalLocationRelationship = repoLocationRelationship;
        }

        #region IMS_CB_WZ
        //
        // GET: /CB/Location/
        [AuthViewPage]
        public ActionResult IndexLocation()
        {
            var getLocationList = Url.Action("GetLocationList");
            var editLocation = Url.Action("EditLocation");

            var script = @"
                
    var cbLocation;

    $(function () {

        // 初始化所有用到的方法
        cbLocation = {
            
            refreshDataGrid: function () {
                $('#grdLocation').datagrid('reload');
            },

            showEdit: function (op, id) {
                var width = '650px';
                var height = '480px';
                var title = '';
                var url = '';

                if (op == 'mod') {
                    url = '{0}' + '?wxbh=' + id;
                    title = '编辑位置信息';
                }
                JsMiracleCommon.showWindow(title, url, width, height, this.refreshDataGrid);

            },
        };


        $('#grdLocation').datagrid({
            url: '{1}',
            singleSelect: true,
            loadMsg: '数据载入中...',
            pagination: true, // 是否显示分页控件
            pagesize: 10,      // 每页行数
            pageList: [10],    // 要选的每页行数 为了界面显示正确一定要和pagesize的值相同
            height: '700px',
            rownumbers: true,
            toolbar: '#locationToolbar',
            frozenColumns: [[
                {
                    field: '_operate', title: '操作', width: 150, align: 'center'
                    , formatter: function (value, row, index) {
                        var html = '<a href=""javascript:void(0)""  class=""loan_edit_button"" '
                                  + ' onclick=""cbLocation.showEdit(\'mod\',\'' + row.WXBH + '\');"">修改</a>';

                        return html;
                    }
                },
                { field: 'WXBH', title: '位置编号', width: 100, align: 'center' },
                { field: 'WZMC', title: '位置名称', width: 100, align: 'center' }
            ]],
            columns: [[

                { field: 'XSBQ', title: '显示标签', width: 100, align: 'center' },
                { field: 'WZLX', title: '位置类型', width: 100, align: 'center' },
                { field: 'CKID', title: '仓库ID', width: 150, align: 'center' },
                { field: 'QY', title: '区域', width: 100, align: 'center' },
                { field: 'XD', title: '巷道', width: 100, align: 'center' },
                { field: 'P', title: '排', width: 100, align: 'center' },
                { field: 'L', title: '列', width: 100, align: 'center' },
                { field: 'C', title: '层', width: 100, align: 'center' },
                { field: 'SD', title: '深度', width: 100, align: 'center' },
                { field: 'WZ', title: '位置', width: 100, align: 'center' },
                { field: 'ABC', title: 'ABC', width: 100, align: 'center' },
                { field: 'WLZT', title: '物理状态', width: 100, align: 'center' },
                { field: 'FWZT', title: '访问状态', width: 100, align: 'center' },
                { field: 'ZYZT', title: '占用状态', width: 100, align: 'center' },
                { field: 'SDZT', title: '锁定状态', width: 100, align: 'center' },
                { field: 'YDZT', title: '预定状态', width: 100, align: 'center' },
                { field: 'JMLX', title: '界面类型', width: 100, align: 'center' },
                { field: 'GNLX', title: '功能类型', width: 100, align: 'center' },
            ]],
            onLoadSuccess: function (data) {
                $("".loan_edit_button"").linkbutton({ iconCls: 'icon-edit' });
            }
        });
    });
";


            return JavaScript(script);
        }

        public ActionResult GetLocationList(int? rows, int? page)
        {
            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            //Expression<Func<IMS_CB_WZ, bool>> filter = null;
            //System.Linq.Dynamic.DynamicExpression.ParseLambda()

            //var dataList = dalLocation.GetDataByPage(
            //    o=> o.P  ,
            //    filter,
            //    pageIndex, pageSize, out totalCount);

            var dataList = dalLocation.GetDataByPage(pageIndex, pageSize, out totalCount
                , " P, L, C ", null);

            //数据组装到viewModel
            //var info = new PaginationModel<IMS_CB_WZ>();

            var info = new PaginationModel(dataList);
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
