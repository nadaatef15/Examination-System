using Exam_System.IRepository;
using Exam_System.Models;
using Exam_System.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Controllers
{
    public class InstructorCourseController : Controller
    {
        TrackIRepo trackIRepo;
        ICourseRepo courseIRepo;
        IInstructorCourseRepo instructorCourseRepo;

        public InstructorCourseController(IInstructorCourseRepo _instructorCourseRepo, TrackIRepo _trackIRepo, ICourseRepo _courseIRepo)
        {
            trackIRepo = _trackIRepo;
            courseIRepo = _courseIRepo;
            instructorCourseRepo = _instructorCourseRepo;

        }
        public IActionResult ShowInstructorCourse()
        {

            //[GetInstructorCourses]
            var inscourse= instructorCourseRepo.getAll();
            return View(inscourse);
        }

        public IActionResult showInstructorCourses(Instructor instructor)
        {
            instructorCourseRepo.getByinstructotId(instructor.InstructorId);
            return View();
        }

        public IActionResult DeleteCourse(int courseId ,int instid)
        {
            instructorCourseRepo.Delete(courseId, instid);
            return View();
        }
    }
}
