using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVotacion.Core.DTOs;
using SistemaVotacion.Web.Models;
using SistemaVotacion.Web.Services;

namespace SistemaVotacion.Web.Controllers
{
    public class VotacionController : Controller
    {
        private readonly IVotanteApiService _votanteApiService;
        private readonly IPartidoApiService _partidoApiService;
        private readonly IVotacionApiService _votacionApiService;

        public VotacionController(
            IVotanteApiService votanteApiService,
            IPartidoApiService partidoApiService,
            IVotacionApiService votacionApiService)
        {
            _votanteApiService = votanteApiService;
            _partidoApiService = partidoApiService;
            _votacionApiService = votacionApiService;
        }

        public async Task<IActionResult> Votar()
        {
            var vm = new VotarViewModel { Partidos = await ObtenerPartidosActivosAsync() };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Votar(VotarViewModel vm)
        {
            vm.Partidos = await ObtenerPartidosActivosAsync();
            if (!ModelState.IsValid) return View(vm);

            // Validar que la cédula exista antes de intentar registrar el voto
            var votante = await _votanteApiService.ObtenerPorCedulaAsync(vm.Cedula);
            if (votante == null)
            {
                ModelState.AddModelError(nameof(vm.Cedula), "La cédula no está registrada como votante.");
                return View(vm);
            }

            if (votante.YaVoto)
            {
                ModelState.AddModelError(nameof(vm.Cedula), "Este votante ya ejerció su voto.");
                return View(vm);
            }

            var dto = new VotoCreateDto { CedulaVotante = vm.Cedula, PartidoPoliticoId = vm.PartidoPoliticoId };
            var error = await _votacionApiService.VotarAsync(dto);
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError(string.Empty, error);
                return View(vm);
            }

            TempData["Mensaje"] = "Voto registrado correctamente.";
            return RedirectToAction(nameof(Votar));
        }

        public async Task<IActionResult> Resultados()
        {
            var resultados = await _votacionApiService.ObtenerResultadosAsync();
            return View(resultados);
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerPartidosActivosAsync()
        {
            var partidos = await _partidoApiService.ObtenerTodosAsync();
            return partidos
                .Where(p => p.Activo)
                .Select(p => new SelectListItem($"{p.Nombre} ({p.Siglas})", p.Id.ToString()));
        }
    }
}
