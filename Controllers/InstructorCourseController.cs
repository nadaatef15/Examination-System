using Exam_System.IRepository;
using Exam_System.Models;
using Exam_System.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Controllers
{
    public class InstructorCourseController : Controller
    {
        IInstructorCourseRepo instructorCourseRepo;
        IInstructorAdminRepo instructorRepo;
        ICourseRepo courseRepo;

        public InstructorCourseController(IInstructorCourseRepo _instructorCourseRepo, IInstructorAdminRepo _instructorRepo, ICourseRepo _courseRepo)
        {
            instructorCourseRepo = _instructorCourseRepo;
            instructorRepo = _instructorRepo;
            courseRepo = _courseRepo;
        }

        public IActionResult ShowInstructorCourses(int id)
        {
            var inscourse = instructorCourseRepo.getInstructorCoursesById(id);
            ViewBag.InstructorId = id;
            return View("ShowInstructorCourses", inscourse);
        }

        public async Task<IActionResult> DeleteCourse(int courseId, int instid)
        {
            await instructorCourseRepo.Delete(courseId, instid);
            return ShowInstructorCourses(instid);
        }


        public IActionResult ManageCourses(int id)
        {
            var insCourses = instructorCourseRepo.getInstructorCoursesById(id);
            var allCourses = courseRepo.getAll();
            var CourseNotforInstructor = allCourses.Except(insCourses).ToList();
            ViewBag.instCourses = insCourses;
            ViewBag.CourseNotforInstructor = CourseNotforInstructor;
            ViewBag.insId = id;
            return View("ManageCourses");
        }

        [HttpPost]
        public async Task<IActionResult> ManageCourses(List<int> coursesToRemove, List<int> coursesToAdd, int insID)
        {

            var ins = instructorRepo.GetById(insID);
            if (ins == null) return NotFound();

            foreach (var item in coursesToAdd)
            {
                await instructorCourseRepo.Add(item, insID);
            }
            foreach (var item in coursesToRemove)
            {
                await instructorCourseRepo.Delete(item, insID);
            }
            return ShowInstructorCourses(insID);
        }
    }
}
