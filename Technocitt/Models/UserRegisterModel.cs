using System.ComponentModel.DataAnnotations;

namespace Technocitt.Models
{
    public class UserRegisterModel
    {

        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        [Required(ErrorMessage = "Please enter a mobile number.")]
        [RegularExpression(@"^\+?\d+$", ErrorMessage = "Mobile number must contain only digits.")]
        public required string Mobile { get; set; }
        public required string Email { get; set; }
        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 16 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$", ErrorMessage = "Password must contain at least one uppercase letter, one digit, and one special character.")]
        public required string Password { get; set; } 
        [Required(ErrorMessage = "Please enter a confirm password.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public required string ConfirmPassword { get; set; }

    }
}
