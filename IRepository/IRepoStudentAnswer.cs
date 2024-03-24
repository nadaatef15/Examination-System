using Exam_System.Models;

namespace Exam_System.IRepository
{
    public interface IRepoStudentAnswer
    {
        public void Add(StudentAnswer answer);
        List<StudentAnswer> GetStudentAnswers(int studentId, int examId);
    }
}
