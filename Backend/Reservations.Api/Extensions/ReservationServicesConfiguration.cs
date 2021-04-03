using Microsoft.Extensions.DependencyInjection;
using Reservations.Api.Services;
using Reservations.Repository;

namespace Reservations.Api.Extensions
{
    public static class ReservationServicesConfiguration
    {
        public static void AddReservationServiceConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
        }
    }
}