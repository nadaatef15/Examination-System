using Exam_System.Models;

namespace Exam_System.IRepository
{

    public interface IInstructorAdminRepo
    {
        Task<List<GetInstructorDataResult>> GetAll();
        public Instructor GetById(int id);
        Task Add(Instructor instructor);
        Task Edit(int id, Instructor instructor);
        Task Delete(int id);
        bool IsEmailExist(string email);

        public List<Track> GetTracksByInstructorId(int instructotId);

    }
}
