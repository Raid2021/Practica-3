using SistemaVotacion.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVotacion.API.Services
{
    public interface IPartidoService
    {
        Task<IEnumerable<PartidoDto>> ObtenerTodosAsync();
        Task<PartidoDto?> ObtenerPorIdAsync(int id);
        Task<string> CrearPartidoAsync(PartidoCreateDto dto);
        Task<string> ActualizarPartidoAsync(int id, PartidoUpdateDto dto);
        Task<string> EliminarPartidoAsync(int id);
    }
}
