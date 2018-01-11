using System;
using System.ComponentModel.DataAnnotations;

namespace ARSN.ViewModels
{
    public class RegisterViewModel
    {
        [Required, EmailAddress, MaxLength(256), Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required, MinLength(6),MaxLength(50),DataType(DataType.Password), Display(Name = "Password")]
        public string Password { get; set; }
        [Required, MinLength(6), MaxLength(50), DataType(DataType.Password), Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage = "Lozinke se ne podudaraju.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Ime")]
        public string Name { get; set; }
        [Display(Name = "Prezime")]
        public string Surname { get; set; }
        [DataType(DataType.Date), Display(Name ="Datum Rođenja")]
        public DateTime BirthDate { get; set; }
        [Required, Display(Name ="Organizacija")]
        public string Organisation { get; set; }
        [DataType(DataType.PhoneNumber), Display(Name ="Broj telefona")]
        public string PhoneNumber { get; set; }
        [MaxLength(1), Display(Name ="Spol")]
        public string Gender { get; set; }
    }
}
