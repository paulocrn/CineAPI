using Domain.Entities;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context
{
    public class CineDbContext : DbContext
    {
        public CineDbContext(DbContextOptions<CineDbContext> opciones) : base(opciones)
        {
        }

        public DbSet<Cartelera> Carteleras { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Asiento> Asientos { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder constructorModelo)
        {
            base.OnModelCreating(constructorModelo);

            // Aplicar configuraciones
            constructorModelo.ApplyConfiguration(new CarteleraConfiguration());
            constructorModelo.ApplyConfiguration(new SalaConfiguration());
            constructorModelo.ApplyConfiguration(new AsientoConfiguration());
            constructorModelo.ApplyConfiguration(new PeliculaConfiguration());
            constructorModelo.ApplyConfiguration(new ClienteConfiguration());
            constructorModelo.ApplyConfiguration(new ReservaConfiguration());

            // Configuración adicional
            constructorModelo.Entity<Pelicula>()
                .Property(p => p.Genero)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Configurar comportamiento de eliminación
            foreach (var relacion in constructorModelo.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relacion.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancelacion = default)
        {
            // Manejo automático del estado
            var entradas = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Base && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entradaEntidad in entradas)
            {
                ((Base)entradaEntidad.Entity).Status = entradaEntidad.State != EntityState.Deleted;
            }

            try
            {
                return await base.SaveChangesAsync(cancelacion);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error al guardar cambios en la base de datos", ex);
            }
        }
    }
}