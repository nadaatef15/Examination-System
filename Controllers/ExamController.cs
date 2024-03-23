using Exam_System.IRepository;
using Exam_System.Models;
using Exam_System.Repository;
using Microsoft.AspNetCore.Mvc;
using System;


namespace Exam_System.Controllers
{
    public class ExamController : Controller
    {
       
        Exam exam;
        IQuestionRepo questionRepo;
        IRepoExam repoExam;
        IRepoStudentAnswer repoStudentAnswer;
        IRepoStudent repoStudent;

        public ExamController(IQuestionRepo _questionRepo, IRepoExam _repoExam, IRepoStudentAnswer _repoStudentAnswer, IRepoStudent _repoStudent)
        {
            questionRepo=_questionRepo;
            repoExam=_repoExam;
            repoStudentAnswer=_repoStudentAnswer;
            repoStudent=_repoStudent;
        }
        public List<Question> GenerateQuestions(int crsId)
        {
            var AllQuestions= questionRepo.GetQuestions();
			Random random = new Random();
            List<Question> questions = AllQuestions.Where(q => q.CourseId == crsId)
												   .OrderBy(_ => random.Next())
												   .Take(10)
												   .ToList();
            return questions;
        }
		
		public Exam GenerateExam(List<Question> questions,int examId, int courseId, int instructorId)
		{
            exam = repoExam.GetExambyIds(examId, courseId, instructorId);
			exam.Questions= questions;
            //repoExam.Save();
            return exam;

		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult StartExam(int CrsId,int ExamId,int InstId)
		{
            var stdid = HttpContext.User.FindFirst("UserId");
            ViewBag.StdID = int.Parse(stdid.Value);
           Student std= repoStudent.getById(ViewBag.StdID);
           ViewBag.StudentName=$"{std.StudentFname} {std.StudentLname}";
            List<Question> questions = GenerateQuestions(CrsId);
			Exam exam = GenerateExam(questions, ExamId, CrsId, InstId);
           // repoExam.Save();
			return View(exam);
		}

        [HttpPost]
        public IActionResult StartExam(int examId, Dictionary<int, StudentAnswer> studentAnswers)
        {
            var stdid = HttpContext.User.FindFirst("UserId");
            ViewBag.StdID = int.Parse(stdid.Value);
            if (ModelState.IsValid)
            {
                foreach (var questionId in studentAnswers.Keys)
                {
                    if (studentAnswers[questionId].AnswerChooseId==null)
                    {
                        ModelState.AddModelError("Answer","there is question didn't answer");
                        return View(exam);
                    }
                    var answer = studentAnswers[questionId];
                    answer.ExamId = examId;
                    answer.StudentId = ViewBag.StdID;
                    repoStudentAnswer.Add(answer);
                }
                //repoExam.Save();
                return RedirectToAction("ShowCourses", "HomePage");
            }

            return View();
        }

    }
}
