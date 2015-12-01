using JsMiracle.Entities;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JsMiracle.Dal.Abstract
{
    public interface IDataLayer<T> where T : IModelBase
    {
        /// <summary>
        /// 按主键得记录
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        T GetEntity(long id);

        /// <summary>
        /// 得数据表中所有数据
        /// </summary>
        /// <param name="filter">过滤条件 == null 返回所有记录</param>
        /// <returns></returns>
        IList<T> GetAllEntites(Expression<Func<T, bool>> filter);

        /// <summary>
        /// 是否存在指定数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool Exists(Expression<Func<T, bool>> filter);

        /// <summary>
        /// 保存或新增记录
        /// </summary>
        /// <param name="entity"></param>
        void SaveOrUpdate(T entity);

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="id">主键</param>
        void Delete(long id);

        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="entity">数据实体</param>
        void Insert(T entity);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey">数据表的实体类</typeparam>
        /// <param name="orderBy">排序条件</param>
        /// <param name="filter">过滤条件h</param>
        /// <param name="intPageIndex">第几页</param>
        /// <param name="intPageSize">每页行数</param>
        /// <param name="rowCount">共多少行 (输出参数)</param>
        /// <returns></returns>
        IList<T> GetDataByPage<TKey>(
            Expression<Func<T, TKey>> orderBy
            , Expression<Func<T, bool>> filter
            , int intPageIndex
            , int intPageSize
            , out int rowCount);


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="intPageIndex">第几页</param>
        /// <param name="intPageSize">每页行数</param>
        /// <param name="rowCount">共多少行(输出参数)</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="where">过滤条件</param>
        /// <param name="whereParams">where 子句中的参数值数组 </param>
        /// <returns></returns>
        dynamic GetDataByPage(
             int intPageIndex
            , int intPageSize
            , out int rowCount
            , string orderBy
            , string where
            , params object[] whereParams);

    }
}
