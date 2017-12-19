using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSN.Models
{
    public class Competition
    {
        public string CompetitionID { get; set; }
        public string Name { get; set; }
        public string OrganizerID { get; set; }
        public string SportType { get; set; }
        public DateTime CompetitionBegin { get; set; }
        public DateTime CompetitionEnd { get; set; }
        public List<Game> GameList { get; set; }
    }
}
