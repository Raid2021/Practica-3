using SistemaVotacion.Core.DTOs;

namespace SistemaVotacion.API.Services
{
    public interface IVotanteService
    {
        Task<IEnumerable<VotanteDto>> ObtenerTodosAsync();
        Task<VotanteDto?> ObtenerPorCedulaAsync(string cedula);
        // Retornamos un string con el error o vacío si fue exitoso (Patrón Result simplificado)
        Task<string> CrearVotanteAsync(VotanteCreateDto dto);
        Task<string> EliminarVotanteAsync(int id);
    }
}
