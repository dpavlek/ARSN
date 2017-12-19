using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSN.Models
{
    public class Organizer
    {
        public string OrganizerID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Organisation { get; set; }
        public string PhoneNumber { get; set; }
        public char Gender { get; set; }
    }

}
