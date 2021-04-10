using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Models.Reservations.Models.Dto;
using Models.Restaurants.Models.Data;
using Models.Restaurants.Models.Dto;
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

        public async Task<RestaurantDto> GetDetails(int id)
        {
            var restaurant = await _restaurantRepository.GetMapped<RestaurantDto>(r => r.Id == id);

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

        public async Task<RestaurantPlanDto> GetPlan()
        {
            var userId = _userService.User.Id;
            var plan = await _restaurantPlanRepository.GetMapped<RestaurantPlanDto>(
                rp => rp.Restaurant.UserId == userId);

            await AddTableLinks(plan);
            
            return plan;
        }

        private async Task AddTableLinks(RestaurantPlanDto plan)
        {
            var tableIds = plan.Tables.Select(t => t.Id).ToList();
            var tableLinks = (await _restaurantPlanRepository.GetTableLinks(l =>
                tableIds.Contains(l.FirstTableId) || tableIds.Contains(l.SecondTableId))).ToList();

            foreach (var table in plan.Tables)
            {
                var linkedTables = plan.Tables.Where(t =>
                {
                    var link = new TableLink
                    {
                        FirstTableId = table.Id,
                        SecondTableId = t.Id
                    };

                    return tableLinks.Exists(tl => tl.Equals(link));
                });

                table.LinkedTableNumbers = linkedTables.Select(lt => lt.Number).ToList();
            }
        }

        public async Task<RestaurantPlanDto> GetPlan(int restaurantId)
        {
            var plan = await _restaurantPlanRepository.GetMapped<RestaurantPlanDto>(
                rp => rp.RestaurantId == restaurantId);

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

            await UpdateTableLinks(mappedPlan, plan);
        }

        private async Task UpdateTableLinks(RestaurantPlan plan, RestaurantPlanDto planDto)
        {
            var links = new List<TableLink>();
            var tablesByNumber = plan.Tables.ToDictionary(table => table.Number, table => table);
            
            foreach (var table in planDto.Tables)
            {
                var tableId = tablesByNumber[table.Number].Id;
                foreach (var linkedTable in table.LinkedTableNumbers.Select(ltn => tablesByNumber[ltn]))
                {
                    if (!links.Exists(l => l.FirstTableId == tableId && l.SecondTableId == linkedTable.Id ||
                                           l.SecondTableId == tableId && l.FirstTableId == linkedTable.Id))
                    {
                        links.Add(new TableLink
                        {
                            FirstTableId = tableId,
                            SecondTableId = linkedTable.Id
                        });
                    }
                }
            }

            await _restaurantPlanRepository.UpdateTableLinks(links, plan.Tables.Select(t => t.Id).ToList());
        }

        public async Task<string> GetPlanSvg()
        {
            var userId = _userService.User.Id;
            var planSvg = await _restaurantPlanRepository.GetMapped(rp => rp.Restaurant.UserId == userId,
                rp => rp.WebSvg);

            return planSvg;
        }

        public async Task<IList<string>> GetRestaurantCities()
        {
            var cities = await _restaurantRepository.GetAll(r => r.City, distinct: true);

            return cities;
        }

        public async Task<IList<RestaurantPageItemDto>> GetPage(int page, string city, string filter)
        {
            city = city.ToLower();
            filter = filter?.ToLower();
            Expression<Func<Restaurant, bool>> filterExpression = string.IsNullOrEmpty(filter) ?
                r => r.City.ToLower().Equals(city) :
                r => r.City.ToLower().Equals(city) && r.Title.ToLower().Contains(filter) || r.Address.ToLower().Contains(filter);

            var paginator = new Paginator
            {
                Offset = page * 10,
                Rows = 10
            };
            var restaurants = await _restaurantRepository.GetPaged<RestaurantPageItemDto>(paginator, new List<Expression<Func<Restaurant, bool>>> {filterExpression});

            return restaurants.Results;
        }
    }
}