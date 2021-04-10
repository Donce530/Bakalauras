using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Reservations.Models.Dto;

namespace Reservations.Api.Services
{
    public interface IReservationService
    {
        public Task Create(NewReservationDto reservation);
        Task<ICollection<int>> GetTableIdsToReserve(int restaurantId, DateTime day, TimeSpan startTime, TimeSpan endTime);
        Task<ICollection<ReservationListItemDto>> GetByUser(string filter);
        Task<PagedResponse<ReservationDataRow>> GetPagedAndFiltered(PagedFilteredParams parameters);
        Task<ReservationDetails> GetDetails(int id);
    }
}