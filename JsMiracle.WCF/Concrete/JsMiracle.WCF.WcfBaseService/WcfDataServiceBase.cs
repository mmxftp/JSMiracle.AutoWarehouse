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
    /// <summary>
    /// 所有单表数据处理wcf处理基类
    /// </summary>
    /// <typeparam name="T">entityframework 实例</typeparam>
    public abstract class WcfDataServiceBase<T> : WcfServiceBase where T : class ,new()
    {

        protected abstract WcfResponse RequestFun(WcfRequest req);

        protected abstract IDataLayer<T> DataLayer { get; }

        protected override WcfResponse BaseRequest(WcfRequest req)
        {
            var res = RequestFun(req);
            if (res != null)
                return res;

            res = new WcfResponse();

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
                    return null;
            }

            res.Head.IsSuccess = true;

            return res;
        }
    }
}
