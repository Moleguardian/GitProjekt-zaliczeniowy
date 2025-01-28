using System.ComponentModel.DataAnnotations;

namespace Projekt_Zaliczeniowy.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła muszą się zgadzać.")]
        public string ConfirmPassword { get; set; }
    }
}
