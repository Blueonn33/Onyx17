using Microsoft.AspNetCore.Mvc;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;
using Onyx17.ViewModels;

namespace Onyx17.Controllers
{
    public class QuizController : Controller
    {
        private readonly IQuizRepository _repository;

        public QuizController(IQuizRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var quizzes = await _repository.GetAllQuizzesAsync();
            return View(quizzes);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(QuizViewModel model)
        {
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await model.ImageFile.CopyToAsync(ms);
                    model.ImageData = ms.ToArray();
                    model.ImageMimeType = model.ImageFile.ContentType;
                }
            }

            var quiz = new Quiz
            {
                Title = model.Title,
                Description = model.Description,
                ImageData = model.ImageData,
                ImageMimeType = model.ImageMimeType,
            };

            await _repository.CreateQuizAsync(quiz);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetQuizImage(int quizId)
        {
            var quiz = await _repository.GetQuizByIdAsync(quizId);

            if (quiz == null || quiz.ImageData == null || quiz.ImageMimeType == null)
            {
                return NotFound();
            }

            return File(quiz.ImageData, quiz.ImageMimeType);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int quizId)
        {
            if(quizId == 0)
            {
                return NotFound();
            }

            var quiz = await _repository.GetQuizByIdAsync(quizId);
            await _repository.DeleteQuizAsync(quizId);
            return RedirectToAction("Index");
        }
    }
}
