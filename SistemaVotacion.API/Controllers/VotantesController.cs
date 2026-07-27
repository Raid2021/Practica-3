using Microsoft.AspNetCore.Mvc;
using SistemaVotacion.API.Services;
using SistemaVotacion.Core.DTOs;

namespace SistemaVotacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotantesController : ControllerBase
    {
        private readonly IVotanteService _votanteService;

        public VotantesController(IVotanteService votanteService)
        {
            _votanteService = votanteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var votantes = await _votanteService.ObtenerTodosAsync();
            return Ok(votantes);
        }

        [HttpGet("{cedula}")]
        public async Task<IActionResult> GetByCedula(string cedula)
        {
            var votante = await _votanteService.ObtenerPorCedulaAsync(cedula);
            if (votante == null) return NotFound("Votante no registrado.");
            return Ok(votante);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VotanteCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var error = await _votanteService.CrearVotanteAsync(dto);

            if (!string.IsNullOrEmpty(error))
                return BadRequest(new { mensaje = error }); // Manejo limpio sin Try-Catch

            return Ok(new { mensaje = "Votante creado exitosamente" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var error = await _votanteService.EliminarVotanteAsync(id);

            if (!string.IsNullOrEmpty(error))
                return BadRequest(new { mensaje = error });

            return Ok(new { mensaje = "Votante eliminado exitosamente" });
        }
    }
}