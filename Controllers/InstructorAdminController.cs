using Exam_System.IRepository;
using Exam_System.Models;
using Exam_System.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace Exam_System.Controllers
{

    [Authorize(Roles = "Admin")]
    public class InstructorAdminController : Controller
    {
        IInstructorAdminRepo instructorAdminRepo;
        IRepoTrack trackIRepo;
        IRepoCourse courseIRepo;
        IInstructorCourseRepo instructorCourseRepo;
        ExaminationContext db;

        public InstructorAdminController(IInstructorAdminRepo _instructorIRepo, IRepoTrack _trackIRepo, IRepoCourse _courseIRepo, IInstructorCourseRepo _instructorCourseRepo, ExaminationContext _db)
        {
            instructorAdminRepo = _instructorIRepo;
            trackIRepo = _trackIRepo;
            courseIRepo = _courseIRepo;
            instructorCourseRepo = _instructorCourseRepo;
            db = _db;
        }


        public async Task<IActionResult> Index()
        {
            var instructors = await instructorAdminRepo.GetAll();


            return View("Index", instructors);
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

            if (instructorAdminRepo.IsEmailExist(instructor.InstructorEmail))
            {
                ViewBag.ErrorMsg = "this email is already exist";
                return View(instructor);

            }

            await instructorAdminRepo.Add(instructor);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {

            if (id == 0) return BadRequest();


            var inst = db.Instructors.Include(a => a.Courses).Include(a => a.Tracks).FirstOrDefault(a => a.InstructorId == id);

            if (inst.Courses.Count > 0)
            {
                ViewBag.ErrorMsg = "this instructor has cources";
            }
            else if (inst.Tracks.Count > 0)
            {
                ViewBag.ErrorMsg = "this instructor has tracks";

            }
            else
            {
                await instructorAdminRepo.Delete(id);

            }


            return await Index();
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Tracks = trackIRepo.getAll();


            if (id == null)
                return BadRequest();

            var insDate = instructorAdminRepo.GetById(id);
            if (insDate == null)
                return NotFound();

            return View(insDate);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Instructor instructor)
        {
            if (string.IsNullOrEmpty(instructor.InstructorFname) || string.IsNullOrEmpty(instructor.InstructorLname) ||
                string.IsNullOrEmpty(instructor.InstructorEmail) || string.IsNullOrEmpty(instructor.InstructorPassword) ||
                 instructor.InstructorSalary == null)
            {
                return View(instructor);
            }

            else
                await instructorAdminRepo.Edit(id, instructor);
            return RedirectToAction("Index");
        }



    }
}