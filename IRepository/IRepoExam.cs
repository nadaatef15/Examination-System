using Exam_System.Models;

namespace Exam_System.IRepository
{
    public interface IRepoExam
    {
        public List<Exam> getAll(int instructorId ,int CourseId);
        public void InsertExam(Exam exam, int numTrueFalseQuestions, int numMCQuestions);
        public bool HasExamConflict(int? trackId, int? courseId, int? instructorId, DateOnly? examDate, TimeOnly? startTime, TimeOnly? endTime);
        public void Delete(int ExamID);
        public Exam GetExambyIds(int examId, int courseId, int instructorId);
        public void Save();
    }
}
