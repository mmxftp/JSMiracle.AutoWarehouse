using JsMiracle.Entities;
using JsMiracle.Entities.WCF;
using JsMiracle.Framework;
using JsMiracle.WCF.Interface;
using System;
using System.Collections.Generic;
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
                        ;
                        break;

                    case "Exists":
                        objs = (object[])req.Body.Parameters;
                        res.Body.Data = DataLayer.Exists((string)objs[0]);

                        break;

                    case "SaveOrUpdate":
                        ent = (T)req.Body.Parameters;
                        DataLayer.SaveOrUpdate(ent);
                        break;

                    case "Delete":
                        objs = (object[])req.Body.Parameters;
                        DataLayer.Delete(objs[0]);
                        break;

                    case "Insert":
                        ent = (T)req.Body.Parameters;
                        DataLayer.Insert(ent);
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


                        //dataList =
                        //DataLayer.GetDataByPageDynamic(    
                        //splitPar.PageIndex
                        //, splitPar.PageSize
                        //, out rowCount
                        //, splitPar.OrderBy
                        //, splitPar.Where
                        //, splitPar.WhereParams);
                        //data.TotalRow = rowCount;

                        res.Body.Data = new object[] { rowCount, dataList };
                        break;

                    default:
                        throw new JsMiracleException(
                            string.Format("调用的方法不存在 {0}", req.Head.RequestMethodName));

                }

                res.Head.IsSuccess = true;

            }
            catch (Exception ex)
            {
                if (ex is JsMiracle.Framework.JsMiracleException)
                    res.Head.Message = ex.Message;
                else
                    res.Head.Message = ex.Message + '|' + ex.StackTrace;

                res.Head.IsSuccess = false;
            }
            return res;
        }
    }
}
