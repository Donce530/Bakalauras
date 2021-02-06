using System;
using Microsoft.Extensions.DependencyInjection;
using Users.Services;

namespace Users.Extensions
{
    public static class UsersServiceConfiguration
    {
        public static void AddUsersServiceConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }
    }
}