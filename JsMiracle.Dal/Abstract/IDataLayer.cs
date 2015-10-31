using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JsMiracle.Dal.Abstract
{
    public interface IDataLayer<T> where T : class, new()
    {
        T Create();
        T Update(T entity);
        T Insert(T entity);
        void Delete(T entity);
        T Find(params object[] keyValues);
        //List<T> FindAll();                
        IList<T> GetDataByPage<TKey>(
            Expression<Func<T, TKey>> orderBy, Expression<Func<T, bool>> filter, int intPageIndex,
            int intPageSize, out int rowCount);
    }
}
