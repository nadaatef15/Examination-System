using System;
using System.Collections.Generic;

namespace Exam_System.Models;

public partial class Branch
{
    public int BranchId { get; set; }

    public string? BranchName { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();

    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
}
