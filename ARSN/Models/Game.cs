using System;
using System.Collections.Generic;
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
    }
}
