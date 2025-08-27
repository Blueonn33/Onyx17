using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onyx17.Areas.Identity.Data;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;
using Onyx17.ViewModels;

namespace Onyx17.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _repository;
        private readonly UserManager<User> _userManager;

        public QuestionController(IQuestionRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var questions = await _repository.GetAllQuestionsAsync();
            //ViewBag.User = await _userManager.GetUserAsync(User);
            return View(questions);
        }

        [HttpPost]
        public async Task<IActionResult> Create(QuestionViewModel model)
        {
            string? userId = _userManager.GetUserId(User);
            User? user = await _userManager.GetUserAsync(User);

            if(userId == null || user == null)
            {
                throw new Exception("User not found");
            }

            var question = new Question
            {
                Text = model.Text,
                CreationDate = DateTime.UtcNow,
                UserId = userId,
                User = user
            };

            await _repository.CreateQuestionAsync(question);
            //ViewBag.User = await _userManager.GetUserAsync(User);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int questionId, QuestionViewModel model)
        {
            if(questionId == 0)
            {
                return NotFound();
            }

            var question = await _repository.GetQuestionByIdAsync(questionId);

            if (question == null)
            {
                return NotFound();
            }

            question.Text = model.Text;
            await _repository.UpdateQuestionAsync(question);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int questionId)
        {
            if(questionId == 0)
            {
                return NotFound();
            }

            var question = await _repository.GetQuestionByIdAsync(questionId);
            await _repository.DeleteQuestionAsync(questionId);

            return Ok();
        }
    }
}
