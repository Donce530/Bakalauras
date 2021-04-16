using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models.Reservations.Models.Data;
using Repository;

namespace Reservations.Repository
{
    public class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
    {
        public ReservationRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override Task Update(Reservation entity, Expression<Func<Reservation, bool>> filter)
        {
            throw new InvalidOperationException("Reservations cannot be edited");
        }

        public async Task<ICollection<int>> GetTableIdsToReserve(int restaurantId, DateTime day, TimeSpan startTime, TimeSpan endTime)
        {
            return await DbContext.PlanTables.Where(t => !t.Reservations.Any(r =>
                    r.RestaurantId == restaurantId &&
                    r.Day.Year == day.Year &&
                    r.Day.DayOfYear == day.DayOfYear &&
                    ((r.Start < startTime && (r.End > startTime && r.End < endTime || r.End > endTime)) ||
                     (r.Start >= startTime && (endTime > r.Start && endTime < r.End || endTime > r.End)) ||
                     (r.Start == startTime && r.End == endTime))
                ))
                .Select(t => t.Id).ToListAsync();
        }

        public async Task UpdateState(int reservationId, int userId, ReservationState state, DateTime localTime)
        {
            var reservation =
                await DbContext.Reservations.SingleOrDefaultAsync(r => r.Id == reservationId && r.UserId == userId);

            if (reservation is null || reservation.State == state)
            {
                throw new InvalidOperationException();
            }

            reservation.State = state;

            switch (state)
            {
                case ReservationState.CheckedIn:
                    reservation.RealStart = localTime.TimeOfDay;
                    break;
                case ReservationState.CheckedOut:
                    reservation.RealEnd = localTime.TimeOfDay;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            await DbContext.SaveChangesAsync();
        }
    }
}