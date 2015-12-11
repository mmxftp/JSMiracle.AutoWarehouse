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
        /// <param name="reload">是否重新从数据库的系统表载入</param>
        /// <returns></returns>
        public ActionResult GetTableInfo(string tablename, bool reload=false)
        {
            try
            {

                // 重新载入,删除原有旧数据
                if (reload)
                {
                    var ent = dalCode.GetAllEntites(n => n.MC.Equals(tablename)
                        && n.LXDM == CodeTypeEnum.TableName.ToString()).FirstOrDefault();

                    if (ent == null)
                        throw new JsMiracleException("对应表名不存在无法处理");

                    dalObjectData.DeleteObjectData(ent.DM);


                    //var tableId = ent.DM;

                    //if (dalUserObj.Exists(n => n.DXDM == tableId))
                    //    throw new JsMiracleException("对应信息已被用户表(IMS_CM_YHDX_S)使用中不得删除");


                    //ent.DM 
                    //dalUserObj.Exists(n=>n.DXDM == )

                }

                var columnsList = dalObjectData.GetColumns(tablename);

                //dalObjectData.GetAllEntites(n=>n.DXDM == )



                return this.JsonFormat(columnsList);
            }
            catch
            {
                return View();
            }
        }

        [AuthViewPage]
        public ActionResult EditObjectData(long id)
        {
            var ent = dalObjectData.GetEntity(id);
            return View(ent);
        }


    }
}
