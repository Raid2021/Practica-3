using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SistemaVotacion.Web.Models
{
    public class VotarViewModel
    {
        [Required(ErrorMessage = "La cédula es obligatoria")]
        [StringLength(20)]
        public string Cedula { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un partido político")]
        public int PartidoPoliticoId { get; set; }

        // Se recarga en cada request (GET y POST) para poder redibujar el dropdown
        public IEnumerable<SelectListItem> Partidos { get; set; } = new List<SelectListItem>();
    }
}
