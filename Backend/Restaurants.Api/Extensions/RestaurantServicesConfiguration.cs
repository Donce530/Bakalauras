using Microsoft.Extensions.DependencyInjection;
using Restaurants.Api.Services;
using Restaurants.Repository;

namespace Restaurants.Api.Extensions
{
    public static class RestaurantServicesConfiguration
    {
        public static void AddRestaurantServiceConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IRestaurantService, RestaurantService>();
            services.AddTransient<IRestaurantRepository, RestaurantsRepository>();
            services.AddTransient<IRestaurantPlanRepository, RestaurantPlansRepository>();
        }
    }
}