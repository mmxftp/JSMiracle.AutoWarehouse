using JsMiracle.Entities.WCF;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.WcfBaseService
{
    /// <summary>
    /// 所有wcf实现处理基类
    /// </summary>
    public abstract class WcfServiceBase
    {
        protected abstract WcfResponse BaseRequest(WcfRequest req);

        /// <summary>
        /// 所有外部请求的接收方法
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual WcfResponse Request(WcfRequest req)
        {
            WcfResponse res = null;
            try
            {
                var resFun = BaseRequest(req);
                if (resFun != null)
                    return resFun;

                throw new JsMiracleException(
                    string.Format("调用的方法不存在 {0}", req.Head.RequestMethodName));
            }
            catch (DbEntityValidationException dbEx)
            {
                if (res == null)
                    res = new WcfResponse();

                StringBuilder sb = new StringBuilder();
                if (dbEx.EntityValidationErrors != null)
                {
                    foreach (var err in dbEx.EntityValidationErrors)
                    {
                        foreach (var valerr in err.ValidationErrors)
                            sb.AppendFormat("{0}:{1}", valerr.PropertyName, valerr.ErrorMessage);
                        //valerr.PropertyName , valerr.
                    }
                    res.Head.Message = sb.ToString();
                }
                else
                {
                    res.Head.Message = dbEx.Message;
                }
                res.Head.IsSuccess = false;
            }
            catch (Exception ex)
            {
                if (res == null)
                    res = new WcfResponse();

                if (ex is JsMiracle.Framework.JsMiracleException)
                    res.Head.Message = ex.Message;
                else
                {

                    Exception innerExp = ex;

                    while (innerExp.InnerException != null)
                    {
                        innerExp = innerExp.InnerException;
                    }
                    res.Head.Message = string.Format("{0}-{1}", ex.Message, innerExp.Message);
                }

                res.Head.IsSuccess = false;
            }
            return res;
        }
    }

}
