using System;
using System.Collections.Generic;

namespace Exam_System.Models;

public partial class Track
{
    public int TrackId { get; set; }

    public string? TrackName { get; set; }

    public int? SupervisorId { get; set; }

    public int? Capacity { get; set; }

    public int? BranchId { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual Instructor? Supervisor { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
