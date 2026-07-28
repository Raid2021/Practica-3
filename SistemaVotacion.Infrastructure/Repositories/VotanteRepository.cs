using SistemaVotacion.Core.Entities;
using SistemaVotacion.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SistemaVotacion.Infrastructure.Repositories
{
    public class VotanteRepository : IVotanteRepository
    {
        private readonly ApplicationDbContext _context;

        public VotanteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // AsNoTracking() para consultas de solo lectura mejora rendimiento y evita tracking innecesario
        public async Task<IEnumerable<Votante>> GetAllAsync() => await _context.Votantes.AsNoTracking().ToListAsync();

        public async Task<Votante?> GetByIdAsync(int id) => await _context.Votantes.FindAsync(id);

        // Usar AsNoTracking para lecturas por cedula (no se planea modificar la entidad a partir de esta consulta)
        public async Task<Votante?> GetByCedulaAsync(string cedula) =>
            await _context.Votantes.AsNoTracking().FirstOrDefaultAsync(v => v.Cedula == cedula);

        public async Task<bool> ExisteCedulaAsync(string cedula) =>
            await _context.Votantes.AnyAsync(v => v.Cedula == cedula);

        public async Task AddAsync(Votante votante)
        {
            await _context.Votantes.AddAsync(votante);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Votante votante)
        {
            _context.Votantes.Update(votante);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Votante votante)
        {
            _context.Votantes.Remove(votante);
            await _context.SaveChangesAsync();
        }
    }
}
