using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace ARSN.Models
{
	public class Round 
	{
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RoundID { get; set; }
        public string Name { get; set; }
        public Competition Competition { get; set; }
        public ICollection<Game> GameCollection { get; set; }

        #endregion Properties
    }
}