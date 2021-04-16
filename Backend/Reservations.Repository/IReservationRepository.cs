using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Reservations.Models.Data;
using Repository;

namespace Reservations.Repository
{
    public interface IReservationRepository : IRepositoryBase<Reservation>
    {
        Task<ICollection<int>> GetTableIdsToReserve(int restaurantId, DateTime day, TimeSpan startTime, TimeSpan endTime);
        Task UpdateState(int reservationId, int userId, ReservationState state, DateTime localTime);
    }
}