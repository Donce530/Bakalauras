using System;
using System.Collections.Generic;
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
                .Include(rp => rp.Labels)
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

            foreach (var incomingTable in entity.Tables)
            {
                incomingTable.PlanId = entity.Id;
                var existingTable = existingItem.Tables
                    .SingleOrDefault(c => c.Id == incomingTable.Id && c.Id != 0);

                if (existingTable is not null)
                {
                    DbContext.Entry(existingTable).CurrentValues.SetValues(incomingTable);
                }
                else
                {
                    existingItem.Tables.Add(incomingTable);
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
            
            foreach (var existingItemLabel in existingItem.Labels)
            {
                if (entity.Labels.All(t => t.Id != existingItemLabel.Id))
                {
                    DbContext.PlanLabels.Remove(existingItemLabel);
                }
            }

            foreach (var entityLabel in entity.Labels)
            {
                entityLabel.PlanId = entity.Id;
                var existingChild = existingItem.Labels
                    .SingleOrDefault(c => c.Id == entityLabel.Id && c.Id != 0);

                if (existingChild is not null)
                {
                    DbContext.Entry(existingChild).CurrentValues.SetValues(entityLabel);
                }
                else
                {
                    existingItem.Labels.Add(entityLabel);
                }
            }

            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateTableLinks(ICollection<TableLink> links, ICollection<int> planTableIds)
        {
            var existingLinks = await DbContext.TableLinks.Where(l =>
                planTableIds.Contains(l.FirstTableId) || planTableIds.Contains(l.SecondTableId)).ToListAsync();

            var linksToRemove = existingLinks.Except(links);
            DbContext.TableLinks.RemoveRange(linksToRemove);

            var newLinks = links.Except(existingLinks);
            await DbContext.TableLinks.AddRangeAsync(newLinks);

            await DbContext.SaveChangesAsync();
        }

        public async Task<ICollection<TableLink>> GetTableLinks(Expression<Func<TableLink, bool>> filter)
        {
            var links = await DbContext.TableLinks.Where(filter).ToListAsync();

            return links;
        }
    }
}