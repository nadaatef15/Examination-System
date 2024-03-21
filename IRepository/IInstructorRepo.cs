using Exam_System.Models;

namespace Exam_System.IRepository
{

    public interface IInstructorRepo
    {
        Task<List<GetInstructorDataResult>> GetAll();
        public Instructor GetById(int id);
        Task Add(Instructor instructor);
        Task Edit(int id, Instructor instructor);
        Task Delete(int id);
        bool IsEmailExist(string email);

       Task< List<GetTrackByInstructorIdResult>> GetTracks(Instructor id);

      Task GetTracksForInstructor(int id);
    }
}
