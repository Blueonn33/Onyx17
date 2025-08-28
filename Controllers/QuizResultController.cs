using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onyx17.Areas.Identity.Data;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;
using Onyx17.ViewModels;

namespace Onyx17.Controllers
{
    public class QuizResultController : Controller
    {
        private readonly IQuizResultRepository _repository;
        private readonly UserManager<User> _userManager;

        public QuizResultController(IQuizResultRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int quizId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            string userId = user.Id;

            if (quizId == 0)
            {
                return RedirectToAction("Index", "Quiz");
            }

            var quizResults = await _repository.GetQuizResultsByQuizAndUserIdAsync(quizId, userId);
            var lastResult = quizResults.OrderByDescending(q => q.TakenAt).FirstOrDefault();

            return View(lastResult);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int quizId, QuizResultViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            string userId = user.Id;

            if (model == null)
            {
                return NotFound();
            }

            var quizResult = new QuizResult
            {
                Score = model.Score,
                TotalQuestions = model.TotalQuestions,
                QuizId = quizId,
                UserId = userId
            };

            await _repository.CreateQuizResultAsync(quizResult);
            ViewBag.QuizId = quizId;
            return View(quizResult);
        }

        //[HttpGet]
        //public async Task<IActionResult> Index(int quizId, int correct, int total)
        //{
        //    ViewBag.QuizId = quizId;
        //    ViewBag.Correct = correct;
        //    ViewBag.Total = total;
        //    return View();
        //}
    }
}
