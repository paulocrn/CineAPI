using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : Base
    {
        protected readonly CineDbContext _context;
        
        public RepositorioBase(CineDbContext context)
        {
            _context = context;
        }
        
        public async Task<T> ObtenerPorIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id) ?? throw new InvalidOperationException("Entidad no encontrada");
        }
        
        public async Task<IEnumerable<T>> ObtenerTodosAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        
        public async Task<T> AgregarAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        
        public async Task ActualizarAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        
        public async Task EliminarAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}