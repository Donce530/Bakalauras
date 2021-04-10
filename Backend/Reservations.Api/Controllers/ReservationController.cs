using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Reservations.Models.Dto;
using Models.Users.Models.Data;
using Reservations.Api.Services;
using Users.Api.Attributes;
using Users.Api.Services;

namespace Reservations.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;

        public ReservationController(IReservationService reservationService, IUserService userService)
        {
            _reservationService = reservationService;
            _userService = userService;
        }

        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create([FromBody] NewReservationDto newReservation)
        {
            await _reservationService.Create(newReservation);

            return Ok();
        }

        [HttpGet(nameof(GetByUser))]
        public async Task<IActionResult> GetByUser([FromQuery] string filter)
        {
            var reservations = await _reservationService.GetByUser(filter);

            return Ok(reservations);
        }

        [HttpPost(nameof(PagedAndFiltered))]
        public async Task<IActionResult> PagedAndFiltered([FromBody] PagedFilteredParams parameters)
        {
            if (parameters is null || _userService.User.Role == Role.Client)
            {
                return BadRequest();
            }

            var reservations = await _reservationService.GetPagedAndFiltered(parameters);

            return Ok(reservations);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var details = await _reservationService.GetDetails(id);

            if (details is null)
            {
                return NoContent();
            }

            return Ok(details);
        }

        [HttpGet(nameof(GetTablesAvailableToReserve))]
        public async Task<IActionResult> GetTablesAvailableToReserve([FromQuery] int restaurantId,
            [FromQuery] DateTime day, [FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
        {
            var tableIds = await _reservationService.GetTableIdsToReserve(restaurantId, day, startTime.TimeOfDay, endTime.TimeOfDay);

            return Ok(tableIds);
        }
    }
}
