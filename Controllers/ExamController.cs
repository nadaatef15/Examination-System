using Exam_System.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Exam_System.Controllers
{
    public class ExamController : Controller
    {
        ExaminationContext db=new ExaminationContext();
        
        public List<Question> GenerateQuestions(int crsId)
        {
            var AllQuestions= db.Questions.ToList();
			Random random = new Random();
            List<Question> questions = AllQuestions.Where(q => q.CourseId == crsId)
												   .OrderBy(_ => random.Next())
												   .Take(10)
												   .ToList();
            return questions;
        }
		/*public List<Answer> GenerateAnswers(int quesId)
		{
			var AllAnswers= db.Answers.ToList();
			List<Answer> answers = AllAnswers.Where(a => a.QuestionId == quesId)
												   .ToList();
			return answers;

		}*/
		public Exam GenerateExam(List<Question> questions, int courseId, int instructorId)
		{
			Exam exam = db.Exams.FirstOrDefault(a => a.InstructorId==instructorId && a.CourseId==courseId);
			exam.Questions= questions;
			//db.SaveChanges();
			return exam;

		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Create()
		{
			int crsId = 4;
			int instructorId = 2;
			List<Question> questions = GenerateQuestions(crsId);
			Exam exam = GenerateExam(questions, crsId, instructorId);

			return View(exam);
		}

		[HttpPost]
		public IActionResult Create(int StudentId,int ExamId, int QuestionId, List<StudentAnswer> studentAnswers)
		{
			/*db.StudentAnswers.AddRange(studentAnswers);
			db.SaveChanges();*/
            if (ModelState.IsValid)
            {
				foreach (var studentAnswer in studentAnswers)
				{
					db.StudentAnswers.Add(studentAnswer);
				}
				db.SaveChanges();
				return RedirectToAction("Index", "Student");
            }
            else
            {
                // Handle invalid model state, if necessary
                return View(); // You may want to return the view with errors displayed
            }

            //return RedirectToAction("Index","Student");
		}
		public IActionResult SaveAnswer(StudentAnswer studentAnswer)
		{
			db.StudentAnswers.Add(studentAnswer);
			db.SaveChanges();
			return RedirectToAction("Create");
		}
    }
}
