using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Models.Reservations.Models.Dto;
using Models.Users.Models.Data;
using Moq;
using Reservations.Api.Controllers;
using Reservations.Api.Services;
using Users.Api.Services;
using Xunit;

namespace UnitTests.Controllers
{
    public class ReservationControllerTests
    {
        private readonly Mock<IReservationService> _reservationService = new();
        private readonly Mock<IUserService> _userService = new();

        private ReservationController TestClass => new(_reservationService.Object, _userService.Object);
        private static Fixture Fixture => new();

        [Fact]
        public void CanConstruct()
        {
            var instance = new ReservationController(_reservationService.Object, _userService.Object);
            Assert.NotNull(instance);
        }

        [Fact]
        public async Task CanCallCreate()
        {
            var fixture = new Fixture();
            var newReservation = fixture.Create<NewReservationDto>();
            
            await TestClass.Create(newReservation);
            
            _reservationService.Verify(_ => _.Create(newReservation), Times.Once);
        }

        [Fact]
        public async Task CannotCallCreateWithNullNewReservation()
        {
            var result = await TestClass.Create(default);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task CanCallGetByUser()
        {
            const string filter = "TestValue1369811919";
            var reservations = Fixture.CreateMany<ReservationListItemDto>().ToList();
            
            _reservationService.Setup(_ => _.GetByUser(filter)).ReturnsAsync(reservations);
            
            var result = await TestClass.GetByUser(filter);

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal((result as OkObjectResult)?.Value, reservations);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task CanCallGetByUserWithNoFilter(string value)
        {
            await TestClass.GetByUser(value);
        }

        [Fact]
        public async Task CanCallPagedAndFiltered()
        {
            var parameters = Fixture.Create<PagedFilteredParams<ReservationFilters>>();
            var response = Fixture.Create<PagedResponse<ReservationDataRow>>();
            var user = Fixture.Build<User>()
                .With(u => u.Role, Role.Manager).Create();

            _userService.Setup(_ => _.User).Returns(user);
            _reservationService.Setup(_ => _.GetPagedAndFiltered(parameters)).ReturnsAsync(response);
            
            var result = await TestClass.PagedAndFiltered(parameters);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal((result as OkObjectResult)?.Value, response);
        }

        [Fact]
        public async Task CannotCallPagedAndFilteredWithNullParameters()
        {
            var result = await TestClass.PagedAndFiltered(default);
            Assert.IsType<BadRequestResult>(result);
        }
        
        [Fact]
        public async Task CannotCallPagedAndFilteredWithClientRole()
        {
            var parameters = Fixture.Create<PagedFilteredParams<ReservationFilters>>();
            var response = Fixture.Create<PagedResponse<ReservationDataRow>>();
            var user = Fixture.Create<User>();
            user.Role = Role.Client;

            _userService.Setup(_ => _.User).Returns(user);
            _reservationService.Setup(_ => _.GetPagedAndFiltered(parameters)).ReturnsAsync(response);
            
            var result = await TestClass.PagedAndFiltered(parameters);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task CanCallDetails()
        {
            const int id = 694438258;
            var expectedResult = Fixture.Build<ReservationDetails>().Without(f => f.LinkedTableDetails).Create();
            _reservationService.Setup(_ => _.GetDetails(id)).ReturnsAsync(expectedResult);
            
            var result = await TestClass.Details(id);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal((result as OkObjectResult)?.Value, expectedResult);
        }

        [Fact]
        public async Task CanCallTryCheckIn()
        {
            const int restaurantId = 972435334;
            var localTime = new DateTime(193069310);
            var expectedResult = Fixture.Create<ReservationListItemDto>();

            _reservationService.Setup(_ => _.TryCheckIn(restaurantId, localTime)).ReturnsAsync(expectedResult);
            
            var result = await TestClass.TryCheckIn(restaurantId, localTime);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal((result as OkObjectResult)?.Value, expectedResult);
        }

        [Fact]
        public async Task CanCallCheckIn()
        {
            const int reservationId = 1652855613;
            var localTime = new DateTime(37899034);
            
            var result = await TestClass.CheckIn(reservationId, localTime);
            Assert.IsType<OkResult>(result);
            _reservationService.Verify(_=> _.CheckIn(reservationId, localTime), Times.Once);
        }

        [Fact]
        public async Task CanCallTryCheckOut()
        {
            const int restaurantId = 1205374643;
            var expectedResult = Fixture.Create<ReservationListItemDto>();

            _reservationService.Setup(_ => _.TryCheckOut(restaurantId)).ReturnsAsync(expectedResult);
            
            var result = await TestClass.TryCheckOut(restaurantId);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal((result as OkObjectResult)?.Value, expectedResult);
        }

        [Fact]
        public async Task CanCallCheckOut()
        {
            const int reservationId = 659741423;
            var localTime = new DateTime(1286256289);
            
            var result = await TestClass.CheckOut(reservationId, localTime);
            Assert.IsType<OkResult>(result);
            _reservationService.Verify(_=> _.CheckOut(reservationId, localTime), Times.Once);
        }

        [Fact]
        public async Task CanCallGetTablesAvailableToReserve()
        {
            const int restaurantId = 374293701;
            var day = new DateTime(454308007);
            var startTime = new DateTime(1145907521);
            var endTime = new DateTime(1897550586);
            var expectedResult = Fixture.CreateMany<int>().ToList();
            _reservationService.Setup(_ => _.GetTableIdsToReserve(restaurantId, day, startTime.TimeOfDay, endTime.TimeOfDay))
                .ReturnsAsync(expectedResult);
            
            var result = await TestClass.GetTablesAvailableToReserve(restaurantId, day, startTime, endTime);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal((result as OkObjectResult)?.Value, expectedResult);
        }

        [Fact]
        public async Task CanCallCancel()
        {
            const int id = 797451072;
            await TestClass.Cancel(id);
            
            _reservationService.Verify(_ => _.Cancel(id), Times.Once);
        }
    }
}