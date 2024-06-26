// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Exam_System.Models;

public partial class Answer
{
    public int AnswerId { get; set; }

    public int? AnswerNumber { get; set; }

    public string AnswerBody { get; set; }

    public bool? IsCorrect { get; set; }

    public int? QuestionId { get; set; }

    public virtual Question? Question { get; set; }

    public virtual ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
}
