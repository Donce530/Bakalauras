using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Api.Services;
using Restaurants.Models.Dto;
using Users.Api.Attributes;

namespace Restaurants.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("Details")]
        public async Task<IActionResult> GetDetails()
        {
            var restaurant = await _restaurantService.GetDetails();

            if (restaurant is null)
            {
                return NoContent();
            }

            return Ok(restaurant);
        }

        [HttpPost("Details")]
        public async Task<IActionResult> SaveDetails([FromBody] RestaurantDto restaurant)
        {
            await _restaurantService.SaveDetails(restaurant);

            return Ok();
        }
    }
}
