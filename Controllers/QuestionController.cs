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
        private readonly IReactionRepository _reactionRepository;

        public QuestionController(IQuestionRepository repository, IAnswerRepository answerRepository ,
            UserManager<IdentityUser> userManager, IReactionRepository reactionRepository)
        {
            _repository = repository;
            _answerRepository = answerRepository;
            _userManager = userManager;
            _reactionRepository = reactionRepository;
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
        public async Task<IActionResult> AddReaction(int answerId, ReactionViewModel model)
        {
            string userId = _userManager.GetUserId(User);

            if (answerId == 0)
            {
                return NotFound();
            }

            var answer = await _answerRepository.GetAnswerByIdAsync(answerId);

            if (answer == null)
            {
                return NotFound();
            }

            var reaction = new Reaction
            {
                AnswerId = answerId,
                Type = model.Type,
                UserId = userId
            };

            await _reactionRepository.CreateReactionAsync(reaction);
            return RedirectToAction("Index");
        }
    }
}
