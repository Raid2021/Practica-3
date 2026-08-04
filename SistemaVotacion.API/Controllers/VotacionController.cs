using Microsoft.AspNetCore.Mvc;
using SistemaVotacion.API.Services;
using SistemaVotacion.Core.DTOs;

namespace SistemaVotacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotacionController : ControllerBase
    {
        private readonly IVotoService _votoService;

        public VotacionController(IVotoService votoService)
        {
            _votoService = votoService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VotoCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var error = await _votoService.VotarAsync(dto);
            if (!string.IsNullOrEmpty(error)) return BadRequest(new { mensaje = error });

            return Ok(new { mensaje = "Voto registrado correctamente" });
        }

        [HttpGet("resultados")]
        public async Task<IActionResult> Resultados()
        {
            var resultados = await _votoService.ObtenerResultadosAsync();
            return Ok(resultados);
        }
    }
}
