using SistemaVotacion.Core.DTOs;

namespace SistemaVotacion.Web.Services
{
    public interface IVotacionApiService
    {
        Task<string> VotarAsync(VotoCreateDto dto);
        Task<IEnumerable<ResultadoVotacionDto>> ObtenerResultadosAsync();
    }
}
