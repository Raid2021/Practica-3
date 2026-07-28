using AutoMapper;
using SistemaVotacion.Core.DTOs;
using SistemaVotacion.Core.Entities;

namespace SistemaVotacion.API
{
    // Perfil de AutoMapper: mapea entidad Votante a VotanteDto y VotanteCreateDto a Votante.
    // NO se agrega mapa para VotanteUpdateDto para que la actualización siga siendo manual
    // y así evitar modificar propiedades controladas (YaVoto, FechaRegistro).
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Votante, VotanteDto>();
            CreateMap<VotanteCreateDto, Votante>();
            CreateMap<SistemaVotacion.Core.Entities.PartidoPolitico, SistemaVotacion.Core.DTOs.PartidoDto>();
            CreateMap<SistemaVotacion.Core.DTOs.PartidoCreateDto, SistemaVotacion.Core.Entities.PartidoPolitico>();
        }
    }
}
