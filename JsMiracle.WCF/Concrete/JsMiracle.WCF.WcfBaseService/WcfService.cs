using JsMiracle.Entities;
using JsMiracle.Entities.WCF;
using JsMiracle.Framework;
using JsMiracle.WCF.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsMiracle.WCF.WcfBaseService
{

    public abstract class WcfService<T> where T : class ,new()
    {

        protected abstract WcfResponse RequestFun(WcfRequest req);

        protected abstract IDataLayer<T> DataLayer { get; }


        public virtual WcfResponse Request(WcfRequest req)
        {
            WcfResponse res = new WcfResponse();

            try
            {
                var resFun = RequestFun(req);
                if (resFun != null)
                    return resFun;

                T ent;
                List<T> dataList;
                object[] objs;

                switch (req.Head.RequestMethodName)
                {
                    case "GetEntity":
                        objs = (object[])req.Body.Parameters;
                        res.Body.Data = DataLayer.GetEntity(objs[0]);
                        break;

                    case "GetAllEntites":
                        objs = (object[])req.Body.Parameters;
                        res.Body.Data = DataLayer.GetAllEntites((string)objs[0]);
                        break;

                    case "Exists":
                        objs = (object[])req.Body.Parameters;
                        res.Body.Data = DataLayer.Exists((string)objs[0]);
                        break;

                    case "SaveOrUpdate":
                        ent = (T)req.Body.Parameters;
                        ent = DataLayer.SaveOrUpdate(ent);
                        res.Body.Data = ent;
                        break;

                    case "Delete":
                        objs = (object[])req.Body.Parameters;
                        DataLayer.Delete(objs[0]);
                        break;

                    case "Insert":
                        ent = (T)req.Body.Parameters;
                        ent = DataLayer.Insert(ent);
                        res.Body.Data = ent;
                        break;

                    case "GetDataByPageDynamic":
                        //splitPar = (SplitPageParameters)req.Body.Parameters;
                        objs = (object[])req.Body.Parameters;
                        int rowCount;

                        //var data = new SplitPageData<T>();
                        dataList =
                            DataLayer.GetDataByPageDynamic(
                                (int)objs[0]        // PageIndex
                                , (int)objs[1]      // PageSize
                                , out rowCount
                                , (string)objs[2]   // OrderBy
                                , (string)objs[3]   // Where
                                , (object[])objs[4]); //WhereParams

                        res.Body.Data = new object[] { rowCount, dataList };
                        break;

                    default:
                        throw new JsMiracleException(
                            string.Format("调用的方法不存在 {0}", req.Head.RequestMethodName));

                }

                res.Head.IsSuccess = true;

            }
            catch (DbEntityValidationException dbEx)
            {
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
