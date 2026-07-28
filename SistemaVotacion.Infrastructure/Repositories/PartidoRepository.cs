using SistemaVotacion.Core.Entities;
using SistemaVotacion.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVotacion.Infrastructure.Repositories
{
    public class PartidoRepository : IPartidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PartidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lecturas de solo lectura usan AsNoTracking
        public async Task<IEnumerable<PartidoPolitico>> GetAllAsync() =>
            await _context.Set<PartidoPolitico>().AsNoTracking().ToListAsync();

        // Para editar/eliminar se usa FindAsync (tracked)
        public async Task<PartidoPolitico?> GetByIdAsync(int id) =>
            await _context.Set<PartidoPolitico>().FindAsync(id);

        public async Task<bool> ExisteNombreAsync(string nombre) =>
            await _context.Set<PartidoPolitico>().AnyAsync(p => p.Nombre == nombre);

        public async Task<bool> ExisteSiglasAsync(string siglas) =>
            await _context.Set<PartidoPolitico>().AnyAsync(p => p.Siglas == siglas);

        public async Task AddAsync(PartidoPolitico partido)
        {
            await _context.Set<PartidoPolitico>().AddAsync(partido);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PartidoPolitico partido)
        {
            _context.Set<PartidoPolitico>().Update(partido);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PartidoPolitico partido)
        {
            _context.Set<PartidoPolitico>().Remove(partido);
            await _context.SaveChangesAsync();
        }
    }
}
