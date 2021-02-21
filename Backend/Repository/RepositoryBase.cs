using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, new()
    {
        protected readonly AppDbContext DbContext;
        protected readonly IMapper Mapper;

        public RepositoryBase(AppDbContext dbContext, IMapper mapper)
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

        public async Task<TResult> GetMapped<TResult>(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            return await query.ProjectTo<TResult>(Mapper.ConfigurationProvider).SingleOrDefaultAsync();
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

        public async Task Update(TEntity restaurant, Expression<Func<TEntity, bool>> filter,
            IList<Expression<Func<TEntity, object>>> relatedDataExpressions = null)
        {
            var query = DbContext.Set<TEntity>().Where(filter);

            if (relatedDataExpressions is not null && relatedDataExpressions.Any())
            {
                query = relatedDataExpressions.Aggregate(query,
                    (current, relatedDataExpression) => current.Include(relatedDataExpression));
            }

            var existingEntity = await query.SingleOrDefaultAsync();

            if (existingEntity is null)
            {
                throw new InvalidOperationException("Cannot update non-existing entities");
            }

            Mapper.Map(restaurant, existingEntity);

            await DbContext.SaveChangesAsync();
        }
    }
}