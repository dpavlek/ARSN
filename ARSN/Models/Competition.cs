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
        public string Name { get; set; }
        public string SportType { get; set; }
        public DateTime CompetitionBegin { get; set; }
        public DateTime CompetitionEnd { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Round> RoundCollection { get; set; }

        #endregion Properties
    }
}
