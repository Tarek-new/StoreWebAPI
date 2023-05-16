using System.ComponentModel.DataAnnotations;

namespace StoreWebAPI.Dtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string DisplayName { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*\W).{6,}$", ErrorMessage = "Password must have at least 1 uppercase letter, 1 lowercase letter, 1 digit, 1 non-alphanumeric character, and be a minimum of 6 characters long.")]
        public string Password { get; set; }
    }
}
