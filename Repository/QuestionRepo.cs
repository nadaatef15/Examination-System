using Exam_System.Models;
using Exam_System.IRepository;


namespace Exam_System.Repository
{
    public class QuestionRepo:IQuestionRepo
    {
        ExaminationContext db;
        public QuestionRepo(ExaminationContext _db)
        {
            db = _db;
        }
        public List<Question> GetQuestions()
        {
            var AllQuestions = db.Questions.ToList();
            return AllQuestions;
        }
    }
}
