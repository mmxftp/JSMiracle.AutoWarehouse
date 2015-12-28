using JsMiracle.Entities;
using JsMiracle.Entities.Enum;
using JsMiracle.Entities.WCF;
using JsMiracle.Framework;
using JsMiracle.WCF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace JsMiracle.WCF.BaseWcfClient
{
    public abstract class WcfClientConfig<T, Channel> : WcfClient<Channel>, IDataLayer<T> 
        where T : class
        where Channel : class,IWcfService
    {
        protected WcfClientConfig(EndpointAddress edpAddr)
            : base(wcfBinding, edpAddr) { }

        protected WcfClientConfig(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress) { }


        public WcfResponse Request(WcfRequest req)
        {
            return base.Channel.Request(req);
        }

        /// <summary>
        /// 执行wcf方法并返回数据
        /// </summary>
        /// <typeparam name="P">参数类型</typeparam>
        /// <typeparam name="Return">返回类型</typeparam>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">参数</param>
        /// <param name="serType"></param>
        /// <returns></returns>
        protected virtual Return RequestFunc<P, Return>(string methodName, P parameters)
        {
            try
            {
                WcfRequest req = new WcfRequest();
                req.Head.RequestMethodName = methodName;
                req.Head.ClassName = this.GetType().Name;
                if (parameters != null)
                    req.Body.Parameters = parameters;

                var res = base.Channel.Request(req);
                if (res.Head.IsSuccess)
                    return (Return)res.Body.Data;

                // 返回不是真 抛出异常
                throw new JsMiracleException(res.Head.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default(Return);
            }
        }



        /// <summary>
        /// 执行wcf方法没有返回数据
        /// </summary>
        /// <typeparam name="P">参数类型</typeparam>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        protected virtual void RequestAction<P>(string methodName, P parameters)
        {
            WcfRequest req = new WcfRequest();
            req.Head.RequestMethodName = methodName;
            req.Body.Parameters = parameters;

            var res = base.Channel.Request(req);
            if (res.Head.IsSuccess)
                return;

            // 返回不是真 抛出异常
            throw new JsMiracleException(res.Head.Message);
        }


        public T GetEntity(object id)
        {
            return this.RequestFunc<object[], T>("GetEntity", new object[] { id });
        }

        public List<T> GetAllEntites(string filter)
        {
            return this.RequestFunc<object[], List<T>>("GetAllEntites", new object[] { filter });
        }

        public bool Exists(string filter)
        {
            return this.RequestFunc<object[], bool>("Exists", new object[] { filter });
        }

        public void SaveOrUpdate(T entity)
        {
            this.RequestAction<T>("SaveOrUpdate", entity);
        }

        public void Delete(object id)
        {
            this.RequestAction<object[]>("Delete", new object[] { id });
        }

        public void Insert(T entity)
        {
            this.RequestAction<T>("Insert", entity);
        }

        public List<T> GetDataByPageDynamic(int intPageIndex, int intPageSize, out int rowCount, string orderBy, string where, params object[] whereParams)
        {
            object[] obj = new object[] { 
                intPageIndex,
                intPageSize,
                orderBy,
                where,
                whereParams
            };

            var data = this.RequestFunc<object[], object[]>("GetDataByPageDynamic", obj);

            rowCount = (int)data[0];
            return (List<T>)data[1];

        }
    }
}
