using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Configuration;
using Microsoft.Extensions.Options;
using Models.Users.Models.Dao;
using Models.Users.Models.Data;
using Moq;
using Repository;
using Users.Repository;
using Xunit;

namespace UnitTests
{
    public class UsersRepositoryTests
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UsersRepository _testClass;

        public UsersRepositoryTests()
        {
            _dbContext = new AppDbContext(new Mock<IOptions<AppSettings>>().Object);
            _mapper = new Mock<IMapper>().Object;
            _testClass = new UsersRepository(_dbContext, _mapper);
        }

        [Fact]
        public void CanConstruct()
        {
            var instance = new UsersRepository(_dbContext, _mapper);
            Assert.NotNull(instance);
        }

        [Fact]
        public void CannotConstructWithNullDbContext()
        {
            Assert.Throws<ArgumentNullException>(() => new UsersRepository(default, new Mock<IMapper>().Object));
        }

        [Fact]
        public void CannotConstructWithNullMapper()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new UsersRepository(new AppDbContext(new Mock<IOptions<AppSettings>>().Object), default));
        }

        // [Fact]
        // public async Task CanCallUpdate()
        // {
        //     var entity = new UserDao
        //     {
        //         Id = 1156765031, FirstName = "TestValue927815050", LastName = "TestValue42646590",
        //         Email = "TestValue1199430809", Password = "TestValue1151822028", PhoneNumber = "TestValue313406335",
        //         Salt = "TestValue1474541014", Role = Role.Client
        //     };
        //     var filter = new Expression<Func<UserDao, bool>>();
        //     await _testClass.Update(entity, filter);
        //     Assert.True(false, "Create or modify test");
        // }
        //
        // [Fact]
        // public async Task CannotCallUpdateWithNullEntity()
        // {
        //     await Assert.ThrowsAsync<ArgumentNullException>(() =>
        //         _testClass.Update(default, new Expression<Func<UserDao, bool>>()));
        // }

        [Fact]
        public async Task CannotCallUpdateWithNullFilter()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _testClass.Update(
                    new UserDao
                    {
                        Id = 1695750669, FirstName = "TestValue100115481", LastName = "TestValue1137932188",
                        Email = "TestValue295827523", Password = "TestValue1977039261",
                        PhoneNumber = "TestValue1193288744", Salt = "TestValue1418814875", Role = Role.Client
                    }, default));
        }
    }
}