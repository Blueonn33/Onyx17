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
        private readonly IAnswerRepository _answerRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public QuestionController(IQuestionRepository repository, IAnswerRepository answerRepository ,
            UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _answerRepository = answerRepository;
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

        [HttpPost]
        public async Task<IActionResult> AddAnswer(int questionId, AnswerViewModel model)
        {
            string userId = _userManager.GetUserId(User);

            if (questionId == 0)
            {
                return NotFound();
            }
            if (string.IsNullOrWhiteSpace(model.Text))
            {
                return RedirectToAction("Index");
            }

            var answer = new Answer
            {
                QuestionId = questionId,
                Text = model.Text,
                CreationDate = DateTime.UtcNow,
                UserId = userId,
            };

            await _answerRepository.CreateAnswerAsync(answer);
            return RedirectToAction("Index");
        }
    }
}
