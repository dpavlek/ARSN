using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARSN.Models
{
    public class Organizer
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrganizerID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Organisation { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public bool Verified { get; set; }
        public ICollection<Competition> Competitions { get; set; }

        #endregion Properties

        #region Constructors

        public Organizer() { }
        public Organizer(string name, string surname, string email, DateTime birthDate, string organisation, string phoneNumber, string gender, string password)
        {
            Name = name;
            Surname = surname;
            Email = email;
            BirthDate = birthDate;
            Organisation = organisation;
            PhoneNumber = phoneNumber;
            Gender = gender;
            Password = password;
        }

        #endregion Constructors
    }

}
