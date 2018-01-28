using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARSN.Models
{
    public class Competition : IValidatableObject
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CompetitionID { get; set; }
        [Required, StringLength(30, MinimumLength = 3, ErrorMessage = "Minimalna dužina mora biti 3 znaka"), Display(Name = "Ime natjecanja")]
        public string Name { get; set; }
        [Required, StringLength(30, MinimumLength = 3, ErrorMessage = "Minimalna dužina mora biti 3 znaka"), Display(Name = "Sport")]
        public string SportType { get; set; }
        [DataType(DataType.Date), Display(Name = "Početak natjecanja")]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2100", ErrorMessage = "Datum je izvan granica")]
        public DateTime CompetitionBegin { get; set; }
        [DataType(DataType.Date), Display(Name = "Kraj natjecanja")]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2100", ErrorMessage = "Datum je izvan granica")]
        public DateTime CompetitionEnd { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Round> RoundCollection { get; set; }

                public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
                {
                    if (CompetitionEnd < CompetitionBegin)
                    {
                        yield return
                          new ValidationResult(errorMessage: "Datum početka mora biti prije Datuma završetka natjecanja",
                                               memberNames: new[] { "CompetitionEnd" });
                    }
                }

        #endregion Properties
    }
}
