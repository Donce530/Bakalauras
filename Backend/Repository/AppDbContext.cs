using Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository.Seed;
using Restaurants.Models.Data;
using Users.Models.Dao;

namespace Repository
{
    public class AppDbContext : DbContext
    {
        private readonly AppSettings _appSettings;

        public DbSet<UserDao> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<OpenHours> OpenHours { get; set; }

        public AppDbContext(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql(_appSettings.DbConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedAdmin();

            modelBuilder.Entity<UserDao>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<OpenHours>()
                .HasKey(oh => new {oh.WeekDay, oh.RestaurantId});

            modelBuilder.Entity<OpenHours>()
                .HasOne(oh => oh.Restaurant)
                .WithMany(r => r.Schedule)
                .HasForeignKey(oh => oh.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OpenHours>()
                .HasIndex(oh => new {oh.WeekDay, oh.RestaurantId});

            base.OnModelCreating(modelBuilder);
        }
    }
}