using Microsoft.Extensions.DependencyInjection;
using Users.Api.Services;
using Users.Repository;

namespace Users.Api.Extensions
{
    public static class UsersServiceConfiguration
    {
        public static void AddUsersServiceConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IUsersRepository, UsersRepository>();
        }
    }
}