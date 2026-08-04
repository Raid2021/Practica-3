using SistemaVotacion.Core.DTOs;

namespace SistemaVotacion.Web.Services
{
    public interface IVotanteApiService
    {
        Task<IEnumerable<VotanteDto>> ObtenerTodosAsync();
        Task<VotanteDto?> ObtenerPorCedulaAsync(string cedula);
        // Retornamos un string con el error o vacío si fue exitoso, igual que en la API
        Task<string> CrearVotanteAsync(VotanteCreateDto dto);
        Task<string> ActualizarVotanteAsync(int id, VotanteUpdateDto dto);
        Task<string> EliminarVotanteAsync(int id);
    }
}
