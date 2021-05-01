using System;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Models.Reservations.Models.Dto;
using Models.Users.Models.Data;
using Models.Users.Models.Dto;
using Moq;
using Users.Api.Controllers;
using Users.Api.Services;
using Xunit;

namespace UnitTests.Controllers
{
    public class UserControllerTests
    {
        private UserController TestClass => new(_userService.Object);
        private readonly Mock<IUserService> _userService = new();
        private Fixture Fixture => new();

        [Fact]
        public void CanConstruct()
        {
            var instance = new UserController(_userService.Object);
            Assert.NotNull(instance);
        }

        [Fact]
        public async Task CanCallRegister()
        {
            var registerRequest = Fixture.Create<RegisterRequest>();
            var result = await TestClass.Register(registerRequest);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task CanCallRegisterInvalidParameters()
        {
            var registerRequest = Fixture.Create<RegisterRequest>();
            _userService.Setup(_ => _.Register(registerRequest)).ThrowsAsync(new InvalidOperationException());
            var result = await TestClass.Register(registerRequest);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CanCallPagedAndFiltered()
        {
            var parameters = Fixture.Create<PagedFilteredParams<UserFilters>>();
            var expectedResult = Fixture.Create<PagedResponse<UserDataRow>>();
            
            var user = Fixture.Create<User>();
            user.Role = Role.Admin;
            _userService.Setup(_ => _.User).Returns(user);
            _userService.Setup(_ => _.GetPagedAndFiltered(parameters)).ReturnsAsync(expectedResult);
            
            var result = await TestClass.PagedAndFiltered(parameters);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal((result as OkObjectResult)?.Value, expectedResult);
        }

        [Fact]
        public async Task CannotCallPagedAndFilteredWithNullParameters()
        {
            var result = await TestClass.PagedAndFiltered(default);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task CanCallDeleteAsAdmin()
        {
            var id = 108488312;
            var user = Fixture.Create<User>();
            user.Role = Role.Admin;
            _userService.Setup(_ => _.User).Returns(user);
            var result = await TestClass.Delete(id);

            Assert.IsType<OkResult>(result);
        }
        
        [Fact]
        public async Task CantCallDeleteAsUser()
        {
            var id = 108488312;
            var user = Fixture.Create<User>();
            user.Role = Role.Manager;
            _userService.Setup(_ => _.User).Returns(user);
            var result = await TestClass.Delete(id);

            Assert.IsType<ForbidResult>(result);
        }
    }
    
}