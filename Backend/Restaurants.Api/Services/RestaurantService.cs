using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurants.Models.Data;
using Restaurants.Models.Dto;
using Restaurants.Repository;
using Users.Api.Services;

namespace Restaurants.Api.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IUserService _userService;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRestaurantPlanRepository _restaurantPlanRepository;
        private readonly IMapper _mapper;

        public RestaurantService(IUserService userService, IRestaurantRepository restaurantRepository, IMapper mapper, IRestaurantPlanRepository restaurantPlanRepository)
        {
            _userService = userService;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _restaurantPlanRepository = restaurantPlanRepository;
        }

        public async Task<RestaurantDto> GetDetails()
        {
            var restaurant = await _restaurantRepository.GetMapped<RestaurantDto>(r => r.UserId == _userService.User.Id);

            return restaurant;
        }

        public async Task SaveDetails(RestaurantDto restaurant)
        {
            var userId = _userService.User.Id;
            var mappedRestaurant = _mapper.Map<Restaurant>(restaurant);
            mappedRestaurant.UserId = userId;

            if (await _restaurantRepository.Exists(r => r.UserId == userId))
            {
                await _restaurantRepository.Update(mappedRestaurant, r => r.UserId == userId);
            } 
            else
            {
                await _restaurantRepository.Create(mappedRestaurant);
            }
        }

        public Task<RestaurantPlanDto> GetPlan()
        {
            var userId = _userService.User.Id;
            var plan = _restaurantPlanRepository.GetMapped<RestaurantPlanDto>(
                rp => rp.Restaurant.UserId == userId);

            return plan;
        }

        public async Task SavePlan(RestaurantPlanDto plan)
        {
            var userId = _userService.User.Id;
            var mappedPlan = _mapper.Map<RestaurantPlan>(plan);

            if (await _restaurantPlanRepository.Exists(r => r.Restaurant.UserId == userId))
            {
                await _restaurantPlanRepository.Update(mappedPlan, r => r.Restaurant.UserId == userId);
            }
            else
            {
                mappedPlan.RestaurantId = await _restaurantRepository.GetMapped(r => r.UserId == userId, 
                    r => r.Id);
                await _restaurantPlanRepository.Create(mappedPlan);
            }
        }

        public Task<string> GetPlanSvg()
        {
            var userId = _userService.User.Id;
            var planSvg = _restaurantPlanRepository.GetMapped(rp => rp.Restaurant.UserId == userId,
                rp => rp.WebSvg);

            return planSvg;
        }
    }
}