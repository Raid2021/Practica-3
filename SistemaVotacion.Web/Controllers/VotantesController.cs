using Microsoft.AspNetCore.Mvc;
using SistemaVotacion.Core.DTOs;
using SistemaVotacion.Web.Models;
using SistemaVotacion.Web.Services;

namespace SistemaVotacion.Web.Controllers
{
    public class VotantesController : Controller
    {
        private readonly IVotanteApiService _votanteApiService;

        public VotantesController(IVotanteApiService votanteApiService)
        {
            _votanteApiService = votanteApiService;
        }

        public async Task<IActionResult> Index()
        {
            var votantes = await _votanteApiService.ObtenerTodosAsync();
            return View(votantes);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(VotanteCreateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var error = await _votanteApiService.CrearVotanteAsync(dto);
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError(string.Empty, error);
                return View(dto);
            }

            TempData["Mensaje"] = "Votante creado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id, string cedula)
        {
            var votante = await _votanteApiService.ObtenerPorCedulaAsync(cedula);
            if (votante == null) return NotFound();

            var vm = new VotanteEditViewModel
            {
                Id = id,
                Cedula = votante.Cedula,
                NombreCompleto = votante.NombreCompleto
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, VotanteEditViewModel vm)
        {
            if (id != vm.Id) return BadRequest();
            if (!ModelState.IsValid) return View(vm);

            var dto = new VotanteUpdateDto { Cedula = vm.Cedula, NombreCompleto = vm.NombreCompleto };
            var error = await _votanteApiService.ActualizarVotanteAsync(id, dto);
            if (!string.IsNullOrEmpty(error))
            {
                ModelState.AddModelError(string.Empty, error);
                return View(vm);
            }

            TempData["Mensaje"] = "Votante actualizado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, string cedula)
        {
            var votante = await _votanteApiService.ObtenerPorCedulaAsync(cedula);
            if (votante == null) return NotFound();

            return View(votante);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var error = await _votanteApiService.EliminarVotanteAsync(id);
            TempData["Mensaje"] = string.IsNullOrEmpty(error) ? "Votante eliminado exitosamente." : error;
            return RedirectToAction(nameof(Index));
        }
    }
}
