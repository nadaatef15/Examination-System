using Exam_System.Models;

namespace Exam_System.IRepository
{
    public interface IRepoInstructor
    {
        public List<Course> getCourseForInstructor(int id);
       
    }
}
