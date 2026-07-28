using System.ComponentModel.DataAnnotations;

namespace SistemaVotacion.Core.DTOs
{
    public class PartidoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Siglas { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
    }

    public class PartidoCreateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Las siglas son obligatorias")]
        [StringLength(20)]
        public string Siglas { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }
    }

    public class PartidoUpdateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Las siglas son obligatorias")]
        [StringLength(20)]
        public string Siglas { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }

        public bool Activo { get; set; }
    }
}
