using Exam_System.IRepository;
using Exam_System.Models;
using Exam_System.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Exam_System.Controllers
{
    public class AdminController : Controller
    {
      
        IRepoCourse courseRepo;

        public AdminController(IRepoCourse _courseRepo)
        {
            courseRepo = _courseRepo;
        }

      
        public IActionResult AllCourses()
        {

            var courses = courseRepo.getAll();


            
            return View("Course/AllCourses",courses);
          
        }
        // add course

        [HttpGet]
        public IActionResult AddCourse()
        {
 
            var tracks=courseRepo.GetAllTracks();
            return View("Course/AddCourse",tracks);
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
