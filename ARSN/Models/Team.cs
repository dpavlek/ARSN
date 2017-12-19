using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ARSN.Models
{
    public class Team
    {
        public string TeamID { get; set; }
        public string Name { get; set; }
        public string Organisation { get; set; }
        public string Email { get; set; }
        public string TrainerName { get; set; }
    
    }
}
