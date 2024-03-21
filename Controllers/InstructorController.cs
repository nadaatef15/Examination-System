using Exam_System.IRepository;
using Exam_System.Models;
using Exam_System.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Controllers
{
    public class InstructorController : Controller
    {
        InstructorCRUDRepo instructorRepo;
        TrackIRepo trackIRepo;
        ICourseRepo courseIRepo;

        public InstructorController(InstructorCRUDRepo _instructorIRepo, TrackIRepo _trackIRepo, ICourseRepo _courseIRepo)
        {
            instructorRepo = _instructorIRepo;
            trackIRepo = _trackIRepo;
            courseIRepo = _courseIRepo;

        }

        public async Task<IActionResult> Index()
        {
            var instructors = await instructorRepo.GetAll();


            return View(instructors);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Instructor instructor)
        {
            if (string.IsNullOrEmpty(instructor.InstructorFname) ||
                string.IsNullOrEmpty(instructor.InstructorLname) ||
                string.IsNullOrEmpty(instructor.InstructorPassword) ||
                instructor.InstructorSalary == null ||
                string.IsNullOrEmpty(instructor.InstructorEmail))
            {
                return View(instructor);
            }

            if (instructor.InstructorPassword.Length < 8)
            {
                ViewBag.ErrorMsg = "password must be more than or equal 8 charactars";
                return View(instructor);
            }

            if (instructorRepo.IsEmailExist(instructor.InstructorEmail))
            {
                ViewBag.ErrorMsg = "this email is already exist";
                return View(instructor);

            }

            await instructorRepo.Add(instructor);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {

            if (id == 0) return BadRequest();
            // getbyid for instructor
            await instructorRepo.Delete(id);

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            ViewBag.Tracks = trackIRepo.getAll();


            if (id == null)
                return BadRequest();

            var insDate = instructorRepo.GetById(id);
            if (insDate == null)
                return NotFound();

            return View(insDate);

        }
        [HttpPost]
        public IActionResult Edit(int id, Instructor instructor)
        {
            if (string.IsNullOrEmpty(instructor.InstructorFname) || string.IsNullOrEmpty(instructor.InstructorLname) ||
                string.IsNullOrEmpty(instructor.InstructorEmail) || string.IsNullOrEmpty(instructor.InstructorPassword) ||
                 instructor.InstructorSalary==null)
            {
                return View(instructor);
            }

            else
                instructorRepo.Edit(id, instructor);
                 return RedirectToAction("Index");
        }

        public IActionResult ManageCourses(List<int> CourseToRemove, List<int> CourseToAdd, int id)
        {
            var ins = instructorRepo.GetById(id);
            if (ins == null) return NotFound();

            foreach (var item in CourseToAdd)
            {
                InstructorCourseRepo.Add(new InstructorCourse()
                { CourseId = item, InstructorId = id });
            }
            foreach (var item in CourseToRemove)
            {
                InstructorCourseRepo.Delete(item, id);
            }
            return RedirectToAction("Index");
        }

    }
}