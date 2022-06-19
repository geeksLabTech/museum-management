

using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.ViewModels {
    public class LoginViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
