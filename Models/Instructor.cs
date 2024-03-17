using System;
using System.Collections.Generic;

namespace Exam_System.Models;

public partial class Instructor
{
    public int InstructorId { get; set; }

    public string? InstructorFname { get; set; }

    public string? InstructorLname { get; set; }

    public decimal? InstructorSalary { get; set; }

    public string? InstructorEmail { get; set; }

    public string? InstructorPassword { get; set; }

    public string? InstructorGender { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
