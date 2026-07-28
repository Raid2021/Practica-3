using Microsoft.AspNetCore.Mvc;
using SistemaVotacion.API.Services;
using SistemaVotacion.Core.DTOs;

namespace SistemaVotacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidosController : ControllerBase
    {
        private readonly IPartidoService _partidoService;

        public PartidosController(IPartidoService partidoService)
        {
            _partidoService = partidoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var partidos = await _partidoService.ObtenerTodosAsync();
            return Ok(partidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var partido = await _partidoService.ObtenerPorIdAsync(id);
            if (partido == null) return NotFound("Partido no registrado.");
            return Ok(partido);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PartidoCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var error = await _partidoService.CrearPartidoAsync(dto);
            if (!string.IsNullOrEmpty(error)) return BadRequest(new { mensaje = error });

            return Ok(new { mensaje = "Partido creado exitosamente" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PartidoUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var error = await _partidoService.ActualizarPartidoAsync(id, dto);
            if (!string.IsNullOrEmpty(error)) return BadRequest(new { mensaje = error });

            return Ok(new { mensaje = "Partido actualizado exitosamente" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var error = await _partidoService.EliminarPartidoAsync(id);
            if (!string.IsNullOrEmpty(error)) return BadRequest(new { mensaje = error });

            return Ok(new { mensaje = "Partido eliminado exitosamente" });
        }
    }
}
