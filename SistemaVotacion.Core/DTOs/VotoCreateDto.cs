using System.ComponentModel.DataAnnotations;

namespace SistemaVotacion.Core.DTOs
{
    public class VotoCreateDto
    {
        [Required(ErrorMessage = "La cédula es obligatoria")]
        public string CedulaVotante { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "El identificador del partido debe ser mayor que cero")]
        public int PartidoPoliticoId { get; set; }
    }
}
