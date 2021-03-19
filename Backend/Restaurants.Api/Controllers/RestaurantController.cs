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

        [HttpGet("AvailableCities")]
        public async Task<IActionResult> GetAvailableCities()
        {
            var cities = await _restaurantService.GetRestaurantCities();

            return Ok(cities);
        }

        [HttpGet("Page")]
        public async Task<IActionResult> GetPage([FromQuery] int page, [FromQuery] string city, [FromQuery] string filter)
        {
            if (page < 0 || string.IsNullOrEmpty(city))
            {
                return BadRequest(new { message = "page cannot be negative and city must be provided"});
            }

            var restaurants = await _restaurantService.GetPage(page, city, filter);

            return Ok(restaurants);
        }

        [HttpGet("Plan")]
        public async Task<IActionResult> GetPlan()
        {
            var plan = await _restaurantService.GetPlan();

            if (plan is null)
            {
                return NoContent();
            }

            return Ok(plan);
        }

        [HttpGet("Plan/Preview")]
        public async Task<IActionResult> GetPlanSvg()
        {
            var planSvg = await _restaurantService.GetPlanSvg();

            if (planSvg is null)
            {
                return NoContent();
            }

            return Ok(planSvg);
        }

        [HttpPost("Plan")]
        public async Task<IActionResult> SavePlan([FromBody] RestaurantPlanDto plan)
        {
            await _restaurantService.SavePlan(plan);

            return Ok();
        }
    }
}
