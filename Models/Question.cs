﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Exam_System.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public string QuestionType { get; set; }

    public string QuestionTitle { get; set; }

    public int? QuestionDegree { get; set; }

    public int? CourseId { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Course Course { get; set; }

    public virtual ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}