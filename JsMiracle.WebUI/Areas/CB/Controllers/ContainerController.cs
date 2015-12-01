using JsMiracle.Dal.Abstract.CB;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JsMiracle.Framework;
using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.WebUI.Controllers.Filter;
using System.Text;
using JsMiracle.WebUI.CommonSupport;
using JsMiracle.WebUI.Controllers;

namespace JsMiracle.WebUI.Areas.CB.Controllers
{
    /// <summary>
    /// 容器处理
    /// </summary>
    public class ContainerController : BaseController
    {
        IContainer dalContainer;
        IContainerType dalContainerType;

        public ContainerController(IContainerType repoContainType, IContainer repoContainer)
        {
            dalContainer = repoContainer;
            dalContainerType = repoContainType;
        }


        #region IMS_CB_RQLX 
        [AuthViewPage]
        public ActionResult IndexContainerType()
        {
            return View();
        }

        public ActionResult GetContainerTypeList(int? rows, int? page)
        {
            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            var dataList = dalContainerType.GetDataByPage(pageIndex, pageSize, out totalCount
                , " LXBH ", null);

            //数据组装到viewModel
            var info = new PaginationModel(dataList);

            return this.JsonFormat(info, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveContainerType(long id)
        {
            try
            {
                dalContainerType.Delete(id);
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


        public ActionResult EditContainerType(long id)
        {
            var ent = dalContainerType.GetEntity(id);
            if (ent == null)
                throw new JsMiracleException(string.Format("不存在容器类型'{0}',无法修改 ", id));

            return View(ent);
        }



        public ActionResult CreateContainerType()
        {
            return View("EditContainerType", new IMS_CB_RQLX());
        }



        public ActionResult SaveContainerType(IMS_CB_RQLX entity)
        {
            Func<ExtResult> saveFun = () =>
            {
                if (entity.ID == 0)
                {
                    if (dalContainerType.Exists(f => f.LXBH.Equals(entity.LXBH, StringComparison.CurrentCultureIgnoreCase)))
                        throw new JsMiracleException("容器类型编号不得重覆");

                }
                else
                {
                    if (dalContainerType.Exists(f => f.LXBH.Equals(entity.LXBH, StringComparison.CurrentCultureIgnoreCase)
                        && f.ID != entity.ID))
                        throw new JsMiracleException("容器类型编号不得重覆");
                }

                // 创建人
                entity.CJR = CurrentUser.GetCurrentUser().UserInfo.YHID;

                dalContainerType.SaveOrUpdate(entity);
                var ret = new ExtResult();
                ret.success = true;
                return ret;
            };

            return base.Save(saveFun);
        }

        #endregion 

        #region IMS_CB_RQ
        [AuthViewPage]
        public ActionResult IndexContainer()
        {
            return View();
        }

        public ActionResult GetContainerList(int? rows, int? page)
        {
            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            var dataList = dalContainer.GetDataByPage(pageIndex, pageSize, out totalCount
                , " rq.RQBH ", null);

            //数据组装到viewModel
            var info = new PaginationModel(dataList);
            return this.JsonFormat(info, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveContainer(long id)
        {
            try
            {
                dalContainer.Delete(id);
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

        [AuthViewPage]
        public ActionResult EditContainer(long id)
        {
            var ent = dalContainer.GetEntity(id);
            if (ent == null)
                throw new JsMiracleException(string.Format("不存在容器类型'{0}',无法修改 ", id));

            ViewBag.ContainerType = new SelectList(
                GetContainerTypeList(), "ID", "LXMC", ent.RQLXID);


            return View(ent);
        }



        public ActionResult CreateContainer()
        {
            ViewBag.ContainerType = new SelectList(
                GetContainerTypeList(), "ID", "LXMC", -1);

            return View("EditContainer", new IMS_CB_RQ());
        }

        private IList<IMS_CB_RQLX> GetContainerTypeList()
        {
            var data = dalContainerType.GetAllEntites(null);
            data.Insert(0, new IMS_CB_RQLX() { ID = -1, LXMC = "--请选择--" });

            return data;
        }



        public ActionResult SaveContainer(IMS_CB_RQ entity)
        {
            Func<ExtResult> saveFun = () =>
            {
                if (entity.ID == 0)
                {
                    if (dalContainer.Exists(f => f.RQBH.Equals(entity.RQBH, StringComparison.CurrentCultureIgnoreCase)))
                        throw new JsMiracleException("容器类型编号不得重覆");

                }
                else
                {
                    if (dalContainer.Exists(f => f.RQBH.Equals(entity.RQBH, StringComparison.CurrentCultureIgnoreCase)
                        && f.ID != entity.ID))
                        throw new JsMiracleException("容器类型编号不得重覆");
                }
                entity.ZCSJ = System.DateTime.Now;
                dalContainer.SaveOrUpdate(entity);
                var ret = new ExtResult();
                ret.success = true;
                return ret;
            };

            return base.Save(saveFun);
        }


        #endregion

    }
}
