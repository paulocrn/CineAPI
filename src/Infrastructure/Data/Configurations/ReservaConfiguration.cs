using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.ToTable("Reservas");
            builder.HasKey(r => r.Id);
            
            builder.Property(r => r.FechaReserva)
                .IsRequired()
                .HasColumnType("datetime");
                
            builder.Property(r => r.Status)
                .IsRequired()
                .HasDefaultValue(true);
                
            builder.HasOne(r => r.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasOne(r => r.Asiento)
                .WithMany(a => a.Reservas)
                .HasForeignKey(r => r.AsientoId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasOne(r => r.Cartelera)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.CarteleraId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}