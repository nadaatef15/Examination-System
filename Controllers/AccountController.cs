using Exam_System.IRepository;
using Exam_System.Models;
using Exam_System.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Exam_System.Controllers
{
    public class AccountController : Controller
    {


        IAuthRepo authRepo;

        public AccountController(IAuthRepo _authRepo)
        {
            authRepo = _authRepo;
        }
        public async Task<IActionResult> Show()
        {
            await HttpContext.SignOutAsync();// remove cooke if open login
            return View();
        }

        //[HttpPost]


        //public IActionResult Login(string email, string password)
        //{


        //    try
        //    {
        //        var admin = authRepo.FindAdmin(email, password);
        //        if (admin != null)
        //        {

        //            return View("/Views/Admin/AdminDashboard.cshtml");
        //        }


        //        var instructor = authRepo.FindInstructor(email, password);
        //        if (instructor != null)
        //        {

        //            return View("/Views/Instructor/View.cshtml");
        //        }


        //        var student = authRepo.FindStudent(email, password);
        //        if (student != null)
        //        {

        //            return View("/Views/Student/View.cshtml");
        //        }

        //        ModelState.AddModelError("", "Invalid email or password.");
        //        return View("Show");
        //    }
        //    catch (Exception ex)
        //    {

        //        ModelState.AddModelError("", "An error occurred while processing your request.");

        //        return View("Show");
        //    }
        //}

        [HttpPost]
        public IActionResult Login(UserLoginView user)
        {
            //student1@gmail.com  123456789
            //AsmaaOmar@gmail.com 123456789

            try
            {
                var Role = "";
                var userId = "";


                var admin = authRepo.FindAdmin(user.Email, user.Password);
                if (admin != null)
                {
                    Role = "Admin";
                    userId = admin.AdminId.ToString();

                }

                var instructor = authRepo.FindInstructor(user.Email, user.Password);
                if (instructor != null)
                {
                    Role = "Instructor";
                    userId = instructor.InstructorId.ToString();

                }

                var student = authRepo.FindStudent(user.Email, user.Password);
                if (student != null)
                {
                    Role = "Student";
                    userId = student.StudentId.ToString();

                }

                if (!string.IsNullOrEmpty(Role))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.Role, Role),
                        new Claim("UserId", userId),
                    };

                    var userIdentity = new ClaimsIdentity(claims, "login");

                    var userPrincipal = new ClaimsPrincipal(userIdentity);

                    HttpContext.SignInAsync(userPrincipal);

                    switch (Role)
                    {
                        case "Admin":
                            return View("Views/Admin/AdminDashboard.cshtml");
                        case "Instructor":
                            return RedirectToAction("Index", "Instructor");
                        case "Student":
                            return RedirectToAction("ShowCourses", "HomePage", new { id = userId });
                    }
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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Show");
        }

        public IActionResult AccessError()
        {
            return View("Unauthorized");
        }
    }
}
