using Exam_System.IRepository;
using Exam_System.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Repository
{
    public class RepoCourse : IRepoCourse
    {
        ExaminationContext db;
        public RepoCourse(ExaminationContext _db)
        {
            db = _db;
        }

        public void AddCourse(string name, int passDegree, string topic, List<int> selectedTracks)
        {
            db.Database.ExecuteSqlRaw("EXEC AddCourse @CourseName, @PassDegree",
               new SqlParameter("@CourseName", name),
               new SqlParameter("@PassDegree", passDegree));


            var courseId = db.Courses.FirstOrDefault(c => c.CourseName == name && c.PassDegree == passDegree)?.CourseId;

            if (courseId != null)
            {

                db.Database.ExecuteSqlRaw("EXEC AddTopic @TopicName, @CourseId",
                    new SqlParameter("@TopicName", topic),
                    new SqlParameter("@CourseId", courseId));

                foreach (var trackId in selectedTracks)
                {
                    db.Database.ExecuteSqlRaw("EXEC AddCourseTrack @TrackId,@CourseId",
                          new SqlParameter("@TrackId", trackId),
                    new SqlParameter("@CourseId", courseId));

                }
            }
        }

        public List<Course> getAll()
        {
            return db.Courses.FromSqlRaw("EXECUTE dbo.GetCourseData").ToList();
        }

        public List<Track> GetAllTracks()
        {
            return db.Tracks.FromSqlRaw("Exec dbo.SelectAllTracks").ToList();
        }

        public Course GetCourseById(int courseId)
        {
            return db.Courses.FirstOrDefault(c => c.CourseId == courseId);
        }

        public void RemoveCourse(int courseId)
        {

            bool isAssigned = db.StudentCourses.Any(sc => sc.CourseId == courseId);
            if (isAssigned)
            {

                throw new Exception("Ooops,Course cannot be deleted as it is assigned to one or more students.");
            }
            else
            {

                db.Database.ExecuteSqlRaw("EXEC DeleteCourse @CourseId", new SqlParameter("@CourseId", courseId));
            }
        }

        public void UpdateCourse(int courseId, string name, int passDegree, string topic, List<int> selectedTracks)
        {
            db.Database.ExecuteSqlRaw("EXEC UpdateCourse @CourseId, @CourseName, @PassDegree",
               new SqlParameter("@CourseId", courseId),
             new SqlParameter("@CourseName", name),
             new SqlParameter("@PassDegree", passDegree));




            if (courseId != 0)
            {
                int topicId = db.Topics.Where(t => t.CourseId == courseId).Select(s => s.TopicId).FirstOrDefault();
                db.Database.ExecuteSqlRaw("EXEC UpdateTopic @topicId ,@TopicName, @CourseId",
                      new SqlParameter("@topicId", topicId),
                    new SqlParameter("@TopicName", topic),
                    new SqlParameter("@CourseId", courseId));
            }

            foreach (var trackId in selectedTracks)
            {
                bool isAssignedToTrack = db.Courses.Any(ct => ct.CourseId == courseId && ct.Tracks.Any(t => t.TrackId == trackId));

                if (!isAssignedToTrack)
                {

                    db.Database.ExecuteSqlRaw("EXEC AddCourseTrack @TrackId,@CourseId",
                        new SqlParameter("@TrackId", trackId),
                        new SqlParameter("@CourseId", courseId));
                }





            }
        }
    }
}