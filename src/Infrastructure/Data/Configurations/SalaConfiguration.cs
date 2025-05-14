using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class SalaConfiguration : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.ToTable("Salas");
            builder.HasKey(s => s.Id);
            
            builder.Property(s => s.Nombre)
                .IsRequired()
                .HasMaxLength(50);
                
            builder.Property(s => s.Numero)
                .IsRequired();
                
            builder.Property(s => s.Status)
                .IsRequired()
                .HasDefaultValue(true);
                
            builder.HasMany(s => s.Asientos)
                .WithOne(a => a.Sala)
                .HasForeignKey(a => a.SalaId);
                
            builder.HasMany(s => s.Carteleras)
                .WithOne(c => c.Sala)
                .HasForeignKey(c => c.SalaId);
        }
    }
}