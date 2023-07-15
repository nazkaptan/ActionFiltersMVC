using System.ComponentModel.DataAnnotations;

namespace ActionFiltersMVC.Models
{
    public class User
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string UserRole { get; set; }
    }
}
