using Exam_System.Models;

namespace Exam_System.IRepository
{

    public interface IInstructorRepo
    {
        Task<List<GetInstructorDataResult>> GetAll();
        Task Add(Instructor instructor);
        //void Edit(int id, Instructor instructor);
        void Delete(int id);
        bool IsEmailExist(string email);
    }
}
