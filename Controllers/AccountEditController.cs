using Exam_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Controllers
{
    public class AccountEditController : Controller
    {

        ExaminationContext db = new ExaminationContext();
        public IActionResult ChangepassStd(int stdID)
        {
            var model = db.Students.FirstOrDefault(a => a.StudentId == stdID);

            return View(model);
        }
        [HttpPost]
        public IActionResult ChangepassStd(string StudentPassword)
        {
            var userIdClaim = HttpContext.User.FindFirst("UserId");
            int userId = int.Parse(userIdClaim.Value);
            var model = db.Students.FirstOrDefault(a => a.StudentId == userId);
            model.StudentPassword = StudentPassword;

            db.Students.Update(model);
            db.SaveChanges();

            return RedirectToAction("ShowCourses", "HomePage", new { id = userId });
        }
        public IActionResult changpassinstructor()
        {
            var userIdClaim = HttpContext.User.FindFirst("UserId");
            int userId = int.Parse(userIdClaim.Value);
            var model = db.Instructors.FirstOrDefault(a => a.InstructorId == userId);

            return View(model);
        }
        [HttpPost]
        public IActionResult changpassinstructor(string InstructorPassword)
        {
            var userIdClaim = HttpContext.User.FindFirst("UserId");
            int userId = int.Parse(userIdClaim.Value);
            var model = db.Instructors.FirstOrDefault(a => a.InstructorId == userId);
            model.InstructorPassword = InstructorPassword;

            db.Instructors.Update(model);
            db.SaveChanges();

            return RedirectToAction("index", "instructor");
        }

    }
}
