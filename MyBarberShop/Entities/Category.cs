using System.ComponentModel.DataAnnotations;

namespace MyBarberShop.Entities
{
    public class Category
    {
        public int Idcategory { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
