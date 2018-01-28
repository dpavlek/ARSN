using System;
using System.ComponentModel.DataAnnotations;

namespace ARSN.ViewModels
{
    public class RegisterViewModel
    {
        [Required, EmailAddress(ErrorMessage = "Nije unesena validna Email adresa"), MaxLength(256), Display(Name = "Email")]
        public string Email { get; set; }
        [Required, MinLength(6),MaxLength(50),DataType(DataType.Password), Display(Name = "Lozinka")]
        public string Password { get; set; }
        [Required, MinLength(6), MaxLength(50), DataType(DataType.Password), Display(Name = "Potvrdi lozinku")]
        [Compare("Password",ErrorMessage = "Lozinke se ne podudaraju.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Ime")]
        public string Name { get; set; }
        [Display(Name = "Prezime")]
        public string Surname { get; set; }
        [DataType(DataType.Date), Display(Name ="Datum Rođenja")]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2018", ErrorMessage = "Datum je izvan granica")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Polje Organizacija je obavezno"), Display(Name ="Organizacija")]
        public string Organisation { get; set; }
        [DataType(DataType.PhoneNumber), Display(Name ="Broj telefona")]
        public string PhoneNumber { get; set; }
        [MaxLength(1, ErrorMessage = "Spol se sastoji od samo jednog znaka"), Display(Name ="Spol")]
        public string Gender { get; set; }
    }
}
