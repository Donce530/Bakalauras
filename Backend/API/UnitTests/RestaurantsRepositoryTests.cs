using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Configuration;
using Microsoft.Extensions.Options;
using Models.Reservations.Models.Data;
using Models.Restaurants.Models.Data;
using Models.Restaurants.Models.Enums;
using Models.Users.Models.Dao;
using Models.Users.Models.Data;
using Moq;
using Repository;
using Restaurants.Repository;
using Xunit;

namespace UnitTests
{
    public class RestaurantsRepositoryTests
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly RestaurantsRepository _testClass;

        public RestaurantsRepositoryTests()
        {
            _dbContext = new AppDbContext(new Mock<IOptions<AppSettings>>().Object);
            _mapper = new Mock<IMapper>().Object;
            _testClass = new RestaurantsRepository(_dbContext, _mapper);
        }

        [Fact]
        public void CanConstruct()
        {
            var instance = new RestaurantsRepository(_dbContext, _mapper);
            Assert.NotNull(instance);
        }

        [Fact]
        public async Task CannotCallUpdateWithNullFilter()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _testClass.Update(
                    new Restaurant
                    {
                        Id = 1003090353, Title = "TestValue37756126", Description = "TestValue1862674564",
                        Address = "TestValue1596949599", City = "TestValue748839013",
                        Schedule = new[]
                        {
                            new OpenHours
                            {
                                Open = new DateTime(876355715), Close = new DateTime(2098122562),
                                WeekDay = WeekDay.Tuesday, Restaurant = default, RestaurantId = 1308347771
                            },
                            new OpenHours
                            {
                                Open = new DateTime(1691678214), Close = new DateTime(1212572260),
                                WeekDay = WeekDay.Thursday, Restaurant = default, RestaurantId = 25842829
                            },
                            new OpenHours
                            {
                                Open = new DateTime(536034894), Close = new DateTime(649683717),
                                WeekDay = WeekDay.Monday, Restaurant = default, RestaurantId = 370608798
                            }
                        },
                        User = new UserDao
                        {
                            Id = 929295095, FirstName = "TestValue1879989357", LastName = "TestValue1932004350",
                            Email = "TestValue365730265", Password = "TestValue1497718387",
                            PhoneNumber = "TestValue2147340739", Salt = "TestValue657299921", Role = Role.Manager
                        },
                        UserId = 1610066371,
                        RestaurantPlan = new RestaurantPlan
                        {
                            Id = 502062014, WebSvg = "TestValue1643814555", Restaurant = default,
                            RestaurantId = 1649768816, Walls = new Mock<ICollection<PlanWall>>().Object,
                            Tables = new Mock<ICollection<PlanTable>>().Object,
                            Labels = new Mock<ICollection<PlanLabel>>().Object
                        },
                        Reservations = new Mock<ICollection<Reservation>>().Object
                    }, default));
        }
    }
}