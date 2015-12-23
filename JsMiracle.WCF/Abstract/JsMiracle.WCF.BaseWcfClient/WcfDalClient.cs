using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace JsMiracle.WCF.BaseWcfClient
{
    public abstract class WcfDalClient<T> : WcfClient<IDataLayer<T>>, IDataLayer<T>, IDisposable where T : class
    {
        protected WcfDalClient(EndpointAddress edpAddr)
            : base(wcfBinding, edpAddr) { }

        protected WcfDalClient(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress) { }

        public T GetEntity(object id)
        {
            return base.Channel.GetEntity(id);
        }

        public List<T> GetAllEntites(string filter)
        {
            return base.Channel.GetAllEntites(filter);
        }

        public bool Exists(string filter)
        {
            return base.Channel.Exists(filter);
        }

        public void SaveOrUpdate(T entity)
        {
            base.Channel.SaveOrUpdate(entity);
        }

        public void Delete(object id)
        {
            base.Channel.Delete(id);
        }

        public void Insert(T entity)
        {
            base.Channel.Insert(entity);
        }

        //public IList<T> GetDataByPage<TKey>(System.Linq.Expressions.Expression<Func<T, TKey>> orderBy
        //    , System.Linq.Expressions.Expression<Func<T, bool>> filter
        //    , int intPageIndex, int intPageSize, out int rowCount)
        //{
        //    return base.Channel.GetDataByPage(orderBy,filter,intPageIndex,intPageSize,out rowCount);
        //}

        public List<T> GetDataByPageDynamic(int intPageIndex, int intPageSize, out int rowCount, string orderBy, string where, params object[] whereParams)
        {
            return base.Channel.GetDataByPageDynamic(intPageIndex, intPageSize, out rowCount, orderBy, where, whereParams);
        }
    }



}
