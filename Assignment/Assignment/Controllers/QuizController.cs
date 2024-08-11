using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Controllers
{
    public class QuizController : Controller
    {
        private readonly DBContext _context;

        public QuizController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult BeginTest()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BeginTest(string email)
        {
            if (!string.IsNullOrEmpty(email) && new EmailAddressAttribute().IsValid(email))
            {
                Response.Cookies.Append("UserEmail", email);
                return RedirectToAction("QuestionPage", new { questionIndex = 1 });
            }
            ViewBag.Error = "Please enter a valid email.";
            return View();
        }
        [HttpGet]
        public IActionResult QuestionPage(int questionIndex)
        {
            var question = _context.Questions.Skip(questionIndex - 1).FirstOrDefault();
            if (question == null)
            {
                return RedirectToAction("ExamResult");
            }

            ViewBag.QuestionIndex = questionIndex;
            return View(question);
        }

        [HttpPost]
        public IActionResult QuestionPage(int questionIndex, int questionId, string selectedAnswer)
        {
            var email = Request.Cookies["UserEmail"];
            if (email != null)
            {
                _context.UserAnswers.Add(new UserAnswer
                {
                    Email = email,
                    QuestionId = questionId,
                    SelectedAnswer = selectedAnswer
                });
                _context.SaveChanges();
            }

            return RedirectToAction("QuestionPage", new { questionIndex = questionIndex + 1 });
        }

        [HttpGet]
        public IActionResult ExamResult()
        {
            var email = Request.Cookies["UserEmail"];
            var userAnswers = _context.UserAnswers.Where(u => u.Email == email).ToList();
            var totalQuestions = _context.Questions.Count();
            var correctAnswers = userAnswers.Count(ua => _context.Questions.First(q => q.Id == ua.QuestionId).CorrectAnswer == ua.SelectedAnswer);


            var questionAnswerResults = new List<QuestionAnswerResult>();

            foreach (var userAnswer in userAnswers)
            {
                var question = _context.Questions.First(q => q.Id == userAnswer.QuestionId);

                questionAnswerResults.Add(new QuestionAnswerResult
                {
                    QuestionText = question.QuestionText, 
                    SelectedAnswer = userAnswer.SelectedAnswer,
                    CorrectAnswer = question.CorrectAnswer
                });
            }

            var model = new ExamResultViewModel
            {
                Email = email,
                QuestionAnswerResults = questionAnswerResults,
                CorrectAnswers = correctAnswers,
                TotalQuestions = totalQuestions
            };

            return View(model);
        }

    }
}
