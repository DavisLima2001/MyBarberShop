using Microsoft.AspNetCore.Mvc;
using MyBarberShop.Models;
using MyBarberShop.Services;
using System.Diagnostics;

namespace MyBarberShop.Controllers
{
    public class HomeController(
        CategoryService _categoryService,
        CorteCabelloService _corteCabelloService
        ) : Controller
    {
        public async Task<IActionResult>  Index()
        {
            var categories = await _categoryService.GetAllAsync();
            var cortescabellos = await _corteCabelloService.GetCatalogAsync();
            var catalog = new CatalogVM { Categories = categories, cortesCabellos = cortescabellos };
            return View(catalog);
        }

        public async Task<IActionResult> FilterByCategory(int id, string name)
        {
            var categories = await _categoryService.GetAllAsync();
            var cortescabellos = await _corteCabelloService.GetCatalogAsync(categoryid:id);

            var catalog = new CatalogVM { Categories = categories, cortesCabellos = cortescabellos, filterBy=name };
            return View("Index",catalog);
        }

        [HttpPost]
        public async Task<IActionResult> FilterBySearch(string value)
        {
            var categories = await _categoryService.GetAllAsync();
            var cortescabellos = await _corteCabelloService.GetCatalogAsync(search: value);

            var catalog = new CatalogVM { Categories = categories, cortesCabellos = cortescabellos, filterBy = $"Resultado para la Busqueda:{value}" };
            return View("Index",catalog);
        }

        public async Task<IActionResult> CorteDetail(int id)
        {
            var corteCabello = await _corteCabelloService.GetByIdAsync(id);
            return View(corteCabello);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
