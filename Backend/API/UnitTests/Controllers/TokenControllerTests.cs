using System;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Models.Users.Models.Dto;
using Moq;
using Users.Api.Controllers;
using Users.Api.Services;
using Xunit;

namespace UnitTests.Controllers
{
    public class TokenControllerTests
    {
        private TokenController TestClass => new(_userService.Object);
        private Fixture Fixture => new();
        private readonly Mock<IUserService> _userService = new();
        

        [Fact]
        public void CanConstruct()
        {
            var instance = new TokenController(_userService.Object);
            Assert.NotNull(instance);
        }

        [Fact]
        public async Task CanCallAuthenticateValid()
        {
            var model = Fixture.Create<AuthenticateRequest>();
            var response = Fixture.Create<AuthenticateResponse>();
            _userService.Setup(_ => _.AuthenticateAsync(model)).ReturnsAsync(response);
            
            var result = await TestClass.Authenticate(model);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal((result as OkObjectResult)?.Value, response);
        }
        
        [Fact]
        public async Task CanCallAuthenticateInvalid()
        {
            var model = Fixture.Create<AuthenticateRequest>();
            
            var result = await TestClass.Authenticate(model);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CannotCallAuthenticateWithNullModel()
        {
            var result = await TestClass.Authenticate(null);
            Assert.IsType<BadRequestResult>(result);
        }
    }
}