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
    public class RestaurantsRepository : RepositoryBase<Restaurant>, IRestaurantRepository
    {
        private readonly IMapper _mapper;
        public RestaurantsRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _mapper = mapper;
        }

        public override async Task Update(Restaurant entity, Expression<Func<Restaurant, bool>> filter)
        {
            var existingItem = await DbContext.Restaurants
                .Where(filter)
                .Include(r => r.Schedule).SingleOrDefaultAsync();

            if (existingItem is null)
            {
                throw new InvalidOperationException("Cannot update not existing restaurant");
            }

            _mapper.Map(entity, existingItem);

            await DbContext.SaveChangesAsync();
        }
    }
}
