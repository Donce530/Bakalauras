using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models.Reservations.Models.Dto;

namespace Repository
{
    public interface IRepositoryBase<T>
    {
        Task<T> Get(Expression<Func<T, bool>> filter = null, bool tracking = false);
        Task<TResult> GetMapped<TResult>(Expression<Func<T, bool>> filter);
        Task<TResult> GetMapped<TResult>(Expression<Func<T, bool>> filter,
            Expression<Func<T, TResult>> select);
        Task<IList<TResult>> GetAll<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> filter = null, bool distinct = false);
        Task<IList<TResult>> GetAll<TResult>(Expression<Func<T, bool>> filter = null, bool distinct = false);
        Task<PagedResponse<TResult>> GetPaged<TResult>(Paginator paginator,
            IEnumerable<Expression<Func<T, bool>>> filters = null, Expression<Func<T, object>> orderBy = null);
        Task<bool> Exists(Expression<Func<T, bool>> filter);
        Task Create(T entity);
        Task Update(T entity, Expression<Func<T, bool>> filter);
        Task<int> Delete(Expression<Func<T, bool>> filter);
    }
}