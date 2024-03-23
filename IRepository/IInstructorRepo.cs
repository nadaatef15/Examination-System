using Exam_System.Models;

namespace Exam_System.IRepository
{
    public interface IInstructorRepo
    {
         List<Course> getInstructorCourses(int userId);
         void AddMcqQuestion(int courseId, string questionType, string questionTitle, string ansA, string ansB, string ansC, string correctAns);

         void AddTfQuestion(int courseId, string questionType, string questionTitle, string correctAnswer);

        public Course getCourseToAddQuestion(int id);

        List<Instructor> getInstructors();
    }
}
