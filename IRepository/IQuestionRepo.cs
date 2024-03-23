using Exam_System.Models;

namespace Exam_System.IRepository
{
    public interface IQuestionRepo
    {
        public List<Question> GetQuestions();
    }
}
