using Exam_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Controllers
{
    public class LoginController : Controller
    {
        ExaminationContext db=new ExaminationContext();

       
        public IActionResult Show()
        {
            return View();
        }

        [HttpPost]
    

        public IActionResult Login(string email, string password)
        {
            // Check if the user is an admin

            try{
                var admin = db.Admins.SingleOrDefault(x => x.Email == email && x.Password == password);
                if (admin != null)
                {
                    // If the user is an admin, redirect to the "Show" action
                    return View("/Views/Admin/AdminDashboard.cshtml");
                }


                var instructor = db.Instructors.SingleOrDefault(x => x.InstructorEmail == email && x.InstructorPassword == password);
                if (instructor != null)
                {

                    return View("/Views/Instructor/View.cshtml");
                }


                var student = db.Students.SingleOrDefault(x => x.StudentEmail == email && x.StudentPassword == password);
                if (student != null)
                {

                    return View("/Views/Student/View.cshtml");
                }

                ModelState.AddModelError("", "Invalid email or password.");
                return View("Show");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "An error occurred while processing your request.");
    
                return View("Show");
            }
        }

    }
}
