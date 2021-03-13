using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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

        public abstract Task Update(TEntity entity, Expression<Func<TEntity, bool>> filter);
    }
}