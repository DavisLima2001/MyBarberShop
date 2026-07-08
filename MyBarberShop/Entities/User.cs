using System.ComponentModel.DataAnnotations;

namespace MyBarberShop.Entities
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]

        public string password { get; set; }
        [Required]

        public string type { get; set; }
    }
}
