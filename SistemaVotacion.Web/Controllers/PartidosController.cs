using Microsoft.AspNetCore.Mvc;
using SistemaVotacion.Core.DTOs;
using SistemaVotacion.Web.Models;
using SistemaVotacion.Web.Services;

namespace SistemaVotacion.Web.Controllers
{
    public class PartidosController : Controller
    {
        private readonly IPartidoApiService _partidoApiService;

        public PartidosController(IPartidoApiService partidoApiService)
        {
            _partidoApiService = partidoApiService;
        }

        public async Task<IActionResult> Index()
        {
            var partidos = await _partidoApiService.ObtenerTodosAsync();
            return View(partidos);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(PartidoCreateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var error = await _partidoApiService.CrearPartidoAsync(dto);
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError(string.Empty, error);
                return View(dto);
            }

            TempData["Mensaje"] = "Partido creado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var partido = await _partidoApiService.ObtenerPorIdAsync(id);
            if (partido == null) return NotFound();

            var vm = new PartidoEditViewModel
            {
                Id = partido.Id,
                Nombre = partido.Nombre,
                Siglas = partido.Siglas,
                Descripcion = partido.Descripcion,
                Activo = partido.Activo
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PartidoEditViewModel vm)
        {
            if (id != vm.Id) return BadRequest();
            if (!ModelState.IsValid) return View(vm);

            var dto = new PartidoUpdateDto
            {
                Nombre = vm.Nombre,
                Siglas = vm.Siglas,
                Descripcion = vm.Descripcion,
                Activo = vm.Activo
            };
            var error = await _partidoApiService.ActualizarPartidoAsync(id, dto);
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError(string.Empty, error);
                return View(vm);
            }

            TempData["Mensaje"] = "Partido actualizado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var partido = await _partidoApiService.ObtenerPorIdAsync(id);
            if (partido == null) return NotFound();

            return View(partido);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var error = await _partidoApiService.EliminarPartidoAsync(id);
            TempData["Mensaje"] = string.IsNullOrEmpty(error) ? "Partido eliminado exitosamente." : error;
            return RedirectToAction(nameof(Index));
        }
    }
}
