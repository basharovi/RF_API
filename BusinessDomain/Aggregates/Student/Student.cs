using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessDomain.Aggregates
{
    public class StudentInfo : BaseModel
    {
        [Key]
        public string StudentId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string StudentName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? GradeId { get; set; }
        public int? RollNumber { get; set; }
        public int? Sex { get; set; }
        public int? IsActive { get; set; }
    }
}
