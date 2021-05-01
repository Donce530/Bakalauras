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
        public void CannotConstructWithNullDbContext()
        {
            Assert.Throws<ArgumentNullException>(() => new RestaurantsRepository(default, new Mock<IMapper>().Object));
        }

        [Fact]
        public void CannotConstructWithNullMapper()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new RestaurantsRepository(new AppDbContext(new Mock<IOptions<AppSettings>>().Object), default));
        }

        // [Fact]
        // public async Task CanCallUpdate()
        // {
        //     var entity = new Restaurant
        //     {
        //         Id = 742836712, Title = "TestValue206818866", Description = "TestValue138269048",
        //         Address = "TestValue1402184152", City = "TestValue777659019",
        //         Schedule = new[]
        //         {
        //             new OpenHours
        //             {
        //                 Open = new DateTime(201145619), Close = new DateTime(4036223), WeekDay = WeekDay.Monday,
        //                 Restaurant = default, RestaurantId = 534846581
        //             },
        //             new OpenHours
        //             {
        //                 Open = new DateTime(1407568045), Close = new DateTime(390307879), WeekDay = WeekDay.Saturday,
        //                 Restaurant = default, RestaurantId = 1200035755
        //             },
        //             new OpenHours
        //             {
        //                 Open = new DateTime(578391190), Close = new DateTime(232413446), WeekDay = WeekDay.Monday,
        //                 Restaurant = default, RestaurantId = 1825430346
        //             }
        //         },
        //         User = new UserDao
        //         {
        //             Id = 2085546247, FirstName = "TestValue357363696", LastName = "TestValue1377612189",
        //             Email = "TestValue5657098", Password = "TestValue240774698", PhoneNumber = "TestValue710487958",
        //             Salt = "TestValue635983879", Role = Role.Admin
        //         },
        //         UserId = 2030915524,
        //         RestaurantPlan = new RestaurantPlan
        //         {
        //             Id = 426943968, WebSvg = "TestValue756956037", Restaurant = default, RestaurantId = 558799575,
        //             Walls = new Mock<ICollection<PlanWall>>().Object,
        //             Tables = new Mock<ICollection<PlanTable>>().Object,
        //             Labels = new Mock<ICollection<PlanLabel>>().Object
        //         },
        //         Reservations = new Mock<ICollection<Reservation>>().Object
        //     };
        //     var filter = new Expression<Func<Restaurant, bool>>();
        //     await _testClass.Update(entity, filter);
        //     Assert.True(false, "Create or modify test");
        // }
        //
        // [Fact]
        // public async Task CannotCallUpdateWithNullEntity()
        // {
        //     await Assert.ThrowsAsync<ArgumentNullException>(() =>
        //         _testClass.Update(default, new Expression<Func<Restaurant, bool>>()));
        // }

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