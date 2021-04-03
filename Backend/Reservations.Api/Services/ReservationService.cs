using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Models.Reservations.Models.Data;
using Models.Reservations.Models.Dto;
using Reservations.Repository;
using Users.Api.Services;

namespace Reservations.Api.Services
{
    internal class ReservationService : IReservationService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IMapper mapper, IUserService userService, IReservationRepository reservationRepository)
        {
            _mapper = mapper;
            _userService = userService;
            _reservationRepository = reservationRepository;
        }

        public async Task Create(NewReservationDto reservation)
        {
            var mappedReservation = _mapper.Map<Reservation>(reservation);
            mappedReservation.UserId = _userService.User.Id;

            await _reservationRepository.Create(mappedReservation);
        }

        public async Task<ICollection<int>> GetTableIdsToReserve(int restaurantId, DateTime day, TimeSpan startTime, TimeSpan endTime)
        {
            var tableIds = await _reservationRepository.GetTableIdsToReserve(restaurantId, day, startTime, endTime);

            return tableIds;
        }

        public async Task<ICollection<ReservationListItemDto>> GetByUser(string filter)
        {
            Expression<Func<Reservation, bool>> filterExpression = filter is null
                ? r => r.User.Id == _userService.User.Id
                : r => r.User.Id == _userService.User.Id &&
                       r.Restaurant.Title.ToLower().Contains(filter.ToLower()) ||
                       r.Restaurant.Address.ToLower().Contains(filter.ToLower()) ||
                       r.Table.Number.ToString().Contains(filter) ||
                       r.Table.Seats.ToString().Contains(filter);
            
            var reservations = await _reservationRepository.GetAll<ReservationListItemDto>(filterExpression);

            return reservations;
        }
    }
}
