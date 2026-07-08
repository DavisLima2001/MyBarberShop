using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBarberShop.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyBarberShop.Models
{
    public class CorteCabelloVM
    {
        public int CorteCabelloId { get; set; }
        public CategoryVM Category { get; set; }
        [Required]
        public  List<SelectListItem> Categories { get; set; }
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Decimal Price { get; set; }
        
        public string? ImageName { get; set; } = null;

        public IFormFile? ImageFile { get; set; }
    }
}
