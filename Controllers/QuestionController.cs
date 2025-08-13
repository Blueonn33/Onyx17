using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;
using Onyx17.ViewModels;

namespace Onyx17.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;

        public QuestionController(IQuestionRepository repository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var questions = await _repository.GetAllQuestionsAsync();
            return View(questions);
        }

        [HttpPost]
        public async Task<IActionResult> Create(QuestionViewModel model)
        {
            string userId = _userManager.GetUserId(User);

            var question = new Question
            {
                Text = model.Text,
                CreationDate = DateTime.UtcNow,
                UserId = userId
            };

            await _repository.CreateQuestionAsync(question);
            return RedirectToAction("Index");
        }
    }
}
