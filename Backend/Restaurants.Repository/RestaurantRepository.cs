using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurants.Models.Data;
using Repository;

namespace Restaurants.Repository
{
    public class RestaurantRepository : RepositoryBase<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        //public async Task Update(Restaurant restaurant)
        //{
        //    var existingRestaurant = await DbContext.Restaurants
        //        .Where(r => r.UserId == restaurant.UserId)
        //        .Include(r => r.Schedule)
        //        .SingleOrDefaultAsync();

        //    if (existingRestaurant is null)
        //    {
        //        throw new InvalidOperationException("Cannot update non-existing entities");
        //    }

        //    Mapper.Map(restaurant, existingRestaurant);

        //    await DbContext.SaveChangesAsync();
        //}
    }
}
