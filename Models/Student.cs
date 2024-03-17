using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exam_System.Models;

public partial class Student
{
    public int StudentId { get; set; }
    [StringLength(25, ErrorMessage = "First name must be between 2 and 50 characters", MinimumLength = 2)]
    public string? StudentFname { get; set; }
    [StringLength(25, ErrorMessage = "Last name must be between 2 and 50 characters", MinimumLength = 2)]
    public string? StudentLname { get; set; }
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]

    public string? StudentEmail { get; set; }

    public string? StudentGender { get; set; }

    public string? StudentPassword { get; set; }

    public int? TrackId { get; set; }

    public virtual ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    public virtual Track? Track { get; set; }
}
