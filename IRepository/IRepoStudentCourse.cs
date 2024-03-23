using Exam_System.Models;

namespace Exam_System.IRepository
{
    public interface IRepoStudentCourse
    {
        public List<StudentCourse> getByCourseId(int courseId);
        public List<StudentCourse> getByStudentId(int studentId);
        public List<StudentCourse> getAll();
        public StudentCourse getByIds(int courseId, int studentId);
        public void Add(StudentCourse studentCourse);
        public void Delete(int courseId, int StudentId);
        public void UpdateGrade(StudentCourse obj, int Grade);
    }
}
