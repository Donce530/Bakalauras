using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Configuration;
using Microsoft.Extensions.Options;
using Models.Reservations.Models.Dto;
using Models.Users.Models.Dao;
using Models.Users.Models.Data;
using Models.Users.Models.Dto;
using Moq;
using Users.Api.Services;
using Users.Repository;
using Xunit;

namespace UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly IOptions<AppSettings> _appSettings = Options.Create(new Fixture().Create<AppSettings>());
        private readonly Mock<IMapper> _mapper = new();
        private UserService TestClass => new(_appSettings, _usersRepository.Object, _mapper.Object);
        private readonly Mock<IUsersRepository> _usersRepository = new();
        private Fixture Fixture => new();

        [Fact]
        public void CanConstruct()
        {
            var instance = new UserService(_appSettings, _usersRepository.Object, _mapper.Object);
            Assert.NotNull(instance);
        }

        [Fact]
        public async Task CanCallRegisterEmailExists()
        {
            var registerRequest = Fixture.Create<RegisterRequest>();
            _usersRepository.Setup(_ => _.Exists(u => u.Email == registerRequest.Email)).ReturnsAsync(true);
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await TestClass.Register(registerRequest));
        }
        
        [Fact]
        public async Task CanCallRegister()
        {
            var registerRequest = Fixture.Create<RegisterRequest>();
            var userDao = Fixture.Create<UserDao>();
            _usersRepository.Setup(_ => _.Exists(u => u.Email == registerRequest.Email)).ReturnsAsync(false);
            _mapper.Setup(_ => _.Map<UserDao>(registerRequest)).Returns(userDao);
            
            await TestClass.Register(registerRequest);
            _usersRepository.Verify(_ => _.Create(It.IsAny<UserDao>()), Times.Once);
        }

        [Fact]
        public async Task CanCallGetPagedAndFiltered()
        {
            var parameters = Fixture.Create<PagedFilteredParams<UserFilters>>();
            parameters.Paginator.SortBy = "FirstName";
            var expected = Fixture.Create<PagedResponse<UserDataRow>>();
            _usersRepository
                .Setup(
                    _ => _.GetPaged<UserDataRow>(It.IsAny<Paginator>(),
                        It.IsAny<IEnumerable<Expression<Func<UserDao, bool>>>>(),
                        It.IsAny<Expression<Func<UserDao, object>>>())).ReturnsAsync(expected);
            var result = await TestClass.GetPagedAndFiltered(parameters);
            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task CanCallDelete()
        {
            const int userId = 1804221634;
            await TestClass.Delete(userId);
            _usersRepository.Verify(_ => _.Delete(u => u.Id == userId), Times.Once);
        }

        [Fact]
        public async Task CanCallAuthenticateAsyncInvalidPassword()
        {
            var model = Fixture.Create<AuthenticateRequest>();
            var userDao = Fixture.Build<UserDao>().Without(u => u.Salt).Create();
            var user = Fixture.Create<User>();
            _mapper.Setup(_ => _.Map<User>(userDao)).Returns(user);
            _usersRepository.Setup(_ => _.Get(u => u.Email.Equals(model.Email), false)).ReturnsAsync(userDao);
            var result = await TestClass.AuthenticateAsync(model);
            Assert.Null(result);
        }

        [Fact]
        public void CanCallGetById()
        {   
            var userDao = Fixture.Create<UserDao>();
            var user = Fixture.Create<User>();
            _mapper.Setup(_ => _.Map<User>(userDao)).Returns(user);
            var id = 1417999496;
            _usersRepository.Setup(_ => _.Get(u => u.Id == id, false)).ReturnsAsync(userDao);
            var result = TestClass.GetById(id);
            Assert.Equal(user, result);
        }
    }
}