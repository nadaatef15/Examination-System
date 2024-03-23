using Exam_System.IRepository;
using Exam_System.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Repository
{
    public class InstructorRepo:IInstructorRepo
    {
        ExaminationContext db;
        public InstructorRepo(ExaminationContext _db)
        {
            db = _db;
        }



        public void AddMcqQuestion(int courseId, string questionType, string questionTitle, string ansA, string ansB, string ansC, string correctAns)
        {
            try
            {
                db.Database.ExecuteSqlRaw("EXEC AddQuestion @QuestionType,@QuestionTitle,@QuestionDegree,@CourseID",
                 new SqlParameter("@QuestionType", questionType),
                 new SqlParameter("@QuestionTitle", questionTitle),
                 new SqlParameter("@QuestionDegree", 3),
                 new SqlParameter("@CourseID", courseId));

                var Question = db.Questions.FirstOrDefault(q => q.QuestionType == questionType && q.QuestionTitle == questionTitle && q.CourseId == courseId);
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
            }
            catch (Exception ex)
            {
               
                Console.WriteLine(ex.Message);
            }
        }

        public void AddTfQuestion(int courseId, string questionType, string questionTitle, string correctAnswer)
        {
            try
            {
                db.Database.ExecuteSqlRaw("EXEC AddQuestion @QuestionType,@QuestionTitle,@QuestionDegree,@CourseID",
               new SqlParameter("@QuestionType", questionType),
               new SqlParameter("@QuestionTitle", questionTitle),
                new SqlParameter("@QuestionDegree", 2),
               new SqlParameter("@CourseID", courseId));
                var Question = db.Questions.FirstOrDefault(q => q.QuestionType == questionType && q.QuestionTitle == questionTitle && q.CourseId == courseId);
                if (Question != null)
                {
                    var QId = Question.QuestionId;
                    string otherAns = correctAnswer == "True" ? "False" : "True";
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
            }
            catch (Exception ex)
            {
        
                Console.WriteLine(ex.Message);
            }
        }

        public List<Course> getInstructorCourses(int userId)
        {
            return db.Instructors.FirstOrDefault(i => i.InstructorId == userId).Courses.ToList();
        }

        public Course getCourseToAddQuestion(int id)
        {
            return db.Courses.FirstOrDefault(i => i.CourseId == id);    
        }
    }
}
