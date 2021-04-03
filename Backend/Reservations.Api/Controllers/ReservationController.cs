using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Reservations.Models.Dto;
using Reservations.Api.Services;
using Users.Api.Attributes;

namespace Reservations.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
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

        [HttpGet(nameof(GetTablesAvailableToReserve))]
        public async Task<IActionResult> GetTablesAvailableToReserve([FromQuery] int restaurantId,
            [FromQuery] DateTime day, [FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
        {
            var tableIds = await _reservationService.GetTableIdsToReserve(restaurantId, day, startTime.TimeOfDay, endTime.TimeOfDay);

            return Ok(tableIds);
        }
    }
}
