using Exam_System.Models;
using Exam_System.IRepository;
namespace Exam_System.Repository
{
    public class RepoStudentAnswer:IRepoStudentAnswer
    {

        ExaminationContext db;
        public RepoStudentAnswer(ExaminationContext _db)
        {
            db = _db;
        }
        public void Add(StudentAnswer answer)
        {
            db.StudentAnswers.Add(answer);
        }
    }
}
