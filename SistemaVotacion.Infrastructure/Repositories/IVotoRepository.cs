using SistemaVotacion.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVotacion.Infrastructure.Repositories
{
    public interface IVotoRepository
    {
        Task<bool> ExisteVotoPorVotanteAsync(int votanteId);
        Task RegistrarVotoAsync(Voto voto, Votante votante);
        // Conteo de votos agrupado por partido: clave = PartidoPoliticoId, valor = cantidad de votos
        Task<Dictionary<int, int>> ObtenerConteoPorPartidoAsync();
    }
}
