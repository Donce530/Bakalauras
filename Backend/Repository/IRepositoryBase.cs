using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepositoryBase<T>
    {
        Task<T> Get(Expression<Func<T, bool>> filter = null, bool tracking = false);
        Task<TResult> GetMapped<TResult>(Expression<Func<T, bool>> filter = null);
        Task<bool> Exists(Expression<Func<T, bool>> filter);
        Task Create(T entity);
        Task Update(T restaurant, Expression<Func<T, bool>> filter,
            IList<Expression<Func<T, object>>> relatedDataExpressions = null);
    }
}