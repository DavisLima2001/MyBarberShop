using System.ComponentModel.DataAnnotations;

namespace MyBarberShop.Models
{
    public class UserVm
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

        [Required]

        public string Repeatpassword { get; set; }
    }
}
