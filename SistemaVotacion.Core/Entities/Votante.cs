using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVotacion.Core.Entities
{
    public class Votante
    {
        public int Id { get; set; }
        public string Cedula { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public bool YaVoto { get; set; } = false; // Útil para la Persona 2
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
