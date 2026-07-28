using System;

namespace SistemaVotacion.Core.Entities
{
    // Entidad Voto: modelo de datos para representar un voto emitido.
    // Contiene identificadores a Votante y PartidoPolitico y fecha del voto.
    public class Voto
    {
        public int Id { get; set; }

        // Claves foráneas
        public int VotanteId { get; set; }
        public int PartidoPoliticoId { get; set; }

        // Fecha del voto, inicializada a UtcNow para un valor seguro por defecto
        public DateTime FechaVoto { get; set; } = DateTime.UtcNow;

        // Propiedades de navegación
        public Votante? Votante { get; set; }
        public PartidoPolitico? PartidoPolitico { get; set; }
    }
}
