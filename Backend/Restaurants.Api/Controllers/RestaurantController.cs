using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Restaurants.Models.Dto;
using Restaurants.Api.Services;
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

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> GetDetailsById([FromRoute] int id)
        {
            var restaurant = await _restaurantService.GetDetails(id);

            if (restaurant is null)
            {
                return NoContent();
            }

            return Ok(restaurant);
        }

        [HttpPost("Details")]
        public async Task<IActionResult> SaveDetails([FromBody] RestaurantDto restaurant)
        {
            if (restaurant is null)
            {
                return BadRequest();
            }
            
            await _restaurantService.SaveDetails(restaurant);

            return Ok();
        }

        [HttpGet("QR")]
        public async Task<IActionResult> GetQrCode()
        {
            var qr = await _restaurantService.GetQrCode();

            var outputStream = new MemoryStream();
            qr.Save(outputStream, ImageFormat.Png);
            outputStream.Seek(0, SeekOrigin.Begin);

            return File(outputStream, "image/png");
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
            if (page < 0 || string.IsNullOrWhiteSpace(city))
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

        [HttpGet("Plan/{restaurantId}")]
        public async Task<IActionResult> GetPlan([FromRoute] int restaurantId)
        {
            var plan = await _restaurantService.GetPlan(restaurantId);

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
            if (plan is null)
            {
                return BadRequest();
            }

            await _restaurantService.SavePlan(plan);

            return Ok();
        }
    }
}
