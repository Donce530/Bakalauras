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
        public void CannotConstructWithNullDbContext()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new RestaurantPlansRepository(default, new Mock<IMapper>().Object));
        }

        [Fact]
        public void CannotConstructWithNullMapper()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new RestaurantPlansRepository(new AppDbContext(new Mock<IOptions<AppSettings>>().Object), default));
        }

        // [Fact]
        // public async Task CanCallUpdate()
        // {
        //     var entity = new RestaurantPlan
        //     {
        //         Id = 638261545, WebSvg = "TestValue894961497",
        //         Restaurant = new Restaurant
        //         {
        //             Id = 56303483, Title = "TestValue2054492885", Description = "TestValue827074939",
        //             Address = "TestValue1698535768", City = "TestValue1452777817",
        //             Schedule = new[]
        //             {
        //                 new OpenHours
        //                 {
        //                     Open = new DateTime(642938099), Close = new DateTime(938986487), WeekDay = WeekDay.Tuesday,
        //                     Restaurant = default, RestaurantId = 2077231425
        //                 },
        //                 new OpenHours
        //                 {
        //                     Open = new DateTime(566118373), Close = new DateTime(1383243863),
        //                     WeekDay = WeekDay.Wednesday, Restaurant = default, RestaurantId = 1600367864
        //                 },
        //                 new OpenHours
        //                 {
        //                     Open = new DateTime(1417114590), Close = new DateTime(50099970), WeekDay = WeekDay.Monday,
        //                     Restaurant = default, RestaurantId = 1382599589
        //                 }
        //             },
        //             User = new UserDao
        //             {
        //                 Id = 240994975, FirstName = "TestValue259916186", LastName = "TestValue1771081575",
        //                 Email = "TestValue727624779", Password = "TestValue588063661",
        //                 PhoneNumber = "TestValue1311656173", Salt = "TestValue620256073", Role = Role.Admin
        //             },
        //             UserId = 50593665, RestaurantPlan = default,
        //             Reservations = new Mock<ICollection<Reservation>>().Object
        //         },
        //         RestaurantId = 2012762727, Walls = new Mock<ICollection<PlanWall>>().Object,
        //         Tables = new Mock<ICollection<PlanTable>>().Object, Labels = new Mock<ICollection<PlanLabel>>().Object
        //     };
        //     var filter = new Expression<Func<RestaurantPlan, bool>>();
        //     await _testClass.Update(entity, filter);
        //     Assert.True(false, "Create or modify test");
        // }
        //
        // [Fact]
        // public async Task CannotCallUpdateWithNullEntity()
        // {
        //     await Assert.ThrowsAsync<ArgumentNullException>(() =>
        //         _testClass.Update(default, new Expression<Func<RestaurantPlan, bool>>()));
        // }

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

        [Fact]
        public async Task CanCallUpdateTableLinks()
        {
            var links = new Mock<ICollection<TableLink>>().Object;
            var planTableIds = new Mock<ICollection<int>>().Object;
            await _testClass.UpdateTableLinks(links, planTableIds);
            Assert.True(false, "Create or modify test");
        }

        [Fact]
        public async Task CannotCallUpdateTableLinksWithNullLinks()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _testClass.UpdateTableLinks(default, new Mock<ICollection<int>>().Object));
        }

        [Fact]
        public async Task CannotCallUpdateTableLinksWithNullPlanTableIds()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _testClass.UpdateTableLinks(new Mock<ICollection<TableLink>>().Object, default));
        }

        // [Fact]
        // public async Task CanCallGetTableLinks()
        // {
        //     var filter = new Expression<Func<TableLink, bool>>();
        //     var result = await _testClass.GetTableLinks(filter);
        //     Assert.True(false, "Create or modify test");
        // }

        [Fact]
        public async Task CannotCallGetTableLinksWithNullFilter()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.GetTableLinks(default));
        }
    }
}