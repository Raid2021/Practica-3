using SistemaVotacion.Core.Entities;
using System.Threading.Tasks;

namespace SistemaVotacion.Infrastructure.Repositories
{
    public interface IVotoRepository
    {
        Task<bool> ExisteVotoPorVotanteAsync(int votanteId);
        Task RegistrarVotoAsync(Voto voto, Votante votante);
    }
}
