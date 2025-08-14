using Microsoft.AspNetCore.Mvc;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;
using Onyx17.ViewModels;

namespace Onyx17.Controllers
{
    public class QuizQuestionController : Controller
    {
        private readonly IQuizQuestionRepository _repository;
        private readonly IQuizRepository _quizRepository;

        public QuizQuestionController(IQuizQuestionRepository repository, IQuizRepository quizRepository)
        {
            _repository = repository;
            _quizRepository = quizRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int quizId)
        {
            if(quizId == 0)
            {
                return RedirectToAction("Index", "Quiz");
            }

            var quizQuestions = await _repository.GetAllQuizQuestionsByQuizIdAsync(quizId);
            return View(quizQuestions);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int quizId, QuizQuestionViewModel model)
        {
            if(quizId == 0)
            {
                return RedirectToAction("Index", "Quiz");
            }

            var quizQuestion = new QuizQuestion
            { 
                AnswerA = model.AnswerA,
                AnswerB = model.AnswerB,
                AnswerC = model.AnswerC,
                AnswerD = model.AnswerD,
                CorrectAnswer = model.CorrectAnswer,
                QuizId = quizId,
            };
            
            await _repository.CreateQuizQuestionAsync(quizQuestion);
            return RedirectToAction("Index", new { quizId });
        }
    }
}
