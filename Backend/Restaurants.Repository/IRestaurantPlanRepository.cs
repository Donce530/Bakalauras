using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models.Restaurants.Models.Data;
using Repository;

namespace Restaurants.Repository
{
    public interface IRestaurantPlanRepository : IRepositoryBase<RestaurantPlan>
    {
        public Task UpdateTableLinks(ICollection<TableLink> links, ICollection<int> planTableIds);
        public Task<ICollection<TableLink>> GetTableLinks(Expression<Func<TableLink, bool>> filter);
    }
}