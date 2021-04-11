using RapidFireLib.Lib.Core;
using System.ComponentModel.DataAnnotations;

namespace BusinessDomain.Aggregates
{
    public class GradeInfo : IModel
    {
        [Key]
        public int GradeId { get; set; }
        [Required]
        public string GradeName { get; set; }
        public string GradeNameBn { get; set; }
    }
}
