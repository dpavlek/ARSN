using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARSN.Models
{
    public class Game //: IValidatableObject
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GameID { get; set; }
        [Display(Name = "Vrsta sporta")]
        public string Type { get; set; }
        [Range(typeof(int), "0", "200", ErrorMessage = "Rezultat mora biti između 0 i 200"), Display(Name = "Rezultat domaćina")]
        public string HomeResult { get; set; }
        [Range(typeof(int), "0", "200", ErrorMessage = "Rezultat mora biti između 0 i 200"), Display(Name = "Rezultat u gostima")]
        public string AwayResult { get; set; }
        [Display(Name = "Pobjednik")]
        public string Winner { get; set; }
        [Display(Name = "Kolo")]
        public Round Round { get; set; }
        [Display(Name = "Domaćin")]
        public Team HomeTeam { get; set; }
        [Display(Name = "Gost")]
        public Team AwayTeam { get; set; }
        public bool Empty
        {
            get
            {
                return (AwayTeam==null);
            }
        }
        /*
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HomeResult == AwayResult)
            {
                yield return
                  new ValidationResult(errorMessage: "Rezultat mora biti različit!",
                                       memberNames: new[] { "AwayResult" });
            }
        }*/
        #endregion Properties

    }
}
