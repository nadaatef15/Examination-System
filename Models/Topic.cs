using System;
using System.Collections.Generic;

namespace Exam_System.Models;

public partial class Topic
{
    public int TopicId { get; set; }

    public string? TopicName { get; set; }

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }
}
