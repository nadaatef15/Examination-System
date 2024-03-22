using Exam_System.IRepository;
using Exam_System.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Repository
{
    public class RepoExam :IRepoExam
    {
        ExaminationContext db;
        public RepoExam(ExaminationContext _db)
        {
            db= _db;
        }
        public List<Exam> getAll(int instructorId, int CourseId)
        {
           return db.Exams.Where(a=>a.InstructorId==instructorId && a.CourseId==CourseId).ToList();
        }
        public void InsertExam(Exam exam, int numTrueFalseQuestions, int numMCQuestions)
        {
            try
            {
                SqlParameter examDateParam = new SqlParameter("@ExamDate", exam.ExamDate);
                SqlParameter startTimeParam = new SqlParameter("@StartTime", exam.StartTime);
                SqlParameter endTimeParam = new SqlParameter("@EndTime", exam.EndTime);
                SqlParameter courseIdParam = new SqlParameter("@CourseId", exam.CourseId);
                SqlParameter trackIdParam = new SqlParameter("@TrackId", exam.TrackId);
                SqlParameter instructorIdParam = new SqlParameter("@InstructorId", exam.InstructorId);
                SqlParameter fullMarkParam = new SqlParameter("@FullMark", exam.FullMark);
                SqlParameter numTrueFalseQuestionsParam = new SqlParameter("@NumTrueFalseQuestions", numTrueFalseQuestions);
                SqlParameter numMCQuestionsParam = new SqlParameter("@NumMCQuestions", numMCQuestions);

                db.Database.ExecuteSqlRaw("EXEC InsertExam @ExamDate, @StartTime, @EndTime, @CourseId, @TrackId, @InstructorId, @FullMark, @NumTrueFalseQuestions, @NumMCQuestions",
                    examDateParam, startTimeParam, endTimeParam, courseIdParam, trackIdParam, instructorIdParam, fullMarkParam, numTrueFalseQuestionsParam, numMCQuestionsParam);
            }
            catch (Exception ex)
            {
                throw new Exception("Error calling stored procedure: " + ex.Message);
            }
        }
        public bool HasExamConflict(int? trackId, int? courseId, int? instructorId, DateOnly? examDate, TimeOnly? startTime, TimeOnly? endTime)
        {
            bool hasConflict = db.Exams.Any(e =>
                e.TrackId == trackId &&
                e.CourseId == courseId &&
                e.InstructorId == instructorId &&
                e.ExamDate == examDate &&
                ((e.StartTime <= startTime && e.EndTime > startTime) || (e.StartTime < endTime && e.EndTime >= endTime) || (e.StartTime >= startTime && e.EndTime <= endTime)));

            return hasConflict;
        }
        public void Delete(int ExamID)
        {
           
            var exam = db.Exams.FirstOrDefault(a => a.ExamId == ExamID);
            if (exam != null)
            {
                exam.Questions.Clear();

                db.Exams.Remove(exam);
                db.SaveChanges();
            }
           
        }
       
    }
}
