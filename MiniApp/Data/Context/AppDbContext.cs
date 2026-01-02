using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MiniApp.Data.Entities;

namespace MiniApp.Data.Context;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=.;Database=MiniAppDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Restaurant> Restaurants { get; set; } = null!;
    public DbSet<DiningTable> DiningTables { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;
}
