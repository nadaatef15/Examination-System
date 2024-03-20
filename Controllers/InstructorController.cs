using Exam_System.IRepository;
using Exam_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Exam_System.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorController : Controller
    {
      

        IInstructorRepo instructorRepo;

        public InstructorController(IInstructorRepo _instructorRepo)
        {
            instructorRepo = _instructorRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddQuestions() //get
        {
        
            var userIdClaim = HttpContext.User.FindFirst("UserId");
            int userId =int.Parse (userIdClaim.Value);
            var instructorCourses = instructorRepo.getInstructorCourses(userId);
            ViewBag.UserId = userId;
            ViewBag.InstructorCourses=instructorCourses;
            return View();
        }
     
        public IActionResult AddMcqQuestions(int courseId,string questionType,string questionTitle,string ansA,string ansB,string ansC, string correctAns) //get
        {
      
            instructorRepo.AddMcqQuestion(courseId,questionType,questionTitle, ansA, ansB, ansC, correctAns);


            return RedirectToAction("SuccessAddQuestion");
        }

        public IActionResult AddTfQuestions(int courseId, string questionType, string questionTitle,string correctAnswer)
        {
           instructorRepo.AddTfQuestion(courseId ,questionType,questionTitle, correctAnswer);

            return RedirectToAction("SuccessAddQuestion");
        }

        public IActionResult SuccessAddQuestion()
        {
            return View();
        }
    }
}
