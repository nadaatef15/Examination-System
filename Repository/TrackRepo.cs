using Exam_System.IRepository;
using Exam_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Exam_System.Repository
{
    public class TrackRepo : TrackIRepo
    {
        ExaminationContext db;

        public TrackRepo(ExaminationContext _db)
        {
            db = _db;
        }
        public void Add(Track track)
        {
            db.Database.ExecuteSqlRaw("EXECUTE dbo.InsertTrack {0}, {1}, {2}, {3}",
                                       track.TrackName,
                                       track.SupervisorId,
                                       track.Capacity
                                       );
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Database.ExecuteSqlRaw("EXECUTE dbo.DeleteTrack {0}", id);
            db.SaveChanges();
        }

        public void Edit(int id, Track track)
        {
            db.Database.ExecuteSqlRaw("EXECUTE dbo.UpdateTrack {0}, {1}, {2}, {3}, {4}",
                                       id,
                                       track.TrackName,
                                       track.SupervisorId,
                                       track.Capacity);
            db.SaveChanges();
        }
        public bool EmailIsExist(string email)
        {
            return db.Tracks.Any(a => a.TrackName == email);
        }

        public List<Track> getAll()
        {
            return db.Tracks.FromSqlRaw("EXECUTE dbo.SelectAllTracks").ToList();
        }

        public Track getById(int id)
        {
            return db.Tracks.FromSqlRaw("EXECUTE dbo.SelectTrackById {0}", id).AsEnumerable().FirstOrDefault();
        }




    }
}
