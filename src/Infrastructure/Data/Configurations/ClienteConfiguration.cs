using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.NumeroDocumento)
                .IsRequired()
                .HasMaxLength(20);
                
            builder.HasIndex(c => c.NumeroDocumento)
                .IsUnique();
                
            builder.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(30);
                
            builder.Property(c => c.Apellido)
                .IsRequired()
                .HasMaxLength(30);
                
            builder.Property(c => c.Edad)
                .IsRequired();
                
            builder.Property(c => c.Telefono)
                .HasMaxLength(20);
                
            builder.Property(c => c.Email)
                .HasMaxLength(100);
                
            builder.Property(c => c.Status)
                .IsRequired()
                .HasDefaultValue(true);
                
            builder.HasMany(c => c.Reservas)
                .WithOne(r => r.Cliente)
                .HasForeignKey(r => r.ClienteId);
        }
    }
}