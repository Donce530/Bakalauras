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
    public class RestaurantPlansRepositoryTests
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly RestaurantPlansRepository _testClass;

        public RestaurantPlansRepositoryTests()
        {
            _dbContext = new AppDbContext(new Mock<IOptions<AppSettings>>().Object);
            _mapper = new Mock<IMapper>().Object;
            _testClass = new RestaurantPlansRepository(_dbContext, _mapper);
        }

        [Fact]
        public void CanConstruct()
        {
            var instance = new RestaurantPlansRepository(_dbContext, _mapper);
            Assert.NotNull(instance);
        }

        [Fact]
        public async Task CannotCallUpdateWithNullFilter()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _testClass.Update(
                    new RestaurantPlan
                    {
                        Id = 737840868, WebSvg = "TestValue773890186",
                        Restaurant = new Restaurant
                        {
                            Id = 1520667691, Title = "TestValue31271792", Description = "TestValue2123159465",
                            Address = "TestValue729854534", City = "TestValue1764286317",
                            Schedule = new[]
                            {
                                new OpenHours
                                {
                                    Open = new DateTime(217078835), Close = new DateTime(1619192427),
                                    WeekDay = WeekDay.Sunday, Restaurant = default, RestaurantId = 786461738
                                },
                                new OpenHours
                                {
                                    Open = new DateTime(563048276), Close = new DateTime(321853586),
                                    WeekDay = WeekDay.Thursday, Restaurant = default, RestaurantId = 1497496237
                                },
                                new OpenHours
                                {
                                    Open = new DateTime(582406563), Close = new DateTime(1070881475),
                                    WeekDay = WeekDay.Saturday, Restaurant = default, RestaurantId = 924282841
                                }
                            },
                            User = new UserDao
                            {
                                Id = 206690345, FirstName = "TestValue2129330850", LastName = "TestValue310896900",
                                Email = "TestValue16934149", Password = "TestValue961868116",
                                PhoneNumber = "TestValue1175665086", Salt = "TestValue2085403339", Role = Role.Manager
                            },
                            UserId = 167336718, RestaurantPlan = default,
                            Reservations = new Mock<ICollection<Reservation>>().Object
                        },
                        RestaurantId = 1615723469, Walls = new Mock<ICollection<PlanWall>>().Object,
                        Tables = new Mock<ICollection<PlanTable>>().Object,
                        Labels = new Mock<ICollection<PlanLabel>>().Object
                    }, default));
        }
    }
}