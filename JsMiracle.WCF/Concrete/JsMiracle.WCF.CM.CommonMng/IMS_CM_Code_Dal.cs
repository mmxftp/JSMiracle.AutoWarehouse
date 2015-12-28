
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
using System.Text;

namespace JsMiracle.WCF.CM.CommonMng
{
    public class IMS_CM_Code_Dal : WcfDataLayerBase<IMS_CM_DM>, ICode
    {

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


    }

    public class IMS_CM_Code_WCF : WcfService<IMS_CM_DM>, IWcfCode
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
                    //var enumVal = FunctionHelp.GetEnum<CodeTypeEnum>((long)objs[0]);
                    var enumVal = (CodeTypeEnum)objs[0];
                    res.Body.Data = dal.GetCodeList(enumVal);
                    //Enum.
                    //res.Body.SetBody(dataList);
                    break;

                default:
                    return null;
            }

            res.Head.IsSuccess = true;
            return res;
        }

    }
}
