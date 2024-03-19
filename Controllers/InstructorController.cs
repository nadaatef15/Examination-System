using Exam_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Exam_System.Controllers
{
    public class InstructorController : Controller
    {
        ExaminationContext db=new ExaminationContext();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddQuestions() //get
        {
        
            var userIdClaim = HttpContext.User.FindFirst("UserId");
            int userId =int.Parse (userIdClaim.Value);
            var instructorCourses = db.Instructors.FirstOrDefault(i => i.InstructorId == userId).Courses.ToList();
            ViewBag.UserId = userId;
            ViewBag.InstructorCourses=instructorCourses;
            return View();
        }
     
        public IActionResult AddMcqQuestions(int courseId,string questionType,string questionTitle,string ansA,string ansB,string ansC, string correctAns) //get
        {
            // add the question
            db.Database.ExecuteSqlRaw("EXEC AddQuestion @QuestionType,@QuestionTitle,@QuestionDegree,@CourseID",
            new SqlParameter("@QuestionType", questionType),
            new SqlParameter("@QuestionTitle", questionTitle),
             new SqlParameter("@QuestionDegree", 3),
            new SqlParameter("@CourseID", courseId));

            var Question=db.Questions.FirstOrDefault(q=>q.QuestionType==questionType && q.QuestionTitle==questionTitle&& q.CourseId==courseId);
            if (Question != null)
            {
                var QId = Question.QuestionId;
                db.Database.ExecuteSqlRaw("EXEC AddAnswer @AnswerNumber,@AnswerBody,@IsCorrect,@QuestionId",
                 new SqlParameter("@AnswerNumber", 1),
                 new SqlParameter("@AnswerBody", ansA),
                 new SqlParameter("@IsCorrect", false),
                 new SqlParameter("@QuestionId", QId));

                db.Database.ExecuteSqlRaw("EXEC AddAnswer @AnswerNumber,@AnswerBody,@IsCorrect,@QuestionId",
               new SqlParameter("@AnswerNumber", 2),
               new SqlParameter("@AnswerBody", ansB),
               new SqlParameter("@IsCorrect", false),
               new SqlParameter("@QuestionId", QId));

                db.Database.ExecuteSqlRaw("EXEC AddAnswer @AnswerNumber,@AnswerBody,@IsCorrect,@QuestionId",
               new SqlParameter("@AnswerNumber", 3),
               new SqlParameter("@AnswerBody", ansC),
               new SqlParameter("@IsCorrect", false),
               new SqlParameter("@QuestionId", QId));

                db.Database.ExecuteSqlRaw("EXEC AddAnswer @AnswerNumber,@AnswerBody,@IsCorrect,@QuestionId",
               new SqlParameter("@AnswerNumber", 4),
               new SqlParameter("@AnswerBody", correctAns),
               new SqlParameter("@IsCorrect", true),
               new SqlParameter("@QuestionId", QId));

            }

            return View();
        }

        public IActionResult AddTfQuestions(int courseId, string questionType, string questionTitle,string correctAnswer)
        {
            // add the question
            db.Database.ExecuteSqlRaw("EXEC AddQuestion @QuestionType,@QuestionTitle,@QuestionDegree,@CourseID",
            new SqlParameter("@QuestionType", questionType),
            new SqlParameter("@QuestionTitle", questionTitle),
             new SqlParameter("@QuestionDegree", 2),
            new SqlParameter("@CourseID", courseId));
            var Question = db.Questions.FirstOrDefault(q => q.QuestionType == questionType && q.QuestionTitle == questionTitle && q.CourseId == courseId);
            if (Question != null)
            {
                var QId = Question.QuestionId;
                string otherAns = correctAnswer=="True" ? "False" : "True";
                db.Database.ExecuteSqlRaw("EXEC AddAnswer @AnswerNumber,@AnswerBody,@IsCorrect,@QuestionId",
               new SqlParameter("@AnswerNumber", 1),
               new SqlParameter("@AnswerBody", correctAnswer),
               new SqlParameter("@IsCorrect", true),
               new SqlParameter("@QuestionId", QId));

                db.Database.ExecuteSqlRaw("EXEC AddAnswer @AnswerNumber,@AnswerBody,@IsCorrect,@QuestionId",
                new SqlParameter("@AnswerNumber", 2),
                new SqlParameter("@AnswerBody", otherAns),
                new SqlParameter("@IsCorrect", false),
                new SqlParameter("@QuestionId", QId));
            }
                return View();
        }
    }
}
