using SistemaVotacion.Core.DTOs;
using SistemaVotacion.Infrastructure.Repositories;
using SistemaVotacion.Core.Entities;

namespace SistemaVotacion.API.Services
{
    public class VotoService : IVotoService
    {
        private readonly IVotanteRepository _votanteRepository;
        private readonly IPartidoRepository _partidoRepository;
        private readonly IVotoRepository _votoRepository;

        public VotoService(IVotanteRepository votanteRepository, IPartidoRepository partidoRepository, IVotoRepository votoRepository)
        {
            _votanteRepository = votanteRepository;
            _partidoRepository = partidoRepository;
            _votoRepository = votoRepository;
        }

        public async Task<string> VotarAsync(VotoCreateDto dto)
        {
            // Buscar votante por cédula
            var votante = await _votanteRepository.GetByCedulaAsync(dto.CedulaVotante);
            if (votante == null) return "Votante no encontrado.";

            // Verificar si ya votó (campo YaVoto)
            if (votante.YaVoto) return "El votante ya ejerció su voto.";

            // Verificar registro en tabla Votos
            if (await _votoRepository.ExisteVotoPorVotanteAsync(votante.Id))
                return "El votante ya tiene un voto registrado.";

            // Buscar partido por id
            var partido = await _partidoRepository.GetByIdAsync(dto.PartidoPoliticoId);
            if (partido == null) return "Partido no encontrado.";
            if (!partido.Activo) return "El partido seleccionado está inactivo.";

            // Crear entidad Voto (no usamos AutoMapper para este caso)
            var voto = new Voto
            {
                VotanteId = votante.Id,
                PartidoPoliticoId = partido.Id,
                FechaVoto = DateTime.UtcNow
            };

            // Registrar el voto y actualizar votante en una sola operación atómica
            await _votoRepository.RegistrarVotoAsync(voto, votante);

            return string.Empty;
        }

        // Combina el listado de partidos con el conteo de votos (0 para los que aún no tienen votos)
        public async Task<IEnumerable<ResultadoVotacionDto>> ObtenerResultadosAsync()
        {
            var partidos = await _partidoRepository.GetAllAsync();
            var conteo = await _votoRepository.ObtenerConteoPorPartidoAsync();

            return partidos.Select(p => new ResultadoVotacionDto
            {
                PartidoId = p.Id,
                Nombre = p.Nombre,
                Siglas = p.Siglas,
                CantidadVotos = conteo.TryGetValue(p.Id, out var cantidad) ? cantidad : 0
            });
        }
    }
}
