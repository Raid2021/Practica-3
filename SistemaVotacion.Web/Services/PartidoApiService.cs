using System.Net;
using System.Net.Http.Json;
using SistemaVotacion.Core.DTOs;

namespace SistemaVotacion.Web.Services
{
    public class PartidoApiService : IPartidoApiService
    {
        private readonly HttpClient _httpClient;

        public PartidoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PartidoDto>> ObtenerTodosAsync() =>
            await _httpClient.GetFromJsonAsync<IEnumerable<PartidoDto>>("api/Partidos") ?? [];

        public async Task<PartidoDto?> ObtenerPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Partidos/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PartidoDto>();
        }

        public async Task<string> CrearPartidoAsync(PartidoCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Partidos", dto);
            return response.IsSuccessStatusCode ? string.Empty : await LeerMensajeErrorAsync(response);
        }

        public async Task<string> ActualizarPartidoAsync(int id, PartidoUpdateDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Partidos/{id}", dto);
            return response.IsSuccessStatusCode ? string.Empty : await LeerMensajeErrorAsync(response);
        }

        public async Task<string> EliminarPartidoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Partidos/{id}");
            return response.IsSuccessStatusCode ? string.Empty : await LeerMensajeErrorAsync(response);
        }

        private static async Task<string> LeerMensajeErrorAsync(HttpResponseMessage response)
        {
            try
            {
                var error = await response.Content.ReadFromJsonAsync<ApiMensajeResponse>();
                return error?.Mensaje ?? "Ocurrió un error al procesar la solicitud.";
            }
            catch
            {
                return "Ocurrió un error al procesar la solicitud.";
            }
        }
    }
}
