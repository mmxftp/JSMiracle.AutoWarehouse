using JsMiracle.Entities;
using JsMiracle.Entities.Enum;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.WCF;
using JsMiracle.Framework;
using JsMiracle.WCF.CM.ICommonMng;
using JsMiracle.WCF.Interface;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Text;

namespace JsMiracle.WCF.CM.CommonMng
{
    /// <summary>
    /// 代码
    /// </summary>
    public class IMS_CM_Code_Dal : WcfDataLayerBase<IMS_CM_DM>, ICode
    {
        protected override bool IsAddEntity(IMS_CM_DM entity)
        {
            if (entity.ID == 0)
            {
                if (base.DbContext.IMS_CM_DM_S.Any(n => n.LXDM == entity.LXDM
                    && n.DM == entity.DM))
                    throw new JsMiracleException("代码编号重覆不得修改");
            }
            else
            {
                if (base.DbContext.IMS_CM_DM_S.Any(n => n.ID != entity.ID
                    && n.SZ == entity.SZ
                    && n.DM == entity.DM))
                    throw new JsMiracleException("代码编号重覆不得修改");
            }

            return base.IsAddEntity(entity);
        }


        public List<IMS_CM_DM> GetCodeList(CodeTypeEnum codeType)
        {
            if (codeType == CodeTypeEnum.None)
                throw new JsMiracleException("codeType不得为空");

            var codeTypeStr = codeType.ToString();

            //Expression<Func<IMS_CM_DM, bool>> filter
            //    = f => f.LXDM.Equals(codeTypeStr, StringComparison.CurrentCultureIgnoreCase);

            return GetAllEntitesByFilter(string.Format(" LXDM == \"{0}\" ", codeTypeStr));

            //return this.GetAllEntites(filter);
        }




        public IMS_CM_DM GetCode(CodeTypeEnum codeType, int sz)
        {
            if (codeType == CodeTypeEnum.None)
                throw new JsMiracleException("codeType不得为空");

            var filter = " LXDM == @0 and sz == @1 ";

            return base.DbContext.IMS_CM_DM_S.Where(filter, codeType.ToString(), sz).FirstOrDefault();
        }
    }

    public class IMS_CM_Code_WCF : WcfDataServiceBase<IMS_CM_DM>, IWcfCode
    {
        IMS_CM_Code_Dal dal = new IMS_CM_Code_Dal();

        protected override IDataLayer<IMS_CM_DM> DataLayer
        {
            get { return dal; }
        }

        protected override WcfResponse RequestFun(WcfRequest req)
        {
            WcfResponse res = new WcfResponse();

            //List<IMS_CM_DM> dataList;
            object[] objs;

            switch (req.Head.RequestMethodName)
            {
                case "GetCodeList":
                    objs = (object[])req.Body.Parameters;
                    res.Body.Data = dal.GetCodeList((CodeTypeEnum)objs[0]);
                    break;

                case "GetCode":
                    objs = (object[])req.Body.Parameters;
                    res.Body.Data = dal.GetCode((CodeTypeEnum)objs[0], (int)objs[1]);
                    break;

                default:
                    return null;
            }

            res.Head.IsSuccess = true;
            return res;
        }

    }
}
