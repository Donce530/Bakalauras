using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Models.Reservations.Models.Data;
using Models.Reservations.Models.Dto;
using Reservations.Repository;
using Restaurants.Repository;
using Users.Api.Services;

namespace Reservations.Api.Services
{
    internal class ReservationService : IReservationService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IReservationRepository _reservationRepository;
        private readonly IRestaurantPlanRepository _restaurantPlanRepository;

        public ReservationService(IMapper mapper, IUserService userService,
            IReservationRepository reservationRepository, IRestaurantPlanRepository restaurantPlanRepository)
        {
            _mapper = mapper;
            _userService = userService;
            _reservationRepository = reservationRepository;
            _restaurantPlanRepository = restaurantPlanRepository;
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

        public async Task<PagedResponse<ReservationDataRow>> GetPagedAndFiltered(PagedFilteredParams<ReservationFilters> parameters)
        {
            var filters = new List<Expression<Func<Reservation, bool>>>();
            filters.Add((r => r.Restaurant.UserId == _userService.User.Id));
            if (parameters.Filters is not null)
            {
                if (!string.IsNullOrEmpty(parameters.Filters.Name))
                {
                    filters.Add(r => (r.User.FirstName.ToLower() + " " + r.User.LastName.ToLower()).Contains(parameters.Filters.Name.ToLower()));
                }

                if (parameters.Filters.TableNumber is not null)
                {
                    filters.Add(r => r.Table.Number == parameters.Filters.TableNumber.Value);
                }

                if (parameters.Filters.Day is not null)
                {
                    filters.Add(r => r.Day.Year.Equals(parameters.Filters.Day.Value.Year) && r.Day.DayOfYear.Equals(parameters.Filters.Day.Value.DayOfYear));
                }

                if (parameters.Filters.StartAfter is not null)
                {
                    filters.Add(r => r.Start >= parameters.Filters.StartAfter.Value.TimeOfDay);
                }
                
                if (parameters.Filters.StartUntil is not null)
                {
                    filters.Add(r => r.Start <= parameters.Filters.StartUntil.Value.TimeOfDay);
                }
                
                if (parameters.Filters.EndAfter is not null)
                {
                    filters.Add(r => r.End >= parameters.Filters.EndAfter.Value.TimeOfDay);
                }
                
                if (parameters.Filters.EndUntil is not null)
                {
                    filters.Add(r => r.End <= parameters.Filters.EndUntil.Value.TimeOfDay);
                }
                
                if (parameters.Filters.RealStartAfter is not null)
                {
                    filters.Add(r => r.RealStart >= parameters.Filters.RealStartAfter.Value.TimeOfDay);
                }
                
                if (parameters.Filters.RealStartUntil is not null)
                {
                    filters.Add(r => r.RealStart <= parameters.Filters.RealStartUntil.Value.TimeOfDay);
                }
                
                if (parameters.Filters.RealEndAfter is not null)
                {
                    filters.Add(r => r.RealEnd >= parameters.Filters.RealEndAfter.Value.TimeOfDay);
                }
                
                if (parameters.Filters.RealEndUntil is not null)
                {
                    filters.Add(r => r.RealEnd <= parameters.Filters.RealEndUntil.Value.TimeOfDay);
                }
            }

            Expression<Func<Reservation, object>> orderBy = null;
            if (parameters.Paginator.SortOrder != 0 && !string.IsNullOrEmpty(parameters.Paginator.SortBy))
            {
                orderBy = parameters.Paginator.SortBy switch
                {
                    nameof(ReservationDataRow.User) => r => r.User.FirstName + " " + r.User.LastName,
                    nameof(ReservationDataRow.Day) => r => r.Day,
                    nameof(ReservationDataRow.Start) => r => r.Start,
                    nameof(ReservationDataRow.End) => r => r.End,
                    nameof(ReservationDataRow.TableNumber) => r => r.Table.Number,
                    nameof(Reservation.RealStart) => r => r.RealStart,
                    nameof(Reservation.RealEnd) => r => r.RealEnd,
                    _ => throw new ArgumentOutOfRangeException(nameof(parameters.Paginator.SortBy), parameters.Paginator.SortBy)
                };
            }

            var pagedResults = await _reservationRepository.GetPaged<ReservationDataRow>(parameters.Paginator, filters, orderBy);

            return pagedResults;
        }

        public async Task<ReservationListItemDto> TryCheckIn(int restaurantId, DateTime localTime)
        {
            var localTimeOfDay = localTime.TimeOfDay;
        
            var reservation = await _reservationRepository.GetMapped<ReservationListItemDto>(r =>
                r.Day.DayOfYear == localTime.DayOfYear && r.Day.Year == localTime.Year &&
                r.Start.Hours - localTimeOfDay.Hours <= 1 &&
                localTimeOfDay.Hours - r.Start.Hours >= -1 &&
                r.State == ReservationState.Created &&
                r.UserId == _userService.User.Id &&
                r.RestaurantId == restaurantId);

            return reservation;
        }

        public async Task<ReservationListItemDto> TryCheckOut(int restaurantId)
        {
            var reservation = await _reservationRepository.GetMapped<ReservationListItemDto>(r =>
                r.Day.DayOfYear == DateTime.Today.DayOfYear && r.Day.Year == DateTime.Today.Year &&
                r.State == ReservationState.CheckedIn &&
                r.UserId == _userService.User.Id &&
                r.RestaurantId == restaurantId);

            return reservation;
        }

        public async Task CheckIn(int reservationId, DateTime localTime)
        {
            await _reservationRepository.UpdateState(reservationId, _userService.User.Id, ReservationState.CheckedIn, localTime);
        }

        public async Task CheckOut(int reservationId, DateTime localTime)
        {
            await _reservationRepository.UpdateState(reservationId, _userService.User.Id, ReservationState.CheckedOut, localTime);
        }

        public async Task Cancel(int id)
        {
            var removedItemsCount = await _reservationRepository.Delete(r => r.Id == id);

            if (removedItemsCount < 0)
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<ReservationDetails> GetDetails(int id)
        {
            var details = await _reservationRepository.GetMapped<ReservationDetails>(r => r.Id == id);

            var tableLinks = await _restaurantPlanRepository.GetTableLinks(l =>
                l.FirstTableId == details.TableId || l.SecondTableId == details.TableId);
            var linkedTableIds = tableLinks.Select(l => l.FirstTableId).Concat(tableLinks.Select(l => l.SecondTableId))
                .Where(i => i != details.TableId);

            details.LinkedTableDetails =
                await _reservationRepository.GetAll<ReservationDetails>(r => linkedTableIds.Contains(r.TableId) && 
                    r.Day.Year == details.Day.Year &&
                    r.Day.DayOfYear == details.Day.DayOfYear &&
                    (r.Start < details.Start.TimeOfDay && (r.End > details.Start.TimeOfDay && r.End < details.End.TimeOfDay || r.End > details.End.TimeOfDay) ||
                     r.Start >= details.Start.TimeOfDay && (details.End.TimeOfDay > r.Start && details.End.TimeOfDay < r.End || details.End.TimeOfDay > r.End) ||
                     r.Start == details.Start.TimeOfDay && r.End == details.End.TimeOfDay || 
                    r.RealStart < details.RealStart.TimeOfDay && (r.RealEnd > details.RealStart.TimeOfDay && r.RealEnd < details.RealEnd.TimeOfDay || r.RealEnd > details.RealEnd.TimeOfDay) ||
                     r.RealStart >= details.RealStart.TimeOfDay && (details.RealEnd.TimeOfDay > r.RealStart && details.RealEnd.TimeOfDay < r.RealEnd || details.RealEnd.TimeOfDay > r.RealEnd) ||
                     r.RealStart == details.RealStart.TimeOfDay && r.RealEnd == details.RealEnd.TimeOfDay));
            
            return details;
        }
    }
}
