using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models.Restaurants.Models.Data;
using Repository;

namespace Restaurants.Repository
{
    public class RestaurantPlansRepository: RepositoryBase<RestaurantPlan>, IRestaurantPlanRepository
    {
        private readonly IMapper _mapper;
        public RestaurantPlansRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _mapper = mapper;
        }

        public override async Task Update(RestaurantPlan entity, Expression<Func<RestaurantPlan, bool>> filter)
        {
            var existingItem = await DbContext.RestaurantPlans
                .Where(filter)
                .Include(rp => rp.Tables)
                .Include(rp => rp.Walls)
                .SingleOrDefaultAsync();

            if (existingItem is null)
            {
                throw new InvalidOperationException("Cannot update not existing restaurant");
            }

            DbContext.Entry(existingItem).CurrentValues.SetValues(entity);
            DbContext.Entry(existingItem).Property(e => e.RestaurantId).IsModified = false;

            foreach (var existingItemTable in existingItem.Tables)
            {
                if (entity.Tables.All(t => t.Id != existingItemTable.Id))
                {
                    DbContext.PlanTables.Remove(existingItemTable);
                }
            }

            foreach (var entityTable in entity.Tables)
            {
                entityTable.PlanId = entity.Id;
                var existingChild = existingItem.Tables
                    .SingleOrDefault(c => c.Id == entityTable.Id && c.Id != 0);

                if (existingChild is not null)
                {
                    DbContext.Entry(existingChild).CurrentValues.SetValues(entityTable);
                }
                else
                {
                    existingItem.Tables.Add(entityTable);
                }
            }

            foreach (var existingItemWall in existingItem.Walls)
            {
                if (entity.Walls.All(t => t.Id != existingItemWall.Id))
                {
                    DbContext.PlanWalls.Remove(existingItemWall);
                }
            }

            foreach (var entityWall in entity.Walls)
            {
                entityWall.PlanId = entity.Id;
                var existingChild = existingItem.Walls
                    .SingleOrDefault(c => c.Id == entityWall.Id && c.Id != 0);

                if (existingChild is not null)
                {
                    DbContext.Entry(existingChild).CurrentValues.SetValues(entityWall);
                }
                else
                {
                    existingItem.Walls.Add(entityWall);
                }
            }

            await DbContext.SaveChangesAsync();
        }
    }
}