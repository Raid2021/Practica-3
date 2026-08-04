using System.Net.Http.Json;
using SistemaVotacion.Core.DTOs;

namespace SistemaVotacion.Web.Services
{
    public class VotacionApiService : IVotacionApiService
    {
        private readonly HttpClient _httpClient;

        public VotacionApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> VotarAsync(VotoCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Votacion", dto);
            return response.IsSuccessStatusCode ? string.Empty : await LeerMensajeErrorAsync(response);
        }

        public async Task<IEnumerable<ResultadoVotacionDto>> ObtenerResultadosAsync() =>
            await _httpClient.GetFromJsonAsync<IEnumerable<ResultadoVotacionDto>>("api/Votacion/resultados") ?? [];

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
