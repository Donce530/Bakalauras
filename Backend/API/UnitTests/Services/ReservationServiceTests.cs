using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Models.Reservations.Models.Data;
using Models.Reservations.Models.Dto;
using Models.Restaurants.Models.Data;
using Models.Users.Models.Data;
using Reservations.Api.Services;
using Moq;
using Reservations.Repository;
using Restaurants.Repository;
using Users.Api.Services;
using Xunit;

namespace UnitTests.Services
{
    public class ReservationServiceTests
    {
        private readonly Mock<IMapper> _mapper = new();
        private readonly Mock<IReservationRepository> _reservationRepository = new();
        private readonly Mock<IRestaurantPlanRepository> _restaurantPlanRepository = new();
        private readonly Mock<IUserService> _userService = new();
        private ReservationService TestClass => new(_mapper.Object, _userService.Object, _reservationRepository.Object, _restaurantPlanRepository.Object);
        private Fixture Fixture => new();
        

        [Fact]
        public void CanConstruct()
        {
            var instance = new ReservationService(_mapper.Object, _userService.Object, _reservationRepository.Object, _restaurantPlanRepository.Object);
            Assert.NotNull(instance);
        }

        [Fact]
        public async Task CanCallCreate()
        {
            var reservationDto = Fixture.Create<NewReservationDto>();
            var reservation = Fixture.Build<Reservation>()
                .Without(r => r.Restaurant).Without(r => r.Table).Without(r => r.User).Create();
            var user = Fixture.Create<User>();

            _userService.Setup(_ => _.User).Returns(user);
            _mapper.Setup(_ => _.Map<Reservation>(reservationDto)).Returns(reservation);
            
            await TestClass.Create(reservationDto);
            _reservationRepository.Verify(_ => _.Create(reservation), Times.Once);
        }

        [Fact]
        public async Task CanCallGetTableIdsToReserve()
        {
            const int restaurantId = 2131476850;
            var day = new DateTime(478098353);
            var startTime = new TimeSpan();
            var endTime = new TimeSpan();
            var tableIds = Fixture.CreateMany<int>().ToList();
            _reservationRepository.Setup(_ => _.GetTableIdsToReserve(restaurantId, day, startTime, endTime)).ReturnsAsync(tableIds);
            
            var result = await TestClass.GetTableIdsToReserve(restaurantId, day, startTime, endTime);
            Assert.Equal(tableIds, result);
        }

        [Fact]
        public async Task CanCallGetByUser()
        {
            const string filter = null;
            var user = Fixture.Create<User>();
            _userService.Setup(_ => _.User).Returns(user);
            var expectedResult = Fixture.CreateMany<ReservationListItemDto>().ToList();
            _reservationRepository
                .Setup(_ => _.GetAll<ReservationListItemDto>(It.IsAny<Expression<Func<Reservation, bool>>>(), false))
                .ReturnsAsync(expectedResult);
            var result = await TestClass.GetByUser(filter);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task CanCallGetPagedAndFiltered()
        {
            var parameters = Fixture.Create<PagedFilteredParams<ReservationFilters>>();
            parameters.Paginator.SortBy = "Day";
            var expectedResult = Fixture.Create<PagedResponse<ReservationDataRow>>();
            _reservationRepository.Setup(_ => _.GetPaged<ReservationDataRow>(It.IsAny<Paginator>(),
                It.IsAny<IEnumerable<Expression<Func<Reservation, bool>>>>(),
                It.IsAny<Expression<Func<Reservation, object>>>())).ReturnsAsync(expectedResult);
            var result = await TestClass.GetPagedAndFiltered(parameters);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task CanCallTryCheckIn()
        {
            const int restaurantId = 623522076;
            var localTime = new DateTime(663968023);
            var expectedResult = Fixture.Create<ReservationListItemDto>();
            _reservationRepository
                .Setup(_ => _.GetMapped<ReservationListItemDto>(It.IsAny<Expression<Func<Reservation, bool>>>()))
                .ReturnsAsync(expectedResult);
            
            var result = await TestClass.TryCheckIn(restaurantId, localTime);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task CanCallTryCheckOut()
        {
            const int restaurantId = 1469689162;
            var expectedResult = Fixture.Create<ReservationListItemDto>();
            _reservationRepository
                .Setup(_ => _.GetMapped<ReservationListItemDto>(It.IsAny<Expression<Func<Reservation, bool>>>()))
                .ReturnsAsync(expectedResult);
            var result = await TestClass.TryCheckOut(restaurantId);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task CanCallCheckIn()
        {
            const int reservationId = 1922767853;
            var localTime = new DateTime(672219801);
            var user = Fixture.Create<User>();
            _userService.Setup(_ => _.User).Returns(user);

            await TestClass.CheckIn(reservationId, localTime);

            _reservationRepository.Verify(
                _ => _.UpdateState(reservationId, user.Id, ReservationState.CheckedIn, localTime), Times.Once);
        }

        [Fact]
        public async Task CanCallCheckOut()
        {
            var reservationId = 431909975;
            var localTime = new DateTime(1840349805);
            
            var user = Fixture.Create<User>();
            _userService.Setup(_ => _.User).Returns(user);

            await TestClass.CheckOut(reservationId, localTime);

            _reservationRepository.Verify(
                _ => _.UpdateState(reservationId, user.Id, ReservationState.CheckedOut, localTime), Times.Once);
        }

        [Fact]
        public async Task CanCallCancel()
        {
            const int id = 1754169069;
            _reservationRepository.Setup(_ => _.Delete(It.IsAny<Expression<Func<Reservation, bool>>>()))
                .ReturnsAsync(1);
            await TestClass.Cancel(id);
            _reservationRepository.Verify(_ => _.Delete(It.IsAny<Expression<Func<Reservation, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task CanCallGetDetails()
        {
            var id = 1803487834;
            var expectedResult = Fixture.Build<ReservationDetails>().Without(r => r.LinkedTableDetails).Create();
            var linked = Fixture.Build<ReservationDetails>().Without(r => r.LinkedTableDetails).CreateMany().ToList();

            _reservationRepository
                .Setup(_ => _.GetMapped<ReservationDetails>(It.IsAny<Expression<Func<Reservation, bool>>>()))
                .ReturnsAsync(expectedResult);

            var tableLinks = Fixture.CreateMany<TableLink>().ToList();
            
            _restaurantPlanRepository.Setup(_ => _.GetTableLinks(It.IsAny<Expression<Func<TableLink, bool>>>()))
                .ReturnsAsync(tableLinks);

            _reservationRepository
                .Setup(_ => _.GetAll<ReservationDetails>(It.IsAny<Expression<Func<Reservation, bool>>>(), false))
                .ReturnsAsync(linked);
            
            var result = await TestClass.GetDetails(id);

            Assert.Equal(result, expectedResult);
        }
    }
}