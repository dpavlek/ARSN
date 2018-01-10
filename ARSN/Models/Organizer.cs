using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARSN.Models
{
    public class Organizer:IdentityUser
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrganizerID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required, EmailAddress, MaxLength(256), Display(Name = "Email")]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Organisation { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [MaxLength(1)]
        public string Gender { get; set; }
        [Required, MinLength(6), MaxLength(50), DataType(DataType.Password), Display(Name = "Lozinka")]
        public string Password { get; set; }
        public bool Verified { get; set; }
        public ICollection<Competition> Competitions { get; set; }

        #endregion Properties

        #region Constructors

        public Organizer() { }
        public Organizer(string name, string surname, string email, DateTime birthDate, string organisation, string phoneNumber, string gender, bool verified)
        {
            Name = name;
            Surname = surname;
            Email = email;
            BirthDate = birthDate;
            Organisation = organisation;
            PhoneNumber = phoneNumber;
            Gender = gender;
            Verified = verified;
        }

        #endregion Constructors
    }

}
