using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CarteleraConfiguration : IEntityTypeConfiguration<Cartelera>
    {
        public void Configure(EntityTypeBuilder<Cartelera> builder)
        {
            builder.ToTable("Carteleras");
            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.Fecha)
                .IsRequired()
                .HasColumnType("datetime");
                
            builder.Property(c => c.HoraInicio)
                .IsRequired()
                .HasColumnType("time");
                
            builder.Property(c => c.HoraFin)
                .IsRequired()
                .HasColumnType("time");
                
            builder.Property(c => c.Status)
                .IsRequired()
                .HasDefaultValue(true);
                
            builder.HasOne(c => c.Pelicula)
                .WithMany(p => p.Carteleras)
                .HasForeignKey(c => c.PeliculaId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasOne(c => c.Sala)
                .WithMany(s => s.Carteleras)
                .HasForeignKey(c => c.SalaId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasMany(c => c.Reservas)
                .WithOne(r => r.Cartelera)
                .HasForeignKey(r => r.CarteleraId);
        }
    }
}