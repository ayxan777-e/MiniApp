using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniApp.Data.Entities;

namespace MiniApp.Configurations;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.ToTable(nameof(Restaurant));

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(r => r.City)
            .IsRequired();
       
        builder.Property(r => r.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasMany(r => r.DiningTables)
            .WithOne(dt => dt.Restaurant)
            .HasForeignKey(dt => dt.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.Reservations)
            .WithOne(rs => rs.Restaurant)
            .HasForeignKey(rs => rs.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);

    }

  
}
