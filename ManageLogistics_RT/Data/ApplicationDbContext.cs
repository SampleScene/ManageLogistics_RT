using CloudinaryDotNet.Actions;
using ManageLogistics_RT.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManageLogistics_RT.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserAuthData>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<AppUser> appUsers { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<PassReciept> passReciepts { get; set;}
        public DbSet<Price> prices { get; set; }
        public DbSet<Terminal> terminals { get; set; }
        public DbSet<UserAuthData> userAuthDatas { get; set; }
        public DbSet<Stop> stops { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Driver> Driveres { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripReceipt> TripReceipts { get; set; }
        public DbSet<Way> Ways { get; set; }
        public DbSet<StopsOnRoute> StopsOnRoutes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StopsOnRoute>()
                .HasKey(o => new { o.RouteId, o.StopId });
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(System.Console.WriteLine);
        }
    }
}
