using SistemaVotacion.Core.Entities;
using SistemaVotacion.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVotacion.Infrastructure.Repositories
{
    public class VotoRepository : IVotoRepository
    {
        private readonly ApplicationDbContext _context;

        public VotoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExisteVotoPorVotanteAsync(int votanteId)
        {
            return await _context.Set<Voto>().AnyAsync(v => v.VotanteId == votanteId);
        }

        // Registra el voto y actualiza el estado del votante en una sola operación de guardado
        public async Task RegistrarVotoAsync(Voto voto, Votante votante)
        {
            await _context.Set<Voto>().AddAsync(voto);

            // Marcar el votante como que ya votó
            votante.YaVoto = true;
            _context.Set<Votante>().Update(votante);

            await _context.SaveChangesAsync();
        }

        // Agrupa los votos por partido para armar la pantalla de resultados
        public async Task<Dictionary<int, int>> ObtenerConteoPorPartidoAsync()
        {
            return await _context.Set<Voto>()
                .GroupBy(v => v.PartidoPoliticoId)
                .Select(g => new { PartidoId = g.Key, Cantidad = g.Count() })
                .ToDictionaryAsync(x => x.PartidoId, x => x.Cantidad);
        }
    }
}
