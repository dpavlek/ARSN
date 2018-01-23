using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ARSN.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Organisation { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public bool Verified { get; set; }
        public ICollection<Competition> Competitions { get; set; }

        public static implicit operator ApplicationUser(UserManager<ApplicationUser> v)
        {
            throw new NotImplementedException();
        }
    }
}
