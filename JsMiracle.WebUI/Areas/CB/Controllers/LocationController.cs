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

namespace JsMiracle.WebUI.Areas.CB.Controllers
{
    public class LocationController : BaseController
    {
        ILocation dalLocation;

        public LocationController(ILocation repoLocation)
        {
            this.dalLocation = repoLocation;
        }

        //
        // GET: /CB/Location/
        [AuthViewPage]
        public ActionResult Index()
        {
            return View();
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

    }
}
