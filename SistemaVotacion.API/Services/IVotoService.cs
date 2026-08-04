using SistemaVotacion.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVotacion.API.Services
{
    public interface IVotoService
    {
        Task<string> VotarAsync(VotoCreateDto dto);
        Task<IEnumerable<ResultadoVotacionDto>> ObtenerResultadosAsync();
    }
}
