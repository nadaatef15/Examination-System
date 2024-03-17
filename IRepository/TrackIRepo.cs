using Exam_System.Models;

namespace Exam_System.IRepository
{
    public interface TrackIRepo
    {
        public List<Track> getAll();
        public Track getById(int id);
        public void Add(Track track);
        public void Edit(int id, Track track);
        public void Delete(int id);
        public bool EmailIsExist(string email);
    }
}
