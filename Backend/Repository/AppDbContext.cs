using Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.Reservations.Models.Data;
using Models.Restaurants.Models.Data;
using Models.Users.Models.Dao;
using Repository.Seed;

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
        public DbSet<PlanLabel> PlanLabels { get; set; }
        public DbSet<PlanItem> PlanItems { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<TableLink> TableLinks { get; set; }

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
                .HasIndex(u => u.Email)
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
            
            modelBuilder.Entity<RestaurantPlan>()
                .HasMany(p => p.Labels)
                .WithOne(w => w.Plan)
                .HasForeignKey(w => w.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Restaurant)
                .WithMany(r => r.Reservations)
                .HasForeignKey(r => r.RestaurantId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Table)
                .WithMany(t => t.Reservations)
                .HasForeignKey(r => r.TableId);

            modelBuilder.Entity<UserDao>()
                .HasMany<Reservation>()
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<PlanTable>()
                .HasMany(x => x.LinkedTables)
                .WithMany(x => x.LinkedTables)
                .UsingEntity<TableLink>(
                    x => x.HasOne(tl => tl.FirstTable)
                        .WithMany().HasForeignKey(tl => tl.FirstTableId),
                    x => x.HasOne(tl => tl.SecondTable)
                        .WithMany().HasForeignKey(tl => tl.SecondTableId),
                    x => x.HasKey(tl => new { tl.FirstTableId, tl.SecondTableId }));

            base.OnModelCreating(modelBuilder);
        }
    }
}