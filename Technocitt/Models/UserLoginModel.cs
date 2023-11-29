using System.ComponentModel.DataAnnotations;

namespace Technocitt.Models
{
    public class UserLoginModel
    {
        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
