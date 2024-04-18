// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exam_System.Models;

public partial class Course
{
    public int CourseId { get; set; }



    [Required(ErrorMessage = "Course name is required")]
    [StringLength(100, ErrorMessage = "Course name cannot exceed 100 characters")]

    public string CourseName { get; set; }

    [Required(ErrorMessage = "Pass degree is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Pass degree must be a positive number")]


    public int? PassDegree { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();

    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
