using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniApp.Data.Entities;

namespace MiniApp.Configurations;

public class DiningTableConfiguration : IEntityTypeConfiguration<DiningTable>
{
    public void Configure(EntityTypeBuilder<DiningTable> builder)
    {
        builder.ToTable(nameof(DiningTable));

        builder.HasKey(dt => dt.Id);

        builder.Property(dt => dt.DiningTableNumber)
            .IsRequired();

        builder.HasIndex(dt => new {dt.RestaurantId,dt.DiningTableNumber})
            .IsUnique();

        builder.ToTable(t => t.HasCheckConstraint("CK_Reservation_GuestCount_NonNegative", "[Capacity] >= 1"));


        builder.Property(dt => dt.IsActive)
            .HasDefaultValue(true);


        builder.HasMany(dt => dt.Reservations)
            .WithOne(rs => rs.DiningTable)
            .HasForeignKey(rs => rs.DiningTableId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
