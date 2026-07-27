using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVotacion.Core.DTOs
{
    public class VotanteDto
    {
        public int Id { get; set; }
        public string Cedula { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public bool YaVoto { get; set; }
    }

    public class VotanteCreateDto
    {
        // Validaciones básicas que el controlador verificará
        [Required(ErrorMessage = "La cédula es obligatoria")]
        [StringLength(20)]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string NombreCompleto { get; set; } = string.Empty;
    }

    // Puedes crear un VotanteUpdateDto similar si necesitas actualizar otros datos
}
