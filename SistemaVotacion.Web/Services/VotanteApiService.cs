using System.Net;
using System.Net.Http.Json;
using SistemaVotacion.Core.DTOs;

namespace SistemaVotacion.Web.Services
{
    public class VotanteApiService : IVotanteApiService
    {
        private readonly HttpClient _httpClient;

        public VotanteApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<VotanteDto>> ObtenerTodosAsync() =>
            await _httpClient.GetFromJsonAsync<IEnumerable<VotanteDto>>("api/Votantes") ?? [];

        public async Task<VotanteDto?> ObtenerPorCedulaAsync(string cedula)
        {
            var response = await _httpClient.GetAsync($"api/Votantes/{cedula}");
            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<VotanteDto>();
        }

        public async Task<string> CrearVotanteAsync(VotanteCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Votantes", dto);
            return response.IsSuccessStatusCode ? string.Empty : await LeerMensajeErrorAsync(response);
        }

        public async Task<string> ActualizarVotanteAsync(int id, VotanteUpdateDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Votantes/{id}", dto);
            return response.IsSuccessStatusCode ? string.Empty : await LeerMensajeErrorAsync(response);
        }

        public async Task<string> EliminarVotanteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Votantes/{id}");
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
