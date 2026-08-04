using SistemaVotacion.Core.DTOs;

namespace SistemaVotacion.Web.Services
{
    public interface IPartidoApiService
    {
        Task<IEnumerable<PartidoDto>> ObtenerTodosAsync();
        Task<PartidoDto?> ObtenerPorIdAsync(int id);
        Task<string> CrearPartidoAsync(PartidoCreateDto dto);
        Task<string> ActualizarPartidoAsync(int id, PartidoUpdateDto dto);
        Task<string> EliminarPartidoAsync(int id);
    }
}
