using Exam_System.IRepository;
using Exam_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Repository
{
    public class AnswerRepo : IAnswerRepo
    {
        ExaminationContext db;
        public AnswerRepo(ExaminationContext _db)
        {
            db = _db;
        }

        public async Task<bool?> ValidateAnswer(int questionId, int? answerId)
        {
            if(answerId == null) 
                return false;

            return await db.Answers.Where(x => x.QuestionId == questionId && x.AnswerId == answerId).Select(x => x.IsCorrect).FirstOrDefaultAsync();
        }
    }
}
