using Exam_System.IRepository;
using Exam_System.Controllers.Filters;
using Exam_System.Models;
using Exam_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Controllers
{
    
    [Authorize(Roles ="Admin")]
    
    public class AdminController : Controller
    {
      
        IRepoCourse courseRepo;
        IInstructorRepo InstructorRepo;
        IRepoTrack trackRepo;

        public AdminController(IRepoCourse _courseRepo,IInstructorRepo _instructorRepo ,IRepoTrack _trackRepo)
        {

            courseRepo = _courseRepo;
            InstructorRepo = _instructorRepo;
            trackRepo = _trackRepo;
        }

        public IActionResult AdminDashboard()
        {
            ViewBag.AllCourses = courseRepo.getAll();
            ViewBag.AllInstructor = InstructorRepo.getInstructors();
            ViewBag.AllTracks = trackRepo.getAll();
            return View();
        }
        public IActionResult AllCourses()
        {

            var courses = courseRepo.getAll();


            
            return View("course/AllCourses", courses);
          
        }
        // add course

        [HttpGet]
        public IActionResult AddCourse()
        {
 
            var tracks=courseRepo.GetAllTracks();
            return View("course/AddCourse",tracks);
        }
        [HttpPost]
        public IActionResult AddCourse(string name, int passDegree, string topic, List<int> selectedTracks)
        {
            
            courseRepo.AddCourse(name, passDegree, topic, selectedTracks);

            return RedirectToAction("AllCourses");
        }


        [HttpGet]
        public IActionResult UpdateCourse(int courseId)
        {
        

            var courseToUpdate = courseRepo.GetCourseById(courseId);

            if (courseToUpdate == null)
            {
               
                return NotFound(); 
            }
            var allTracks=courseRepo.GetAllTracks();
            ViewData["AllTracks"] = allTracks;

            return View("course/UpdateCourse", courseToUpdate);
        }

        [HttpPost]
        public IActionResult UpdateCourse(int courseId, string name, int passDegree, string topic, List<int> selectedTracks)
        {
         
           courseRepo.UpdateCourse(courseId, name, passDegree, topic, selectedTracks);

            return RedirectToAction("AllCourses"); 
        }

        public IActionResult RemoveCourse(int courseId)
        {

            try
            {
                courseRepo.RemoveCourse(courseId);

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                return RedirectToAction("AllCourses");
            }

            return RedirectToAction("AllCourses");




        }






    }
}
