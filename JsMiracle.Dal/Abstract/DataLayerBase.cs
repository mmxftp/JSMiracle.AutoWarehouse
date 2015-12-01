using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;

public abstract class DataLayerBase<T> : IDataLayer<T> where T : class, IModelBase
{
    private IIMS_ORGEntities _dbContext;

    protected virtual IIMS_ORGEntities DbContext
    {
        get
        {
            if (_dbContext == null)
                _dbContext = new IIMS_ORGEntities();

            return _dbContext;
        }
    }

    public virtual T GetEntity(long id)
    {
        return DbContext.Set<T>().Find(id);
    }

    public virtual void SaveOrUpdate(T entity)
    {
        if (entity.ID == 0)
        {
            Insert(entity);
        }
        else
        {
            var oldEnt = GetEntity(entity.ID);
            if (oldEnt == null)
                throw new JsMiracleException(
                    string.Format("对象({0})不存在无法修改 id:{1}", typeof(T).Name, entity.ID));

            ModuleMemberCopy.SameValueCopier(entity, oldEnt);

            //DbContext.Entry(entity).State = EntityState.Modified;

            if (DbContext.Entry(oldEnt).State == EntityState.Modified)
                DbContext.SaveChanges();
        }
    }

    public virtual void Delete(long id)
    {
        var entity = GetEntity(id);
        if (entity != null)
        {
            DbContext.Set<T>().Remove(entity);
            DbContext.SaveChanges();
        }
    }

    public virtual void Insert(T entity)
    {
        DbContext.Set<T>().Add(entity);
        DbContext.SaveChanges();
    }



    public virtual IList<T> GetAllEntites(Expression<Func<T, bool>> filter)
    {
        // 返回按条件过滤的记录
        if (filter != null)
            return DbContext.Set<T>().Where(filter).ToList();

        // 条件为空返回全部
        return DbContext.Set<T>().ToList();
    }


    public virtual bool Exists(Expression<Func<T, bool>> filter)
    {
        if (filter != null)
        {
            return DbContext.Set<T>().Any(filter);
        }

        return DbContext.Set<T>().Any();
    }

    public virtual IList<T> GetDataByPage<TKey>(Expression<Func<T, TKey>> orderBy
        , Expression<Func<T, bool>> filter
        , int intPageIndex
        , int intPageSize
        , out int rowCount)
    {
        IQueryable<T> query = null;

        if (filter != null)
        {
            query = DbContext.Set<T>()
                .Where(filter)
                .OrderBy(orderBy)
                .Skip((intPageIndex - 1) * intPageSize)
                .Take(intPageSize);

            rowCount = DbContext.Set<T>().Where(filter).Count();
        }
        else
        {
            query = DbContext.Set<T>()
                .OrderBy(orderBy)
                .Skip((intPageIndex - 1) * intPageSize)
                .Take(intPageSize);

            rowCount = DbContext.Set<T>().Count();
        }

        var result = query.ToList();

        return result;
    }

    public virtual dynamic GetDataByPage(int intPageIndex
        , int intPageSize
        , out int rowCount
        , string orderBy
        , string where
        , params object[] whereParams)
    {
        IQueryable<T> query = null;

        if (!string.IsNullOrEmpty(where))
        {
            query = DbContext.Set<T>()
                .Where(where,whereParams)
                .OrderBy(orderBy)
                .Skip((intPageIndex - 1) * intPageSize)
                .Take(intPageSize);

            rowCount = DbContext.Set<T>().Where(where,whereParams).Count();
        }
        else
        {
            query = DbContext.Set<T>()
                .OrderBy(orderBy)
                .Skip((intPageIndex - 1) * intPageSize)
                .Take(intPageSize);

            rowCount = DbContext.Set<T>().Count();
        }

        var result = query.ToList();

        return result;
    }
}