using ManageLogisticsRT.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageLogisticsRT.Data;


public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
            entity.Property(e => e.Email)
                .IsRequired();
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(t => t.Title);
        });
    }

}