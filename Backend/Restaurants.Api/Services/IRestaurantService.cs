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
    }
}