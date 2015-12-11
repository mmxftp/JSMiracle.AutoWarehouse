using JsMiracle.Dal.Abstract.CM;
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
            return this.JsonFormat(data);
        }

        /// <summary>
        /// 得数据库中实际存在的数据
        /// </summary>
        /// <param name="tablename">表名称</param>
        /// <returns></returns>
        public ActionResult GetTableInfo(string tablename)
        {
            var code = dalCode.GetAllEntites(n => n.MC.Equals(tablename)
               && n.LXDM == CodeTypeEnum.TableName.ToString()).FirstOrDefault();

            var dm = "";
            if (code != null)
                dm = code.DM;

            var dataList =
                dalObjectData.GetAllEntites(
                n => n.DXDM.Equals(dm, StringComparison.CurrentCultureIgnoreCase));

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
            return View(ent);
        }


    }
}
