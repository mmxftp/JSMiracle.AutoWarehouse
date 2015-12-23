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

    public abstract class WcfService<T>  where T : class ,new()
    //public abstract class WcfService<T> : WcfDataLayerBase<T> where T : class ,new()
    {


        protected virtual WcfResponse RequestFun(WcfRequest req)
        {
            return null;
        }

        protected abstract IDataLayer<T> DataLayer  { get; }


        public virtual WcfResponse Request(WcfRequest req)
        {
            WcfResponse res = new WcfResponse();

            try
            {
                var resFun = RequestFun(req);
                if (resFun != null)
                    return resFun;

               
                string filter;
                T ent;
                List<T> dataList;
                object id;
                bool exists;
                SplitPageParameters splitPar;

                switch (req.Head.RequestMethodName)
                {
                    case "GetEntity":
                        id = req.Body.GetParameters<object>();
                        ent = DataLayer.GetEntity(id);
                        res.Body.SetBody(ent);


                        break;

                    case "GetAllEntites":
                        filter = (string)req.Body.GetParameters<object>();
                        dataList = DataLayer.GetAllEntites(filter);
                        res.Body.SetBody(dataList);
                        break;

                    case "Exists":
                        filter = (string)req.Body.GetParameters<object>();
                        exists = DataLayer.Exists(filter);
                        res.Body.SetBody(exists);
                        break;

                    case "SaveOrUpdate":
                        ent = req.Body.GetParameters<T>();
                        DataLayer.SaveOrUpdate(ent);
                        break;

                    case "Delete":
                        id = req.Body.GetParameters<object>();
                        DataLayer.Delete(id);
                        break;

                    case "Insert":
                        ent = req.Body.GetParameters<T>();
                        DataLayer.Insert(ent);
                        break;

                    case "GetDataByPageDynamic":
                        splitPar = req.Body.GetParameters<SplitPageParameters>();
                        int rowCount;

                        var data = new SplitPageData<T>();
                        data.DataList =
                            DataLayer.GetDataByPageDynamic(splitPar.PageIndex
                            , splitPar.PageSize
                            , out rowCount
                            , splitPar.OrderBy
                            , splitPar.Where
                            , splitPar.WhereParams);
                        data.TotalRow = rowCount;
                        res.Body.SetBody(data);
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

        ///// <summary>
        ///// 基类执行有返回信息的方法
        ///// </summary>
        ///// <typeparam name="T">返回的类型</typeparam>
        ///// <param name="function">返回类型的方法</param>
        ///// <returns>wcf返回</returns>
        //protected WcfReturn<T> ProcFunction<T>(Func<T> function) where T : class
        //{
        //    var ret = new WcfReturn<T>();
        //    try
        //    {
        //        ret.Data = function();
        //        ret.Success = true;
        //    }
        //    catch (JsMiracle.Framework.JsMiracleException jsex)
        //    {
        //        ret.Success = false;
        //        ret.Message = jsex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        ret.Success = false;
        //        ret.Message = ex.Message + '|' + ex.StackTrace;
        //    }
        //    return ret;
        //}

        ///// <summary>
        ///// 执行无返回数据的方法
        ///// </summary>
        ///// <param name="action">活动</param>
        ///// <returns>wcf返回</returns>
        //protected WcfReturn ProcAction(Action action)
        //{
        //    var ret = new WcfReturn();
        //    try
        //    {
        //        action();
        //        ret.Success = true;
        //    }
        //    catch (JsMiracle.Framework.JsMiracleException jsex)
        //    {
        //        ret.Success = false;
        //        ret.Message = jsex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        ret.Success = false;
        //        ret.Message = ex.Message + '|' + ex.StackTrace;
        //    }
        //    return ret;
        //}

    }
}
