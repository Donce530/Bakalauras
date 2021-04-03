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
                    Password = "GL2wAuCc9zf3Laur5fp+Q8dymRZIxrf3Vxa8DsyV8QAtLMFx",
                    Salt = "GL2wAuCc9zf3Laur5fp+Qw==",
                    Role = Role.Admin
                });
        }
    }
}