using JsMiracle.Framework.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JsMiracle.WebAPI.Areas.Receiving.Controllers
{
    public class ProductlineController : Controller
    {
        ICache cache;

        public ProductlineController(ICache repoCache)
        {
            cache = repoCache;
        }

        //
        // GET: /Receiving/Productline/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 批次开始
        /// </summary>
        /// <param name="productionPlanNo">生产计划编号</param>
        /// <param name="lineNumber">生产线编号</param>
        /// <param name="productNo">品规</param>
        /// <param name="batchNo">批次号</param>
        /// <param name="owner">所有者(货主)</param>
        /// <param name="planQty">计划箱数</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BatchStar(string productionPlanNo,
            string lineNumber,
            string productNo,
            string batchNo,
            string owner,
            int planQty)
        {

            ContentResult ret = new ContentResult();
            ret.Content = "0";

            return ret;
        }

        /// <summary>
        /// 批次结束
        /// </summary>
        /// <param name="productionPlanNo">生产计划编号</param>
        /// <param name="lineNumber">生产线编号</param>
        /// <param name="completeQty">实际完成码盘箱数</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BatchEnd(
            string productionPlanNo
            , string lineNumber
            , string completeQty)
        {
            ContentResult ret = new ContentResult();
            ret.Content = "0";

            return ret;
        }

        /// <summary>
        /// 托盘码垛完成
        /// </summary>
        /// <param name="taskNo">作业序号(每个托盘码垛完成作业的唯一序号)</param>
        /// <param name="lineNumber">生产线编号</param>
        /// <param name="palletNo">托盘编号</param>
        /// <param name="startCartonNo">起始箱号</param>
        /// <param name="endCartonNo">结束箱号</param>
        /// <param name="completeQty">托盘完成箱数</param>
        /// <param name="productionTime">产品生产时间编码yyyyMMddHHmmss 例:20151126093001</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PalletBinding(string taskNo,
            string lineNumber,
            string palletNo,
            string startCartonNo,
            string endCartonNo,
            string completeQty,
            string productionTime
        )
        {
            ContentResult ret = new ContentResult();
            ret.Content = "0";

            return ret;
        }

    }
}
