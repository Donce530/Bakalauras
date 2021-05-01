using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Models.Reservations.Models.Dto;
using Models.Restaurants.Models.Data;
using Models.Restaurants.Models.Dto;
using Models.Restaurants.Models.Enums;
using Moq;
using Restaurants.Api.Controllers;
using Restaurants.Api.Services;
using Xunit;

namespace UnitTests.Controllers
{
    public class RestaurantControllerTests
    {
        private readonly Mock<IRestaurantService> _restaurantService = new Mock<IRestaurantService>();
        private RestaurantController TestClass => new RestaurantController(_restaurantService.Object);
        private Fixture Fixture => new Fixture();

        [Fact]
        public void CanConstruct()
        {
            var instance = new RestaurantController(_restaurantService.Object);
            Assert.NotNull(instance);
        }

        [Fact]
        public async Task CanCallGetDetails()
        {
            var expectedResult = Fixture.Create<RestaurantDto>();
            _restaurantService.Setup(_ => _.GetDetails()).ReturnsAsync(expectedResult);
            
            var result = await TestClass.GetDetails();
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal((result as OkObjectResult)?.Value, expectedResult);
        }

        [Fact]
        public async Task CanCallGetDetailsById()
        {
            const int id = 114805795;
            var expectedResult = Fixture.Create<RestaurantDto>();
            _restaurantService.Setup(_ => _.GetDetails(id)).ReturnsAsync(expectedResult);
            
            var result = await TestClass.GetDetailsById(id);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal((result as OkObjectResult)?.Value, expectedResult);
        }

        [Fact]
        public async Task CanCallSaveDetails()
        {
            var restaurant = Fixture.Create<RestaurantDto>();
            
            var result = await TestClass.SaveDetails(restaurant);
            Assert.IsType<OkResult>(result);
            
            _restaurantService.Verify(_ => _.SaveDetails(restaurant), Times.Once);
        }

        [Fact]
        public async Task CannotCallSaveDetailsWithNullRestaurant()
        {
            var result = await TestClass.SaveDetails(default);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task CanCallGetQrCode()
        {
            _restaurantService.Setup(_ => _.GetQrCode()).ReturnsAsync(new Bitmap(5,5));
            var result = await TestClass.GetQrCode();
            Assert.IsType<FileStreamResult>(result);
        }

        [Fact]
        public async Task CanCallGetAvailableCities()
        {
            var expectedResult = Fixture.CreateMany<string>().ToList();
            _restaurantService.Setup(_ => _.GetRestaurantCities()).ReturnsAsync(expectedResult);
            
            var result = await TestClass.GetAvailableCities();
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal((result as OkObjectResult)?.Value, expectedResult);
        }

        [Fact]
        public async Task CanCallGetPage()
        {
            const int page = 1937298505;
            const string city = "TestValue1781209323";
            const string filter = "TestValue1917097506";
            var expectedResult = Fixture.CreateMany<RestaurantPageItemDto>().ToList();
            _restaurantService.Setup(_ => _.GetPage(page, city, filter)).ReturnsAsync(expectedResult);

            var result = await TestClass.GetPage(page, city, filter);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal((result as OkObjectResult)?.Value, expectedResult);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task CanCallGetPageWithNoFilter(string value)
        {
            const int page = 1937298505;
            const string city = "TestValue1781209323";
            var expectedResult = Fixture.CreateMany<RestaurantPageItemDto>().ToList();
            _restaurantService.Setup(_ => _.GetPage(page, city, value)).ReturnsAsync(expectedResult);

            var result = await TestClass.GetPage(page, city, value);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal((result as OkObjectResult)?.Value, expectedResult);
        }

        [Fact]
        public async Task CallGetPlanWithNoParametersAndNoRestaurantReturnsNoContentResult()
        {
            var result = await TestClass.GetPlan();
            Assert.IsType<NoContentResult>(result);
            _restaurantService.Verify(_ => _.GetPlan(), Times.Once);
        }

        [Fact]
        public async Task CanCallGetPlanWithRestaurantId()
        {
            const int restaurantId = 1156521485;
            var expectedResult = Fixture.Create<RestaurantPlanDto>();
            _restaurantService.Setup(_ => _.GetPlan(restaurantId)).ReturnsAsync(expectedResult);
            
            var result = await TestClass.GetPlan(restaurantId);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal((result as OkObjectResult)?.Value, expectedResult);
        }

        [Fact]
        public async Task CanCallGetPlanSvg()
        {
            var expectedResult = Fixture.Create<string>();
            _restaurantService.Setup(_ => _.GetPlanSvg()).ReturnsAsync(expectedResult);
            
            var result = await TestClass.GetPlanSvg();
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal((result as OkObjectResult)?.Value, expectedResult);
        }

        [Fact]
        public async Task CanCallSavePlan()
        {
            var plan = Fixture.Create<RestaurantPlanDto>();
            var result = await TestClass.SavePlan(plan);
            
            Assert.IsType<OkResult>(result);
            _restaurantService.Verify(_ => _.SavePlan(plan), Times.Once);
        }

        [Fact]
        public async Task CannotCallSavePlanWithNullPlan()
        {
            var result = await TestClass.SavePlan(default);
            
            Assert.IsType<BadRequestResult>(result);
            _restaurantService.Verify(_ => _.SavePlan(It.IsAny<RestaurantPlanDto>()), Times.Never);
        }
    }
}