using Microsoft.AspNetCore.Mvc;
using MyBarberShop.Models;
using MyBarberShop.Services;

namespace MyBarberShop.Controllers
{
    public class CategoryController(CategoryService _categoryService) : Controller
    {
        public async Task<IActionResult> Index()
        {
           var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            var categoryVM = await _categoryService.GetByIdAsync(id);
            return View(categoryVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(CategoryVM entityVM)
        {
            ViewBag.message = null;
            if (!ModelState.IsValid) return View(entityVM);


            if (entityVM.CategoryId == 0) 
            {
                await _categoryService.AddAsync(entityVM);
                ModelState.Clear();
                entityVM = new CategoryVM();
                ViewBag.message = "Categoria Creada";

            }
            else
            {
                await _categoryService.EditAsync(entityVM);
                ViewBag.message = "Categoria Editada";
            }


            return View(entityVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction("Index");   
        }

    }
}
