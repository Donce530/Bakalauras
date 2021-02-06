using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Users.Attributes;
using Users.Models.Authentication;
using Users.Services;

namespace Users.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;

        public TokenController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(nameof(Authenticate))]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [Authorize]
        [HttpGet(nameof(GetAll))]
        public async Task<IActionResult> GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}