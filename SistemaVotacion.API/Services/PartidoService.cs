using AutoMapper;
using SistemaVotacion.Core.DTOs;
using SistemaVotacion.Core.Entities;
using SistemaVotacion.Infrastructure.Repositories;

namespace SistemaVotacion.API.Services
{
    public class PartidoService : IPartidoService
    {
        private readonly IPartidoRepository _repository;
        private readonly IMapper _mapper;

        public PartidoService(IPartidoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PartidoDto>> ObtenerTodosAsync()
        {
            var partidos = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PartidoDto>>(partidos);
        }

        public async Task<PartidoDto?> ObtenerPorIdAsync(int id)
        {
            var partido = await _repository.GetByIdAsync(id);
            return partido == null ? null : _mapper.Map<PartidoDto>(partido);
        }

        public async Task<string> CrearPartidoAsync(PartidoCreateDto dto)
        {
            // Validaciones de unicidad
            if (await _repository.ExisteNombreAsync(dto.Nombre))
                return "Ya existe un partido con ese nombre.";

            if (await _repository.ExisteSiglasAsync(dto.Siglas))
                return "Ya existen siglas registradas para otro partido.";

            var nuevo = _mapper.Map<PartidoPolitico>(dto);
            // Regla de negocio: todo partido nuevo se crea activo por defecto
            nuevo.Activo = true;
            await _repository.AddAsync(nuevo);
            return string.Empty;
        }

        public async Task<string> ActualizarPartidoAsync(int id, PartidoUpdateDto dto)
        {
            var partido = await _repository.GetByIdAsync(id);
            if (partido == null) return "Partido no encontrado.";

            // Si cambia el nombre, validar unicidad excluyendo este registro
            if (!string.Equals(partido.Nombre, dto.Nombre, StringComparison.OrdinalIgnoreCase))
            {
                if (await _repository.ExisteNombreAsync(dto.Nombre))
                    return "Ya existe un partido con ese nombre.";
            }

            if (!string.Equals(partido.Siglas, dto.Siglas, StringComparison.OrdinalIgnoreCase))
            {
                if (await _repository.ExisteSiglasAsync(dto.Siglas))
                    return "Ya existen siglas registradas para otro partido.";
            }

            // Actualización manual de campos permitidos
            partido.Nombre = dto.Nombre;
            partido.Siglas = dto.Siglas;
            partido.Descripcion = dto.Descripcion;
            partido.Activo = dto.Activo;

            await _repository.UpdateAsync(partido);
            return string.Empty;
        }

        public async Task<string> EliminarPartidoAsync(int id)
        {
            var partido = await _repository.GetByIdAsync(id);
            if (partido == null) return "Partido no encontrado.";

            await _repository.DeleteAsync(partido);
            return string.Empty;
        }
    }
}
