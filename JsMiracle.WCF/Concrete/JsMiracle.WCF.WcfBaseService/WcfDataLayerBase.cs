using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Data.Entity;
using JsMiracle.Framework;

namespace JsMiracle.WCF.WcfBaseService
{

    public abstract class WcfDataLayerBase<T> : IDataLayer<T> where T : class ,new()
    {
        private ORGModels _dbContext;

        protected virtual ORGModels DbContext
        {
            get
            {
                if (_dbContext == null)
                    _dbContext = new ORGModels();

                return _dbContext;
            }
        }

        ///// <summary>
        ///// 是否使用属性copy功能有返回结果集,
        ///// 有外键需显示的数据不得带有集合,只能用copy功能去除集合属性,进行返回
        ///// </summary>
        //private bool UserCopyMemberProperty = false;

        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="copyMemerProperty">是否使用属性copy功能有返回结果集,有外键需显示的数据不得带有集合,只能用copy功能去除集合属性,进行返回</param>
        //protected WcfDataLayerBase(bool copyMemerProperty = false)
        //{
        //    UserCopyMemberProperty = copyMemerProperty;
        //}


        public virtual T GetEntity(object id)
        {

            return FindEntityByKey(id);
        }


        public virtual void SaveOrUpdate(T entity)
        {
            SaveEntity(entity);
        }

        #region protected method

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity">数据实体</param>
        protected virtual void SaveEntity(T entity)
        {
            //if (entity.ID == 0)
            if (IsAddEntity(entity))
            {
                Insert(entity);
            }
            else
            {
                //var oldEnt = GetEntity(entity.ID);
                var oldEnt = GetOldEntity(entity);
                if (oldEnt == null)
                    throw new JsMiracleException(
                        string.Format("对象({0})不存在无法修改 id:{1}", typeof(T).Name, GetKeyValue(entity)));

                ModuleMemberCopy.SameValueCopier(entity, oldEnt);

                //DbContext.Entry(entity).State = EntityState.Modified;

                if (DbContext.Entry(oldEnt).State == EntityState.Modified)
                    DbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 按主键查询数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected virtual T FindEntityByKey(object id)
        {
            return DbContext.Set<T>().Find(id);
        }
        /// <summary>
        /// 判断是否为新增实例
        /// </summary>
        /// <param name="entity">实例信息</param>
        /// <returns></returns>
        protected virtual bool IsAddEntity(T entity)
        {
            dynamic data = entity;


            return data.ID == 0;
        }

        /// <summary>
        /// 得到修改前的实例
        /// </summary>
        /// <param name="entity">实例信息</param>
        /// <returns></returns>
        protected virtual T GetOldEntity(T entity)
        {
            dynamic data = entity;
            return FindEntityByKey(data.ID);
        }

        protected virtual string GetKeyValue(T entity)
        {
            dynamic data = entity;
            return data.ID;
        }

        protected virtual void DeleteByKey(object id)
        {
            var entity = FindEntityByKey(id);
            if (entity != null)
            {
                DbContext.Set<T>().Remove(entity);
                DbContext.SaveChanges();
            }
        }

        protected virtual List<T> GetAllEntitesByFilter(string filter)
        {
            //返回按条件过滤的记录
            if (!string.IsNullOrEmpty(filter))
                return DbContext.Set<T>().Where(filter).ToList();

            //条件为空返回全部
            return DbContext.Set<T>().ToList();
        }
        #endregion


        public virtual void Delete(object id)
        {
            DeleteByKey(id);
        }

        public virtual void Insert(T entity)
        {
            DbContext.Set<T>().Add(entity);
            DbContext.SaveChanges();
        }



        public virtual List<T> GetAllEntites(string filter)
        {
            return GetAllEntitesByFilter(filter);
        }


        //public virtual bool Exists(Expression<Func<T, bool>> filter)
        public virtual bool Exists(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                var data = DbContext.Set<T>().Where(filter).FirstOrDefault();

                if (data == null)
                    return false;

                return true;
            }

            return DbContext.Set<T>().Any();
        }



        //public virtual IList<T> GetDataByPage<TKey>(Expression<Func<T, TKey>> orderBy
        //    , Expression<Func<T, bool>> filter
        //    , int intPageIndex
        //    , int intPageSize
        //    , out int rowCount)
        //{
        //    IQueryable<T> query = null;

        //    if (filter != null)
        //    {
        //        query = DbContext.Set<T>()
        //            .Where(filter)
        //            .OrderBy(orderBy)
        //            .Skip((intPageIndex - 1) * intPageSize)
        //            .Take(intPageSize);

        //        rowCount = DbContext.Set<T>().Where(filter).Count();
        //    }
        //    else
        //    {
        //        query = DbContext.Set<T>()
        //            .OrderBy(orderBy)
        //            .Skip((intPageIndex - 1) * intPageSize)
        //            .Take(intPageSize);

        //        rowCount = DbContext.Set<T>().Count();
        //    }
        //    var selectQuery = GetPageQuery(query);
        //    //var result = query.ToList();

        //    return selectQuery;
        //}

        public virtual List<T> GetDataByPageDynamic(int intPageIndex
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
                    .Where(where, whereParams)
                    .OrderBy(orderBy)
                    .Skip((intPageIndex - 1) * intPageSize)
                    .Take(intPageSize);

                rowCount = DbContext.Set<T>().Where(where, whereParams).Count();
            }
            else
            {
                query = DbContext.Set<T>()
                    .OrderBy(orderBy)
                    .Skip((intPageIndex - 1) * intPageSize)
                    .Take(intPageSize);

                rowCount = DbContext.Set<T>().Count();
            }
            //var selectQuery = GetPageQuery(query);
            var selectQuery = query.ToList();
            //var result = selectQuery.ToList();

            return selectQuery;
        }

        //protected virtual IList<T> GetPageQuery(IQueryable<T> query)
        //{
        //    // 不使用成员copy (快)
        //    if (!UserCopyMemberProperty)
        //        return query.ToList();

        //    // 使用成员copy (反射,慢, 去除集合属性,防止外键序列化显示时的列循环)
        //    List<T> lx = new List<T>();
        //    foreach (var data in query)
        //    {
        //        T ent = new T();
        //        ModuleMemberCopy.SameValueCopier(data, ent, false);
        //        lx.Add(ent);
        //    }
        //    return lx;
        //}

        //protected virtual 



    }
}