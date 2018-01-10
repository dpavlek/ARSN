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
        public string HomeResult { get; set; }
        public string AwayResult { get; set; }
        public string Winner { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        #endregion Properties

    }
}
