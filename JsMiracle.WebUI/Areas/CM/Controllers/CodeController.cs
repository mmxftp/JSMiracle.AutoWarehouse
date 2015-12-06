﻿using JsMiracle.Dal.Abstract.CM;
using System;
using System.Linq;
using System.Web.Mvc;
using JsMiracle.Framework;
using JsMiracle.WebUI.Controllers.Filter;
using System.Linq.Expressions;
using JsMiracle.Entities;
using JsMiracle.WebUI.Models;
using JsMiracle.Entities.EasyUI_Model;
using System.Text;
using JsMiracle.WebUI.Controllers;
using JsMiracle.WebUI.CommonSupport;
using JsMiracle.Entities.TabelEntities;

namespace JsMiracle.WebUI.Areas.CM.Controllers
{
    public class CodeController : BaseController
    {
        ICode dalCode;
        ICodeType dalCodeType;

        public CodeController(ICode repoCode, ICodeType repoCodeType)
        {
            this.dalCode = repoCode;
            this.dalCodeType = repoCodeType;
        }

        //
        // GET: /Code/
        [AuthViewPage]
        public ActionResult Index()
        {
            return View();
        }

        #region IMS_CM_DM_S  操作

        public ActionResult GetCode(string lxdm, int sz)
        {
            var data = dalCode.GetAllEntites(n => n.LXDM == lxdm && n.SZ == sz);

            var ent = data.FirstOrDefault();

            if (ent != null)
            {
                return this.JsonFormat(new { success = true, data = ent }, JsonRequestBehavior.AllowGet);
            }

            return this.JsonFormat(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="page"></param>
        /// <param name="codeType">类型代码</param>
        /// <returns></returns>
        public ActionResult GetCodeList(int? rows, int? page, string codeType)
        {
            int totalCount = 0;

            int pageIndex = page ?? 1;
            int pageSize = rows ?? 10;

            Expression<Func<IMS_CM_DM, bool>> filter = null;

            if (!string.IsNullOrEmpty(codeType))
            {
                filter =
                    f => (f.LXDM.Equals(codeType, StringComparison.CurrentCultureIgnoreCase));
            }

            var dataList = dalCode.GetDataByPage(
                o => o.MC,
                filter,
                pageIndex, pageSize, out totalCount);

            //数据组装到viewModel
            var info = new PaginationModel(dataList);
            //var info = new PaginationModel();
            //info.SetRows(dataList);
            //info.total = totalCount;
            //info.rows = dataList;

            return this.JsonFormat(info);
        }

        /// <summary>
        /// 创建代码配置
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateCode(long codeTypeID)
        {
            var ent = dalCodeType.GetEntity(codeTypeID);
            if (ent == null)
                throw new JsMiracleException("代码大类不存在");

            ViewBag.LXMC = string.Format("{0}({1})", ent.LXMC, ent.LXDM);

            var data = new IMS_CM_DM();
            data.LXDM = ent.LXDM;

            return View("EditCode", data);
        }

        /// <summary>
        /// 修改代码配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditCode(long id)
        {
            var data = dalCode.GetEntity(id);

            if (data == null)
                throw new JsMiracleException("要修改的代码配置不存在,请刷新后重试");

            var ent = dalCodeType.GetEntity(data.LXDM);
            if (ent == null)
                throw new JsMiracleException("代码大类不存在");

            ViewBag.LXMC = string.Format("{0}({1})", ent.LXMC, ent.LXDM);


            return View(data);
        }

        public ActionResult RemoveCode(long id)
        {
            try
            {
                dalCode.Delete(id);
                return this.JsonFormat(new ExtResult { success = true, msg = "删除成功" });
            }
            catch (Exception ex)
            {
                return this.JsonFormat(new ExtResult { success = false, msg = "删除失败" + ex.Message });
            }
        }

        public ActionResult SaveCode(IMS_CM_DM entity)
        {
            Func<ExtResult> saveFun = () => 
            {
                if (entity.ID == 0)
                {
                    if (dalCode.Exists(n => n.LXDM.Equals(entity.LXDM, StringComparison.CurrentCultureIgnoreCase)
                        && (n.DM.Equals(entity.DM) || n.SZ == entity.SZ)))
                        throw new JsMiracleException(string.Format("类型'{0}'已存在代码'{1}',数值'{2}'"
                            , entity.LXDM, entity.DM, entity.SZ));
                }
                else
                {
                    if (dalCode.Exists(n => n.LXDM.Equals(entity.LXDM, StringComparison.CurrentCultureIgnoreCase)
                        && (n.DM.Equals(entity.DM) || n.SZ == entity.SZ)
                        && n.ID != entity.ID))
                        throw new JsMiracleException(string.Format("类型'{0}'已存在代码'{1}',数值'{2}'"
                            , entity.LXDM, entity.DM, entity.SZ));
                }

                //entity.CJR = CurrentUser.GetCurrentUser().UserInfo.YHID;
                dalCode.SaveOrUpdate(entity);
                ExtResult ret = new ExtResult();
                ret.success = true;
                ret.msg = "保存成功";

                var codeTypeEnt = dalCodeType.GetEntity(entity.LXDM);
                if (codeTypeEnt != null)
                    ret.parentid = codeTypeEnt.LXDM;

                return ret;
            };

            return base.Save(saveFun);
        }

        #endregion

        #region IMS_CM_DMLX_S 操作
        public ActionResult GetAllCodeType()
        {
            var data = dalCodeType.GetAllEntites(null);
            return this.JsonFormat(data);
        }

        public ActionResult EditCodeType(int id)
        {
            var ent = dalCodeType.GetEntity(id);

            return View(ent);
        }

        public ActionResult CreateCodeType()
        {
            var data = new IMS_CM_DMLX();
            return View("EditCodeType", data);
        }

        public ActionResult SaveCodeType(IMS_CM_DMLX ent)
        {
            Func<ExtResult> saveFun = () =>
            {

                // 是否为新增
                if (ent.ID == 0)
                {
                    if (dalCodeType.Exists(f => f.LXDM.Equals(ent.LXDM, StringComparison.CurrentCultureIgnoreCase)))
                        throw new JsMiracleException("代码编号不得重覆");
                }
                else
                {
                    if (dalCodeType.Exists(f => f.LXDM.Equals(ent.LXDM, StringComparison.CurrentCultureIgnoreCase)
                        && f.ID != ent.ID))
                        throw new JsMiracleException("代码编号不得重覆");
                }

                ent.CJSJ = System.DateTime.Now;
                //ent.CJR = CurrentUser.GetCurrentUser().UserInfo.YHID;

                dalCodeType.SaveOrUpdate(ent);

                ExtResult ret = new ExtResult();
                ret.success = true;
                ret.msg = "保存成功";
                ret.parentid = ent.ID;

                return ret;
            };

            return base.Save(saveFun);

        }


        public ActionResult RemoveCodeType(long id)
        {
            try
            {
                var ent = dalCodeType.GetEntity(id);
                if (dalCode.Exists(n => n.LXDM.Equals(ent.LXDM, StringComparison.CurrentCultureIgnoreCase)))
                    throw new JsMiracleException(string.Format("请选删除代码类型('{0}')下的代码配置", ent.LXMC));

                dalCodeType.Delete(id);

                return this.JsonFormat(new ExtResult { success = true });
            }
            catch (Exception ex)
            {
                return this.JsonFormat(new ExtResult { success = false, msg = ex.Message });
            }
        }
        #endregion
    }
}
