using System.Threading.Tasks;
using Restaurants.Models.Dto;

namespace Restaurants.Api.Services
{
    public interface IRestaurantService
    {
        Task<RestaurantDto> GetDetails();
        Task SaveDetails(RestaurantDto restaurant);
    }
}