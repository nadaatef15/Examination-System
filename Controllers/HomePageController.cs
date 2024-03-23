using Exam_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Controllers
{
    [Authorize(Roles = "Student")]
    public class HomePageController : Controller
    {
        ExaminationContext db;

        public HomePageController(ExaminationContext _db)
        {
            db = _db;
        }

        public IActionResult ShowCourses(int id)
        {
            var model = db.Students
                .Include(s => s.StudentCourses)
                .ThenInclude(cs => cs.Course)
                .ThenInclude(e => e.Exams)
                .SingleOrDefault(s => s.StudentId == id);

            var track = db.Tracks.SingleOrDefault(t => t.TrackId == model.TrackId);
            ViewBag.track = track;
            return View(model);
        }
    }
}
