using SistemaVotacion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVotacion.Infrastructure.Repositories
{
    public interface IVotanteRepository
    {
        Task<IEnumerable<Votante>> GetAllAsync();
        Task<Votante?> GetByIdAsync(int id);
        Task<Votante?> GetByCedulaAsync(string cedula);
        Task AddAsync(Votante votante);
        Task UpdateAsync(Votante votante);
        Task DeleteAsync(Votante votante);
        Task<bool> ExisteCedulaAsync(string cedula);

    }
}