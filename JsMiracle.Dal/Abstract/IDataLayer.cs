using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JsMiracle.Dal.Abstract
{
    public interface IDataLayer<T> where T : class, new()
    {
        /// <summary>
        /// 得到数据处理层类
        /// </summary>
        ObjectContext ObjContext { get; }

        /// <summary>
        /// 得到数据处理类
        /// </summary>
        IIMS_ORGEntities Context { get; }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Update(T entity);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Insert(T entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// 按主键查询数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        T Find(params object[] keyValues);

        /// <summary>
        /// 根据条件得数据
        /// </summary>
        /// <param name="filter">查询条件 ,filter == null 返回的所有记录</param>
        /// <returns></returns>
        IList<T> FindWhere(Expression<Func<T, bool>> filter);


        /// <summary>
        /// 分页方法
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="orderBy">排序条件</param>
        /// <param name="filter">查询条件</param>
        /// <param name="intPageIndex">第几页</param>
        /// <param name="intPageSize">每页行数</param>
        /// <param name="rowCount">总行数 (输出参数)</param>
        /// <returns></returns>
        IList<T> GetDataByPage<TKey>(
            Expression<Func<T, TKey>> orderBy, Expression<Func<T, bool>> filter, int intPageIndex,
            int intPageSize, out int rowCount);
    }
}
