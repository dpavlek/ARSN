using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARSN.Models
{
    public class Competition
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CompetitionID { get; set; }
        [Required, StringLength(30, MinimumLength = 3), Display(Name = "Ime natjecanja")]
        public string Name { get; set; }
        [Required, StringLength(30, MinimumLength = 3), Display(Name = "Sport")]
        public string SportType { get; set; }
        [DataType(DataType.Date), Display(Name = "Početak natjecanja")]
        public DateTime CompetitionBegin { get; set; }
        [DataType(DataType.Date), Display(Name = "Kraj natjecanja")]
        public DateTime CompetitionEnd { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Round> RoundCollection { get; set; }

        #endregion Properties
    }
}
