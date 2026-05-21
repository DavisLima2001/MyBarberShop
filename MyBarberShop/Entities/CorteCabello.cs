using System.ComponentModel.DataAnnotations;

namespace MyBarberShop.Entities
{
    public class CorteCabello
    {
        public int CorteCabelloId { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description {  get; set; }
        public Decimal Price { get; set; }
        public string? ImageName { get; set; } = null;

        public Category? Category { get; set; }
    }
}
