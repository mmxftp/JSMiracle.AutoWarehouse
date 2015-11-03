using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;


namespace JsMiracle.Dal.Abstract
{
    public abstract class DataLayerBase<T> : IDataLayer<T> where T : class,new()
    {
        protected IIMS_ORGEntities context;

        internal DataLayerBase(IIMS_ORGEntities context)
        {
            this.context = context;
        }


        public virtual T Update(T entity)
        {
            if (context.Entry<T>(entity).State == EntityState.Modified)
                context.SaveChanges();


            return entity;
        }

        public virtual T Insert(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
            return entity;
        }

        public virtual void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public virtual T Find(params object[] keyValues)
        {
            return context.Set<T>().Find(keyValues);

        }

        public virtual IList<T> FindWhere(Expression<Func<T, bool>> filter)
        {
            // 条件为空返回所有记录
            if (filter == null)
                return context.Set<T>().ToList();

            // 条件不为空，按条件返回数据
            return context.Set<T>().Where(filter).ToList();
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="orderBy">排序条件</param>
        /// <param name="filter">where条件</param>
        /// <param name="intPageIndex">第几页</param>
        /// <param name="intPageSize">每页行数</param>
        /// <param name="rowCount">总计行数</param>
        /// <returns></returns>
        public virtual IList<T> GetDataByPage<TKey>(
            Expression<Func<T, TKey>> orderBy
            , Expression<Func<T, bool>> filter
            , int intPageIndex
            , int intPageSize
            , out int rowCount)
        {
            IQueryable<T> query = null;

            if (filter != null)
            {
                query = context.Set<T>()
                    .Where(filter)
                    .OrderBy(orderBy)
                    .Skip((intPageIndex - 1) * intPageSize)
                    .Take(intPageSize);

                rowCount = context.Set<T>().Where(filter).Count();
            }
            else
            {
                query = context.Set<T>()
                    .OrderBy(orderBy)
                    .Skip((intPageIndex - 1) * intPageSize)
                    .Take(intPageSize);

                rowCount = context.Set<T>().Count();
            }

            var result = query.ToList();

            return result;


        }
    }
}
