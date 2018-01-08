using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARSN.Models
{
    public class Game
    {
        public string GameID { get; set; }
        public string Type { get; set; }
        public string HomeTeamID { get; set; }
        public string AwayTeamID { get; set; }
        public string HomeResult { get; set; }
        public string AwayResult { get; set; }
        public string Winner { get; set; }
        public Competition CompetitionObject { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

    }
}
