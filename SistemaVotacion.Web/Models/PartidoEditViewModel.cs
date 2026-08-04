using SistemaVotacion.Core.DTOs;

namespace SistemaVotacion.Web.Models
{
    // Extiende el DTO de actualización solo para poder llevar el Id en el formulario de edición
    public class PartidoEditViewModel : PartidoUpdateDto
    {
        public int Id { get; set; }
    }
}
