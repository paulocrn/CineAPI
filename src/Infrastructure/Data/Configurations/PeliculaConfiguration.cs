using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class PeliculaConfiguration : IEntityTypeConfiguration<Pelicula>
    {
        public void Configure(EntityTypeBuilder<Pelicula> builder)
        {
            builder.ToTable("Peliculas");
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(p => p.Genero)
                .IsRequired()
                .HasConversion<string>();
                
            builder.Property(p => p.EdadMinimaPermitida)
                .IsRequired();
                
            builder.Property(p => p.DuracionMinutos)
                .IsRequired();
                
            builder.Property(p => p.Status)
                .IsRequired()
                .HasDefaultValue(true);
                
            builder.HasMany(p => p.Carteleras)
                .WithOne(c => c.Pelicula)
                .HasForeignKey(c => c.PeliculaId);
        }
    }
}