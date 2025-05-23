using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class AsientoConfiguration : IEntityTypeConfiguration<Asiento>
    {
        public void Configure(EntityTypeBuilder<Asiento> builder)
        {
            builder.ToTable("Asientos");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                    .ValueGeneratedOnAdd();
            
            builder.Property(a => a.Numero)
                .IsRequired();
                
            builder.Property(a => a.Fila)
                .IsRequired();

            builder.Property(a => a.SalaId)
                .IsRequired();

            builder.Property(a => a.Status)
                .IsRequired();

            builder.HasOne(a => a.Sala)
                .WithMany(s => s.Asientos)
                .HasForeignKey(a => a.SalaId);
                
            builder.HasMany(a => a.Reservas)
                .WithOne(r => r.Asiento)
                .HasForeignKey(r => r.AsientoId);
        }
    }
}