using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARSN.Models
{
    public class Game
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GameID { get; set; }
        public string Type { get; set; }
        [Range(typeof(int), "0", "100"), Display(Name = "Rezultat domaćina")]
        public string HomeResult { get; set; }
        [Range(typeof(int), "0", "100"), Display(Name = "Rezultat u gostima")]
        public string AwayResult { get; set; }
        public string Winner { get; set; }
        public Round Round { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        #endregion Properties

    }
}
