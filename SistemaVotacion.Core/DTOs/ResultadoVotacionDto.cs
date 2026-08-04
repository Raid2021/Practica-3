namespace SistemaVotacion.Core.DTOs
{
    public class ResultadoVotacionDto
    {
        public int PartidoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Siglas { get; set; } = string.Empty;
        public int CantidadVotos { get; set; }
    }
}
