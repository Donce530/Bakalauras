using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Users.Api.Services;
using Users.Models.Dto;

namespace Users.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                await _userService.Register(registerRequest);
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok();
        }
    }
}