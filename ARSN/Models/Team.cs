using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ARSN.Models
{
    public class Team
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TeamID { get; set; }
        [Required, StringLength(30, MinimumLength = 3), Display(Name = "Ime tima")]
        public string Name { get; set; }
        [Required, StringLength(30, MinimumLength = 3), Display(Name = "Ime organizacije")]
        public string Organisation { get; set; }
        [EmailAddress, MaxLength(256), Display(Name = "Email Adresa")]
        public string Email { get; set; }
        [StringLength(30, MinimumLength = 3), Display(Name = "Ime trenera")]
        public string TrainerName { get; set; }

        #endregion Properties
    }
}
