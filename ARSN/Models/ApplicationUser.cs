using Microsoft.AspNetCore.Identity;
using System;

namespace ARSN.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String Email{get;set;}
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Organisation { get; set; }
    }
}
