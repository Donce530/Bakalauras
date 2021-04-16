using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Models.Reservations.Models.Dto;

namespace Repository
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, new()
    {
        protected readonly AppDbContext DbContext;
        protected readonly IMapper Mapper;

        protected RepositoryBase(AppDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, bool tracking = false)
        { 
            IQueryable<TEntity> query = DbContext.Set<TEntity>();

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            if (tracking is false)
            {
                query = query.AsNoTracking();
            }

            return await query.SingleOrDefaultAsync();
        }

        public async Task<IList<TResult>> GetAll<TResult>(Expression<Func<TEntity, TResult>> select, Expression<Func<TEntity, bool>> filter = null, bool distinct = false)
        {
            var query = DbContext.Set<TEntity>().AsQueryable();

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            var selectedQuery = query.Select(select);
            if (distinct)
            {
                selectedQuery = selectedQuery.Distinct();
            }

            return await selectedQuery.ToListAsync();
        }
        
        public async Task<IList<TResult>> GetAll<TResult>(Expression<Func<TEntity, bool>> filter = null, bool distinct = false)
        {
            var query = DbContext.Set<TEntity>().AsQueryable();

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            var selectedQuery = query.ProjectTo<TResult>(Mapper.ConfigurationProvider);
            if (distinct)
            {
                selectedQuery = selectedQuery.Distinct();
            }

            return await selectedQuery.ToListAsync();
        }

        public async Task<PagedResponse<TResult>> GetPaged<TResult>(Paginator paginator,
            IEnumerable<Expression<Func<TEntity, bool>>> filters = null,
            Expression<Func<TEntity, object>> orderBy = null)
        {
            if (paginator is null)
            {
                throw new InvalidOperationException();
            }

            var query = DbContext.Set<TEntity>().AsQueryable();

            if (filters is not null)
            {
                query = filters.Aggregate(query, (current, filter) => current.Where(filter));
            }

            if (orderBy is not null && paginator.SortOrder != 0)
            {
                query = paginator.SortOrder > 0 ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }

            var results = await query.ProjectTo<TResult>(Mapper.ConfigurationProvider)
                .Skip(paginator.Offset * paginator.Rows).Take(paginator.Rows).ToListAsync();
            paginator.TotalRows = results.Count > 0 ? await query.CountAsync() : 0;

            var response = new PagedResponse<TResult> {Paginator = paginator, Results = results};

            return response;
        }

        public async Task<TResult> GetMapped<TResult>(Expression<Func<TEntity, bool>> filter)
        {
            var query = DbContext.Set<TEntity>().Where(filter);

            return await query.ProjectTo<TResult>(Mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<TResult> GetMapped<TResult>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TResult>> select)
        {
            var query = DbContext.Set<TEntity>().Where(filter).Select(select);

            return await query.SingleOrDefaultAsync();
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            return await DbContext.Set<TEntity>().Where(filter).AnyAsync();
        }

        public async Task Create(TEntity entity)
        {
            await DbContext.Set<TEntity>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }
        
        public async Task<int> Delete(Expression<Func<TEntity, bool>> filter)
        {
            var itemsToRemove = await DbContext.Set<TEntity>().Where(filter).ToListAsync();

            DbContext.RemoveRange(itemsToRemove);
            await DbContext.SaveChangesAsync();

            return itemsToRemove.Count;
        }

        public abstract Task Update(TEntity entity, Expression<Func<TEntity, bool>> filter);
    }
}