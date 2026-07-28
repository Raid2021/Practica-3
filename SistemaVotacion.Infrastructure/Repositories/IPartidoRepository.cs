using SistemaVotacion.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVotacion.Infrastructure.Repositories
{
    public interface IPartidoRepository
    {
        Task<IEnumerable<PartidoPolitico>> GetAllAsync();
        Task<PartidoPolitico?> GetByIdAsync(int id);
        Task<bool> ExisteNombreAsync(string nombre);
        Task<bool> ExisteSiglasAsync(string siglas);
        Task AddAsync(PartidoPolitico partido);
        Task UpdateAsync(PartidoPolitico partido);
        Task DeleteAsync(PartidoPolitico partido);
    }
}
