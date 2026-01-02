using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniApp.Data.Entities;

namespace MiniApp.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(rs => rs.Id);

        builder.Property(rs => rs.CustomerName)
            .IsRequired();

        builder.ToTable(t => t.HasCheckConstraint("CK_Reservation_GuestCount_NonNegative", "[GuestCount] >= 1"));

        builder.ToTable(t => t.HasCheckConstraint("CK_Reservation_ReservationDate_Future", "[ReservationDate] >= GETDATE()"));

        builder.Property(rs => rs.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");
    }

   
}
