//using Examination_System.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace Examination_System.Controllers
//{
//    public class HomePageController : Controller
//    {
//        ITIContext db;

//        public HomePageController(ITIContext _db)
//        {
//            db = _db;
//        }

//        public IActionResult ShowCourses(int id)
//        {
//            var model = db.Student.Include(s => s.CourseStudents).ThenInclude(cs => cs.course).ThenInclude(e => e.Exam).SingleOrDefault(s => s.Id == id);
//            var track = db.Track.SingleOrDefault(t => t.TrackId == model.TrackId);
//            ViewBag.track = track;
//            return View(model);
//        }
//        public IActionResult Instructor(int id)
//        {
//            var model = db.Instructor.SingleOrDefault(s => s.Ins_Id == id);
//            return View(model);
//        }


//    }
//}
