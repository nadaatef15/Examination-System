using Exam_System.Models;

namespace Exam_System.IRepository
{
    public interface ICourseRepo
    {
        public List<Course> getAll();

        Course GetCourseById(int courseId);
        List<Track> GetAllTracks();
        void AddCourse(string name, int passDegree, string topic, List<int> selectedTracks);
        void UpdateCourse(int courseId, string name, int passDegree, string topic, List<int> selectedTracks);

        public void RemoveCourse(int courseId);
    }
}
