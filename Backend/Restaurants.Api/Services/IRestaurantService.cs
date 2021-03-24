using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurants.Models.Dto;

namespace Restaurants.Api.Services
{
    public interface IRestaurantService
    {
        Task<RestaurantDto> GetDetails();
        Task SaveDetails(RestaurantDto restaurant);
        Task<RestaurantPlanDto> GetPlan();
        Task SavePlan(RestaurantPlanDto plan);
        Task<string> GetPlanSvg();
        Task<IList<string>> GetRestaurantCities();
        Task<IList<RestaurantPageItemDto>> GetPage(int page, string city, string filter);
        Task<RestaurantDto> GetDetails(int id);
        Task<RestaurantPlanDto> GetPlan(int id);
    }
}