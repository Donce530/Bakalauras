using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public RestaurantService(IUserService userService, IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _userService = userService;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
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
                var relatedData = new Expression<Func<Restaurant, object>>[]
                {
                    r => r.Schedule
                };
                await _restaurantRepository.Update(mappedRestaurant, r => r.UserId == userId, relatedData);
            } 
            else
            {
                await _restaurantRepository.Create(mappedRestaurant);
            }
        }
    }
}