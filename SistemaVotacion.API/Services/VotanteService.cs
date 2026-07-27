using AutoMapper;
using SistemaVotacion.Core.DTOs;
using SistemaVotacion.Core.Entities;
using SistemaVotacion.Infrastructure.Repositories;

namespace SistemaVotacion.API.Services
{
    public class VotanteService : IVotanteService
    {
        private readonly IVotanteRepository _repository;
        private readonly IMapper _mapper; // Asumiendo que usan AutoMapper, como sugirió el profesor

        public VotanteService(IVotanteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VotanteDto>> ObtenerTodosAsync()
        {
            var votantes = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<VotanteDto>>(votantes);
        }

        public async Task<VotanteDto?> ObtenerPorCedulaAsync(string cedula)
        {
            var votante = await _repository.GetByCedulaAsync(cedula);
            return votante == null ? null : _mapper.Map<VotanteDto>(votante);
        }

        public async Task<string> CrearVotanteAsync(VotanteCreateDto dto)
        {
            // Validar regla de negocio: Cédula única
            if (await _repository.ExisteCedulaAsync(dto.Cedula))
            {
                return "Ya existe un votante registrado con esta cédula.";
            }

            var nuevoVotante = _mapper.Map<Votante>(dto);
            await _repository.AddAsync(nuevoVotante);

            return string.Empty; // Éxito
        }

        public async Task<string> EliminarVotanteAsync(int id)
        {
            var votante = await _repository.GetByIdAsync(id);
            if (votante == null) return "Votante no encontrado.";

            if (votante.YaVoto) return "No se puede eliminar un votante que ya ejerció su voto."; // Lógica extra

            await _repository.DeleteAsync(votante);
            return string.Empty; // Éxito
        }
    }
}