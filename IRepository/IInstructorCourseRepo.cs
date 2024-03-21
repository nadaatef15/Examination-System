using Exam_System.Models;

namespace Exam_System.IRepository
{
    public interface IInstructorCourseRepo
    {
        public List<InstructorCourse> getByCourseId(int courseId);

        Task<List<GetAllInstructorsCoursesResult>> getAll();
        public List<InstructorCourse> getByinstructotId(int instructorId);

        public List<InstructorCourse> getByCourseByInstructor(int instructorid);
        public void Add(InstructorCourse instructorCourse);
        public void Delete(int courseId, int instructorId);
    }
}
