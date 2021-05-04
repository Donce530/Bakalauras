using Microsoft.EntityFrameworkCore;
using Models.Users.Models.Dao;
using Models.Users.Models.Data;

namespace Repository.Seed
{
    public static class AdminSeedExtension
    {
        public static void SeedAdmin(this ModelBuilder builder)
        {
            builder.Entity<UserDao>().HasData(
                new UserDao
                {
                    FirstName = "Administrator",
                    LastName = "Administrator",
                    Id = 1,
                    Email = "admin",
                    Password = "xblszZkYu3Mh/7hSDL636s+XK0fz8AgTBI2fY+ABMCktG7BF",
                    Salt = "xblszZkYu3Mh/7hSDL636g==",
                    Role = Role.Admin
                });
        }
    }
}