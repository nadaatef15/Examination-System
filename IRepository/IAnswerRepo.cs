namespace Exam_System.IRepository
{
    public interface IAnswerRepo
    {
        Task<bool?> ValidateAnswer(int questionId, int? answerId);
        
    }
}
