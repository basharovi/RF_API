using RapidFireLib.Lib.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessDomain.Aggregates.Student
{
    public class StudentInfoView:IModel
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string DateOfBirth { get; set; }
        public string GradeName { get; set; }
        public int? RollNumber { get; set; }
        public int? Sex { get; set; }
        public int? IsActive { get; set; }
    }
}
