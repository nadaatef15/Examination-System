using Exam_System.IRepository;
using Exam_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exam_System.Controllers
{
    public class StudentAdminController : Controller
    {
        IRepoStudent studentIRepo;
        IRepoTrack trackIRepo;
        IRepoCourse courseIRepo;
        IRepoStudentCourse studentCourseIRepo;
        public StudentAdminController(IRepoStudent _studentIRepo,IRepoTrack _trackIRepo,IRepoCourse _courseIRepo,IRepoStudentCourse _studentCourseIRepo)
        {
            studentIRepo = _studentIRepo;
            trackIRepo = _trackIRepo;
            courseIRepo = _courseIRepo;
            studentCourseIRepo = _studentCourseIRepo;
        }
        public IActionResult Index()
        {
            var students = studentIRepo.getAll();
            return View(students);
        }
        public IActionResult Add()
        {
            ViewBag.Tracks = trackIRepo.getAll();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Student student)
        {
            if (string.IsNullOrEmpty(student.StudentFname) || string.IsNullOrEmpty(student.StudentLname) ||
                string.IsNullOrEmpty(student.StudentEmail) || string.IsNullOrEmpty(student.StudentPassword) ||
                string.IsNullOrEmpty(student.StudentGender) || student.TrackId == 0)
            {
                return View(student); 
            }

            if (studentIRepo.EmailIsExist(student.StudentEmail))
            {
                ViewBag.ErrorMsg = "Email Is Already Exist";
                ViewBag.Tracks = trackIRepo.getAll();
                return View(student);
            }

            var track = trackIRepo.getById((int)student.TrackId);

            if (track.Capacity > track.Students.Count())
            {
                studentIRepo.Add(student);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CapacityError = "Track Is Full";
                ViewBag.Tracks = trackIRepo.getAll();
                return View(student);
            }
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Tracks = trackIRepo.getAll();


            if (id == null)
                return BadRequest();

            var stdDate = studentIRepo.getById(id);
            if (stdDate == null)
                return NotFound();

            return View(stdDate);

        }
        [HttpPost]
        public IActionResult Edit(int id, Student student)
        {
            if (string.IsNullOrEmpty(student.StudentFname) || string.IsNullOrEmpty(student.StudentLname) ||
                string.IsNullOrEmpty(student.StudentEmail) || string.IsNullOrEmpty(student.StudentPassword) ||
                string.IsNullOrEmpty(student.StudentGender) || student.TrackId == 0)
            {
                return View(student);
            }

            var track = trackIRepo.getById((int)student.TrackId);

            if (track.Capacity > track.Students.Count())
            {
                studentIRepo.Edit(id, student);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CapacityError = "Track Is Full";
                ViewBag.Tracks = trackIRepo.getAll();
                return View(student);
            }
        }
        public IActionResult Delete(int id)
        {
            if(id == 0) return BadRequest();

            var std=studentIRepo.getById(id);
            if(std==null) return NotFound();

            studentIRepo.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult ManageCourses(int id)
        {
            if(id== 0) return BadRequest();

            var std = studentIRepo.getById(id);
            if(std==null) return NotFound();

            var allCourses = courseIRepo.getAll();
            var stdCourse = courseIRepo.getAll().Where(a=>a.StudentCourses.Any(a=>a.StudentId==id)).ToList();
            var stdNotCourse = allCourses.Except(stdCourse).ToList();
            
            ViewBag.StudentCourses = stdCourse;
            ViewBag.StudentNotCourses=stdNotCourse;

            return View(std);
        }
        [HttpPost]
        public IActionResult ManageCourses(List<int> CourseToRemove, List<int> CourseToAdd, int id)
        {
            var std = studentIRepo.getById(id);
            if(std==null) return NotFound();

            foreach(var item in CourseToAdd)
            {
                studentCourseIRepo.Add(new StudentCourse() { CourseId = item, StudentId = id });
            }
            foreach(var item in CourseToRemove)
            {
                studentCourseIRepo.Delete(item, id);
            }
            return RedirectToAction("Index");
        }
        public bool IsExist(string email)
        {
           return studentIRepo.EmailIsExist(email);
        } 
        public IActionResult ShowGrades(int id)
        {
            ViewBag.stdData =studentIRepo.getById(id);
            var stdCourses = studentCourseIRepo.getByStudentId(id);

            return View(stdCourses);
        }
    }
}
