using Exam_System.Models;

namespace Exam_System.IRepository
{
    public interface IInstructorCourseRepo
    {
        List<Course> getInstructorCoursesById(int courseId);
        Task Add(int courseId, int instructorId);
        Task Delete(int courseId, int instructorId);
        Task<List<Course>> ListCource();
    }
}
