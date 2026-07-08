using System.ComponentModel.DataAnnotations;

namespace MyBarberShop.Models
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
