using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using MyBarberShop.Entities;
using MyBarberShop.Models;
using MyBarberShop.Repositories;
using System.Linq.Expressions;

namespace MyBarberShop.Services
{
    public class CorteCabelloService (
        GenericRepository<Category> _categoryRepository,
        GenericRepository<CorteCabello> _corteCabelloRepository, 
        IWebHostEnvironment _webHostEnvironment)
    {
        public async Task<IEnumerable<CorteCabelloVM>> GetAllAsync()
        {
            var CorteCabello = await _corteCabelloRepository.GetAllAsync(
                includes: new Expression<Func<CorteCabello, object>>[] { x => x.Category! }
                );

            var CorteCabelloVM = CorteCabello.Select(item => new CorteCabelloVM
            {
                CorteCabelloId = item.CorteCabelloId,
                Category = new CategoryVM
                {
                    CategoryId = item.Category!.CategoryId,
                    Name = item.Category!.Name,
                },
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                ImageName = item.ImageName,
            }).ToList();

            return CorteCabelloVM;
        }

        public async Task<CorteCabelloVM> GetByIdAsync(int id) { 
            var CorteCabello = await _corteCabelloRepository.GetByIdAsync(id);
            var categories = await _categoryRepository.GetAllAsync();

            var CorteCabelloVM = new CorteCabelloVM();
            if(CorteCabello != null)
            {
                CorteCabelloVM = new CorteCabelloVM
                {
                    CorteCabelloId = CorteCabello.CorteCabelloId,
                    Category = new CategoryVM
                    {
                        CategoryId = CorteCabello.Category!.CategoryId,
                        Name = CorteCabello.Category!.Name,
                    },
                    Name = CorteCabello.Name,
                    Description = CorteCabello.Description,
                    Price = CorteCabello.Price,
                    ImageName = CorteCabello.ImageName,

                };
            }
            
            CorteCabelloVM.Categories = categories.Select(item => new SelectListItem{
                Value = item.CategoryId.ToString(),
                Text = item.Name,

            }).ToList();
            return CorteCabelloVM;
        }

        public async Task AddAsync (CorteCabelloVM viewModel) {

            if (viewModel.ImageFile != null) 
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(viewModel.ImageFile.FileName);
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using( var fileStream = new FileStream(filePath, FileMode.Create))
                    await viewModel.ImageFile.CopyToAsync(fileStream);
                
                viewModel.ImageName = uniqueFileName;
                
            }

            var entity = new CorteCabello
            {
                CategoryId = viewModel.Category.CategoryId,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                ImageName = viewModel.ImageName,
            };

            await _corteCabelloRepository.AddAsync(entity);


        }
        public async Task EditAsync (CorteCabelloVM viewModel) {
            
            var cortecabello = await _corteCabelloRepository.GetByIdAsync(viewModel.CorteCabelloId);

            if (viewModel.ImageFile != null) {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(viewModel.ImageFile.FileName);
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                    await viewModel.ImageFile.CopyToAsync(fileStream);

                if (!cortecabello.ImageName.IsNullOrEmpty())
                {
                    var previousImage = cortecabello.ImageName; 
                    string deleteFilePath = Path.Combine(uploadFolder, previousImage);

                    if(File.Exists(deleteFilePath)) System.IO.File.Delete(deleteFilePath);

                }

                viewModel.ImageName = uniqueFileName;
            }else
            {
                viewModel.ImageName = cortecabello.ImageName;
            }

            cortecabello.CategoryId = viewModel.Category.CategoryId;
            cortecabello.Name = viewModel.Name;
            cortecabello.Description = viewModel.Description;
            cortecabello.Price = viewModel.Price;
            cortecabello.ImageName = viewModel.ImageName;

            await _corteCabelloRepository.EditAsync(cortecabello);
        }
        public async Task DeleteAsync (int id) {

            var cortecabello = await _corteCabelloRepository.GetByIdAsync(id);
            await _corteCabelloRepository.DeleteAsync(cortecabello!);

        }


        public async Task<IEnumerable<CorteCabelloVM>> GetCatalogAsync(int categoryid = 0, string search = "")
        {
            var conditions = new List<Expression<Func<CorteCabello, bool>>>
            {
                x => x.Price > 0
            };

            if (categoryid != 0) conditions.Add(X => X.CategoryId == categoryid);
            if (!string.IsNullOrEmpty(search)) conditions.Add(x => x.Name.Contains(search));

            var CorteCabello = await _corteCabelloRepository.GetAllAsync(conditions : conditions.ToArray());

            var CorteCabelloVM = CorteCabello.Select(item => new CorteCabelloVM
            {
                CorteCabelloId = item.CorteCabelloId,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                ImageName = item.ImageName,
            }).ToList();

            return CorteCabelloVM;
        }




    }
}
