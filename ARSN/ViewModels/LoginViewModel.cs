using System.ComponentModel.DataAnnotations;

namespace ARSN.ViewModels
{
    public class LoginViewModel
    {
        [Required, EmailAddress(ErrorMessage = "Nije unesena validna Email adresa"), MaxLength(256), Display(Name = "Email")]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Lozinka")]
        public string Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
