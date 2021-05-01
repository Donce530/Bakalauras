using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Models.Reservations.Models.Dto;
using Models.Restaurants.Models.Data;
using Models.Restaurants.Models.Dto;
using Models.Restaurants.Models.Enums;
using Models.Users.Models.Data;
using Moq;
using Restaurants.Api.Services;
using Restaurants.Repository;
using Users.Api.Services;
using Xunit;

namespace UnitTests.Services
{
    public class RestaurantServiceTests
    {
        private readonly Mock<IMapper> _mapper = new();
        private readonly Mock<IRestaurantPlanRepository> _restaurantPlanRepository = new();
        private readonly Mock<IRestaurantRepository> _restaurantRepository = new();
        private readonly Mock<IUserService> _userService = new();

        private RestaurantService TestClass => new(_userService.Object, _restaurantRepository.Object,
            _mapper.Object, _restaurantPlanRepository.Object);

        private Fixture Fixture => new();

        [Fact]
        public void CanConstruct()
        {
            var instance =
                new RestaurantService(_userService.Object, _restaurantRepository.Object,
                    _mapper.Object, _restaurantPlanRepository.Object);
            Assert.NotNull(instance);
        }
        
        [Fact]
        public async Task CanCallGetDetailsWithNoParameters()
        {
            var dto = Fixture.Create<RestaurantDto>();
            _userService.Setup(_ => _.User).Returns(Fixture.Create<User>());
            _restaurantRepository.Setup(_ => _.GetMapped<RestaurantDto>(It.IsAny<Expression<Func<Restaurant, bool>>>())).ReturnsAsync(dto);
            var result = await TestClass.GetDetails();
            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task CanCallGetDetailsWithId()
        {
            var id = 1374403947;
            var dto = Fixture.Create<RestaurantDto>();
            _restaurantRepository.Setup(_ => _.GetMapped<RestaurantDto>(It.IsAny<Expression<Func<Restaurant, bool>>>())).ReturnsAsync(dto);
            var result = await TestClass.GetDetails(id);
            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task CanCallSaveDetailsUpdate()
        {
            var dto = Fixture.Create<RestaurantDto>();
            var restaurant = Fixture.Build<Restaurant>().Without(r => r.Reservations).Without(r => r.User)
                .Without(r => r.Schedule).Without(r => r.RestaurantPlan).Create();
            _userService.Setup(_ => _.User).Returns(Fixture.Create<User>());
            _mapper.Setup(_ => _.Map<Restaurant>(dto)).Returns(restaurant);
            _restaurantRepository.Setup(_ => _.Exists(It.IsAny<Expression<Func<Restaurant, bool>>>())).ReturnsAsync(true);
            await TestClass.SaveDetails(dto);
            
            _restaurantRepository.Verify(_ => _.Update(restaurant, It.IsAny<Expression<Func<Restaurant, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task CanCallSaveDetailsCreate()
        {
            var dto = Fixture.Create<RestaurantDto>();
            var restaurant = Fixture.Build<Restaurant>().Without(r => r.Reservations).Without(r => r.User)
                .Without(r => r.Schedule).Without(r => r.RestaurantPlan).Create();
            _userService.Setup(_ => _.User).Returns(Fixture.Create<User>());
            _mapper.Setup(_ => _.Map<Restaurant>(dto)).Returns(restaurant);
            _restaurantRepository.Setup(_ => _.Exists(It.IsAny<Expression<Func<Restaurant, bool>>>()))
                .ReturnsAsync(false);
            await TestClass.SaveDetails(dto);

            _restaurantRepository.Verify(_ => _.Create(restaurant),
                Times.Once);
        }

        [Fact]
        public async Task CanCallGetPlanWithNoParameters()
        {
            var dto = Fixture.Create<RestaurantPlanDto>();
            dto.Tables = new[]
            {
                Fixture.Build<TableDto>().With(x => x.Id, 1).Create(),
                Fixture.Build<TableDto>().With(x => x.Id, 2).Create(),
                Fixture.Build<TableDto>().With(x => x.Id, 3).Create(),
                Fixture.Build<TableDto>().With(x => x.Id, 4).Create(),
                Fixture.Build<TableDto>().With(x => x.Id, 5).Create(),
            };

            var tableLinks = new[]
            {
                new TableLink {FirstTableId = 1, SecondTableId = 2},
                new TableLink {FirstTableId = 3, SecondTableId = 2},
                new TableLink {FirstTableId = 4, SecondTableId = 5}
            };
            
            _userService.Setup(_ => _.User).Returns(Fixture.Create<User>());
            _restaurantPlanRepository
                .Setup(_ => _.GetMapped<RestaurantPlanDto>(It.IsAny<Expression<Func<RestaurantPlan, bool>>>()))
                .ReturnsAsync(dto);
            _restaurantPlanRepository.Setup(_ => _.GetTableLinks(It.IsAny<Expression<Func<TableLink, bool>>>()))
                .ReturnsAsync(tableLinks);

            var result = await TestClass.GetPlan();
            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task CanCallGetPlanWithRestaurantId()
        {
            var dto = Fixture.Create<RestaurantPlanDto>();
            const int restaurantId = 173511696;
            _restaurantPlanRepository
                .Setup(_ => _.GetMapped<RestaurantPlanDto>(It.IsAny<Expression<Func<RestaurantPlan, bool>>>()))
                .ReturnsAsync(dto);
            var result = await TestClass.GetPlan(restaurantId);
            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task CantCallGetQrCodeAsClient()
        {
            var user = Fixture.Create<User>();
            user.Role = Role.Client;
            _userService.Setup(_ => _.User).Returns(user);
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await TestClass.GetQrCode());
        }
        
        [Fact]
        public async Task CanCallGetQrCode()
        {
            var user = Fixture.Create<User>();
            user.Role = Role.Admin;
            _userService.Setup(_ => _.User).Returns(user);
            _restaurantPlanRepository.Setup(_ => _.GetMapped(It.IsAny<Expression<Func<RestaurantPlan, bool>>>(),
                It.IsAny<Expression<Func<RestaurantPlan, int>>>())).ReturnsAsync(1);
            var result = await TestClass.GetQrCode();
            Assert.True(result != null);
        }

        [Fact]
        public async Task CanCallSavePlanUpdate()
        {
            var user = Fixture.Create<User>();
            _userService.Setup(_ => _.User).Returns(user);
            var dto = Fixture.Create<RestaurantPlanDto>();
            var fixture = Fixture;
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            
            var tables = new[]
            {
                fixture.Build<PlanTable>().With(x => x.Id, 1).With(x => x.Number, 1).With(l => l.LinkedTables, new List<PlanTable>()).Create(),
                fixture.Build<PlanTable>().With(x => x.Id, 2).With(x => x.Number, 2).With(l => l.LinkedTables, new List<PlanTable>()).Create(),
                fixture.Build<PlanTable>().With(x => x.Id, 3).With(x => x.Number, 3).With(l => l.LinkedTables, new List<PlanTable>()).Create(),
                fixture.Build<PlanTable>().With(x => x.Id, 4).With(x => x.Number, 4).With(l => l.LinkedTables, new List<PlanTable>()).Create(),
                fixture.Build<PlanTable>().With(x => x.Id, 5).With(x => x.Number, 5).With(l => l.LinkedTables, new List<PlanTable>()).Create(),
            };
            dto.Tables = new[]
            {
                fixture.Build<TableDto>().With(x => x.Id, 1).With(x => x.Number, 1).With(l => l.LinkedTableNumbers, new List<int>()).Create(),
                fixture.Build<TableDto>().With(x => x.Id, 2).With(x => x.Number, 2).With(l => l.LinkedTableNumbers, new List<int>()).Create(),
                fixture.Build<TableDto>().With(x => x.Id, 3).With(x => x.Number, 3).With(l => l.LinkedTableNumbers, new List<int>()).Create(),
                fixture.Build<TableDto>().With(x => x.Id, 4).With(x => x.Number, 4).With(l => l.LinkedTableNumbers, new List<int>()).Create(),
                fixture.Build<TableDto>().With(x => x.Id, 5).With(x => x.Number, 5).With(l => l.LinkedTableNumbers, new List<int>()).Create(),
            };
            
            var restaurant = Fixture.Build<RestaurantPlan>().Without(r => r.Restaurant).With(r => r.Tables, tables)
                .Without(r => r.Walls).Without(r => r.Labels).Create();
            _mapper.Setup(_ => _.Map<RestaurantPlan>(dto)).Returns(restaurant);
            _restaurantPlanRepository.Setup(_ => _.Exists(It.IsAny<Expression<Func<RestaurantPlan, bool>>>())).ReturnsAsync(true);
            await TestClass.SavePlan(dto);
            
            _restaurantPlanRepository.Verify(_ => _.Update(restaurant, It.IsAny<Expression<Func<RestaurantPlan, bool>>>()), Times.Once);
        }
        
        [Fact]
        public async Task CanCallSavePlanCreate()
        {
            var fixture = Fixture;
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            
            var tables = new[]
            {
                fixture.Build<PlanTable>().With(x => x.Id, 1).With(x => x.Number, 1).With(l => l.LinkedTables, new List<PlanTable>()).Create(),
                fixture.Build<PlanTable>().With(x => x.Id, 2).With(x => x.Number, 2).With(l => l.LinkedTables, new List<PlanTable>()).Create(),
                fixture.Build<PlanTable>().With(x => x.Id, 3).With(x => x.Number, 3).With(l => l.LinkedTables, new List<PlanTable>()).Create(),
                fixture.Build<PlanTable>().With(x => x.Id, 4).With(x => x.Number, 4).With(l => l.LinkedTables, new List<PlanTable>()).Create(),
                fixture.Build<PlanTable>().With(x => x.Id, 5).With(x => x.Number, 5).With(l => l.LinkedTables, new List<PlanTable>()).Create(),
            };
            
            var user = Fixture.Create<User>();
            _userService.Setup(_ => _.User).Returns(user);
            var dto = Fixture.Create<RestaurantPlanDto>();
            dto.Tables = new[]
            {
                fixture.Build<TableDto>().With(x => x.Id, 1).With(x => x.Number, 1).With(l => l.LinkedTableNumbers, new List<int>()).Create(),
                fixture.Build<TableDto>().With(x => x.Id, 2).With(x => x.Number, 2).With(l => l.LinkedTableNumbers, new List<int>()).Create(),
                fixture.Build<TableDto>().With(x => x.Id, 3).With(x => x.Number, 3).With(l => l.LinkedTableNumbers, new List<int>()).Create(),
                fixture.Build<TableDto>().With(x => x.Id, 4).With(x => x.Number, 4).With(l => l.LinkedTableNumbers, new List<int>()).Create(),
                fixture.Build<TableDto>().With(x => x.Id, 5).With(x => x.Number, 5).With(l => l.LinkedTableNumbers, new List<int>()).Create(),
            };
            
            var restaurant = Fixture.Build<RestaurantPlan>().Without(r => r.Restaurant).With(r => r.Tables, tables)
                .Without(r => r.Walls).Without(r => r.Labels).Create();
            _mapper.Setup(_ => _.Map<RestaurantPlan>(dto)).Returns(restaurant);
            _restaurantPlanRepository.Setup(_ => _.Exists(It.IsAny<Expression<Func<RestaurantPlan, bool>>>())).ReturnsAsync(false);
            await TestClass.SavePlan(dto);
            
            _restaurantPlanRepository.Verify(_ => _.Create(restaurant), Times.Once);
        }

        [Fact]
        public async Task CanCallGetPlanSvg()
        {
            var expectedResult = "aa";
            var user = Fixture.Create<User>();
            _userService.Setup(_ => _.User).Returns(user);
            _restaurantPlanRepository.Setup(_ => _.GetMapped(It.IsAny<Expression<Func<RestaurantPlan, bool>>>(),
                It.IsAny<Expression<Func<RestaurantPlan, string>>>())).ReturnsAsync(expectedResult);
            var result = await TestClass.GetPlanSvg();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task CanCallGetRestaurantCities()
        {
            var expected = Fixture.CreateMany<string>().ToList();
            _restaurantRepository.Setup(_ => _.GetAll(It.IsAny<Expression<Func<Restaurant, string>>>(), null, true))
                .ReturnsAsync(expected);
            var result = await TestClass.GetRestaurantCities();
            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task CanCallGetPage()
        {
            const int page = 1807058209;
            const string city = "TestValue1398961793";
            const string filter = "TestValue360996870";
            var expected = Fixture.Create<PagedResponse<RestaurantPageItemDto>>();
            _restaurantRepository.Setup(_ => _.GetPaged<RestaurantPageItemDto>(It.IsAny<Paginator>(),
                It.IsAny<IEnumerable<Expression<Func<Restaurant, bool>>>>(), null)).ReturnsAsync(expected);

            var result = await TestClass.GetPage(page, city, filter);
            Assert.Equal(result, expected.Results);
        }
    }
}