using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;


namespace JsMiracle.Dal.Abstract
{
    //public abstract class DataLayerBase<T> : IDataLayer<T> where T : class,new()
    public class DataLayerBase : IIMS_ORGEntities
    {
        //protected IIMS_ORGEntities context;
        //protected ObjectContext objContext;

        //internal DataLayerBase()
        //{
        //    context = new IIMS_ORGEntities();

        //    //this.context = context;
        //    objContext = ((IObjectContextAdapter)context).ObjectContext;
        //}
        //public ObjectContext ObjContext { get { return objContext; } }

        //public IIMS_ORGEntities Context { get { return context; } }

        //public DbSet<T> GetTable
        //{
        //    get { return context.Set<T>(); }
        //}
        //public T Update1(T entity)
        //{
        //    if (this.Entry(entity).State == EntityState.Modified)
        //        this.SaveChanges();

        //    return entity;
        //}


        public static T Update<T>(DbContext context, T entity) where T : class,new()
        {
            if (context.Entry(entity).State == EntityState.Modified)
                context.SaveChanges();

            return entity;
        }

        public static T Insert<T>(DbContext context, T entity) where T : class,new()
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
            return entity;
        }

        public static int Delete<T>(DbContext context,T entity) where T : class,new()
        {
            context.Set<T>().Remove(entity);
            return context.SaveChanges();                
        }

        public static T Find<T>(DbContext context, params object[] keyValues)where T:class ,new ()
        {
            return context.Set<T>().Find(keyValues);
        }

        public static IList<T> FindWhere<T>(DbContext context, Expression<Func<T, bool>> filter) where T :class, new ()
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
        public static IList<T> GetDataByPage<T, TKey>(
            DbContext context,
            Expression<Func<T, TKey>> orderBy
            , Expression<Func<T, bool>> filter
            , int intPageIndex
            , int intPageSize
            , out int rowCount) where T : class,new()
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
