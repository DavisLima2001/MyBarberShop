using System.ComponentModel.DataAnnotations;

namespace MyBarberShop.Models
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }
        
        public string? Name { get; set; }
    }
}
