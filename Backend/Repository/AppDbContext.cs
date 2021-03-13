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
        public DbSet<RestaurantPlan> RestaurantPlans { get; set; }
        public DbSet<PlanWall> PlanWalls { get; set; }
        public DbSet<PlanTable> PlanTables { get; set; }
        public DbSet<PlanItem> PlanItems { get; set; }

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

            modelBuilder.Entity<RestaurantPlan>()
                .HasOne(rp => rp.Restaurant)
                .WithOne(r => r.RestaurantPlan)
                .HasForeignKey<RestaurantPlan>(r => r.RestaurantId);

            modelBuilder.Entity<RestaurantPlan>()
                .HasMany(rp => rp.Tables)
                .WithOne(t => t.Plan)
                .HasForeignKey(p => p.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RestaurantPlan>()
                .HasMany(p => p.Walls)
                .WithOne(w => w.Plan)
                .HasForeignKey(w => w.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}