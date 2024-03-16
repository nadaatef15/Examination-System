using Exam_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Controllers
{
    public class AdminController : Controller
    {
        ExaminationContext db = new ExaminationContext();
        public IActionResult Show()
        {
            return View();
        }
        public IActionResult Reports()
        {
        
            return PartialView("_Reports");
        }

        public IActionResult Students()
        {
           
            return PartialView("_Students");
        }

        public IActionResult Instructors()
        {
         
            return PartialView("_Instructors");
        }

      
        public IActionResult AllCourses()
        {
           
            var courses = db.Courses
                       .Include(c => c.Topics) 
                       .Include(t=>t.Tracks)
                       .ToList();

    
            return View("Course/AllCourses",courses);
          
        }
        // add course

        [HttpGet]
        public IActionResult AddCourse()
        {
            var tracks = db.Tracks.Distinct().ToList();
            return View("Course/AddCourse",tracks);
        }
        [HttpPost]
        public IActionResult AddCourse(string name, int passDegree, string topic, List<int> selectedTracks)
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
                    db.Database.ExecuteSqlRaw("EXEC AddCourseTrack @CourseId, @TrackId",
                        new SqlParameter("@CourseId", courseId),
                        new SqlParameter("@TrackId", trackId));
                }
            }

            return RedirectToAction("AllCourses");
        }


        [HttpGet]
        public IActionResult UpdateCourse(int courseId)
        {
        
            var courseToUpdate = db.Courses
                                    .Include(t=>t.Topics)
                                    .Include(c => c.Tracks)
                                    .FirstOrDefault(c => c.CourseId == courseId);

        
            if (courseToUpdate == null)
            {
               
                return NotFound(); 
            }

          
            return View("course/UpdateCourse", courseToUpdate);
        }

        [HttpPost]
        public IActionResult UpdateCourse(int courseId, string name, int passDegree, string topic)
        {
         
            db.Database.ExecuteSqlRaw("EXEC UpdateCourse @CourseId, @CourseName, @PassDegree",
                  new SqlParameter("@CourseId", courseId),
                new SqlParameter("@CourseName", name),
                new SqlParameter("@PassDegree", passDegree));

         

         
            if (courseId != 0)
            {  
                int topicId=db.Topics.Where(t=>t.CourseId==courseId).Select(s=>s.TopicId).FirstOrDefault(); 
                db.Database.ExecuteSqlRaw("EXEC UpdateTopic @topicId ,@TopicName, @CourseId",
                      new SqlParameter("@topicId", topicId),
                    new SqlParameter("@TopicName", topic),
                    new SqlParameter("@CourseId", courseId)); 
            }

            return RedirectToAction("AllCourses"); 
        }

        public IActionResult RemoveCourse(int courseId)
        {
           
                db.Database.ExecuteSqlRaw("EXEC DeleteCourse @CourseId", new SqlParameter("@CourseId", courseId));
         

            return RedirectToAction("AllCourses");
        }






    }
}
