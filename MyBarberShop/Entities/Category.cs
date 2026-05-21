using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MyBarberShop.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        public string? Name { get; set; }

        public Collection<CorteCabello> CortesCabellos { get; set; }


    }
}
