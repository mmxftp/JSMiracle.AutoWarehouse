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
    public abstract class WcfClientConfig<Entity, Channel> : WcfClient<Channel>, IDataLayer<Entity>
        where Entity : class
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
#if DEBUG
            //try
            //{
#endif
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
#if DEBUG
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return default(Return);
            //}
#endif
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


        public Entity GetEntity(object id)
        {
            return this.RequestFunc<object[], Entity>("GetEntity", new object[] { id });
        }

        public List<Entity> GetAllEntites(string filter)
        {
            return this.RequestFunc<object[], List<Entity>>("GetAllEntites", new object[] { filter });
        }

        public bool Exists(string filter)
        {
            return this.RequestFunc<object[], bool>("Exists", new object[] { filter });
        }

        public Entity SaveOrUpdate(Entity entity)
        {
            var ent = this.RequestFunc<Entity, Entity>("SaveOrUpdate", entity);
            CopyID(ent, entity);
            return entity;
        }

        public void Delete(object id)
        {
            this.RequestAction<object[]>("Delete", new object[] { id });
        }

        public Entity Insert(Entity entity)
        {
            var ent = this.RequestFunc<Entity, Entity>("Insert", entity);
            CopyID(ent, entity);
            //ModuleMemberCopy.SameValueCopier(ent, entity);
            return entity;
        }

        private void CopyID(dynamic entSource, dynamic entTarget)
        {
            entTarget.ID = entSource.ID;
            //var sourceProperty = entSource.GetType().GetProperty("ID");
            //var targetProperty = entTarget.GetType().GetProperty("ID");

            //if (sourceProperty != null &&targetProperty != null )
            //{
            //    targetProperty.SetValue(entTarget, sourceProperty.GetValue(entSource, null), null);
            //}
        }

        public List<Entity> GetDataByPageDynamic(int intPageIndex, int intPageSize, out int rowCount, string orderBy, string where, params object[] whereParams)
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
            return (List<Entity>)data[1];

        }
    }
}
