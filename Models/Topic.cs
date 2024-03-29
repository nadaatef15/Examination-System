// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exam_System.Models;

public partial class Topic
{
    public int TopicId { get; set; }

    [Required(ErrorMessage = "Topic name is required")]
    [StringLength(100, ErrorMessage = "Topic name cannot exceed 100 characters")]

    public string TopicName { get; set; }

    public int? CourseId { get; set; }

    public virtual Course Course { get; set; }
}
