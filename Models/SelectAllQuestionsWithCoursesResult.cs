﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam_System.Models
{
    public partial class SelectAllQuestionsWithCoursesResult
    {
        public int QuestionId { get; set; }
        public string QuestionType { get; set; }
        public string QuestionTitle { get; set; }
        public int? QuestionDegree { get; set; }
        public int? CourseId { get; set; }
        public string CourseName { get; set; }
    }
}
