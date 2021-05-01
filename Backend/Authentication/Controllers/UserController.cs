using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Reservations.Models.Dto;
using Models.Users.Models.Data;
using Models.Users.Models.Dto;
using Users.Api.Services;

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
        
        [HttpPost(nameof(PagedAndFiltered))]
        public async Task<IActionResult> PagedAndFiltered([FromBody] PagedFilteredParams<UserFilters> parameters)
        {
            if (parameters is null || _userService.User.Role != Role.Admin)
            {
                return BadRequest();
            }

            var users = await _userService.GetPagedAndFiltered(parameters);

            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (_userService.User.Role != Role.Admin)
            {
                return Forbid();
            }

            await _userService.Delete(id);

            return Ok();
        }
    }
}