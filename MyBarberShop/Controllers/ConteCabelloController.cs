using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBarberShop.Models;
using MyBarberShop.Services;

namespace MyBarberShop.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ConteCabelloController(CorteCabelloService _corteCabelloService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var cortescabello = await _corteCabelloService.GetAllAsync();
            return View(cortescabello);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            var corteCabelloVM = await _corteCabelloService.GetByIdAsync(id);
            return View(corteCabelloVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(CorteCabelloVM entityVM)
        {
            ViewBag.message = null;

            ModelState.Remove("Categories");
            ModelState.Remove("Category.Name");
            if (!ModelState.IsValid) return View(entityVM);
            

            if (entityVM.CorteCabelloId == 0)
            {
                await _corteCabelloService.AddAsync(entityVM);
                ModelState.Clear();
                entityVM = new CorteCabelloVM();
                ViewBag.message = "Corte de Cabello Creado";

            }
            else
            {
                await _corteCabelloService.EditAsync(entityVM);
                ViewBag.message = "Corte de Cabello Editado";
            }


            return View(entityVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _corteCabelloService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
