using Exam_System.IRepository;
using Exam_System.Models;
using Exam_System.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;


namespace Exam_System.Controllers
{
    public class ExamController : Controller
    {

        Exam exam;
        IQuestionRepo questionRepo;
        IRepoExam repoExam;
        IRepoStudentAnswer repoStudentAnswer;
        IRepoStudent repoStudent;
        IAnswerRepo answerRepo;

        public ExamController(IQuestionRepo _questionRepo, IRepoExam _repoExam, IRepoStudentAnswer _repoStudentAnswer, IRepoStudent _repoStudent, IAnswerRepo _answerRepo)
        {
            questionRepo = _questionRepo;
            repoExam = _repoExam;
            repoStudentAnswer = _repoStudentAnswer;
            repoStudent = _repoStudent;
            answerRepo = _answerRepo;
        }
        public List<Question> GenerateQuestions(int crsId)
        {
            var AllQuestions = questionRepo.GetQuestions();
            Random random = new Random();
            List<Question> questions = AllQuestions.Where(q => q.CourseId == crsId)
                                                   .OrderBy(_ => random.Next())
                                                   .Take(10)
                                                   .ToList();
            return questions;
        }

        public Exam GenerateExam(List<Question> questions, int examId, int courseId, int instructorId)
        {
            exam = repoExam.GetExambyIds(examId, courseId, instructorId);
            //Delete Old Exam Questions
            exam.Questions = questions;
            repoExam.Save(); //comment this line to make data don't save in table in DB
            return exam;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult StartExam(int CrsId, int ExamId, int InstId)
        {
            var stdid = HttpContext.User.FindFirst("UserId");
            ViewBag.StdID = int.Parse(stdid.Value);
            Student std = repoStudent.getById(ViewBag.StdID);
            ViewBag.StudentName = $"{std.StudentFname} {std.StudentLname}";
            List<Question> questions = GenerateQuestions(CrsId);
            Exam exam = GenerateExam(questions, ExamId, CrsId, InstId);
            // Assuming exam.Duration is a TimeSpan?
            string formattedDuration = exam.Duration?.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture);
            ViewBag.duration = formattedDuration;



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
                    if (studentAnswers[questionId].AnswerChooseId == null)
                    {
                        ModelState.AddModelError("AnswerChooseId", "there is question didn't answer");
                        //return View(exam);
                    }
                    var answer = studentAnswers[questionId];
                    answer.ExamId = examId;
                    answer.StudentId = ViewBag.StdID;
                    repoStudentAnswer.Add(answer);
                }
                repoExam.Save(); //comment this line to make data don't save in table in DB
                return RedirectToAction("EndExam", "Exam", new { examId = examId });
            }

            return View();
        }
        public async Task<ActionResult> EndExam(int examId)
        {
            var stdid = HttpContext.User.FindFirst("UserId");
            var studentAnswer = repoStudentAnswer.GetStudentAnswers(int.Parse(stdid.Value), examId);
            var mark = studentAnswer.Count;
            foreach (var answer in studentAnswer)
            {
                var isCorrect = await answerRepo.ValidateAnswer(answer.QuestionId, answer.AnswerChooseId);
                if (isCorrect == null || !isCorrect.Value) mark--;
            }
            ViewBag.StdID = int.Parse(stdid.Value);
            ViewBag.Mark = mark;

            return View();
        }

    }
}
