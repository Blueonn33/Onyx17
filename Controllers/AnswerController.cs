using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;
using Onyx17.ViewModels;

namespace Onyx17.Controllers
{
    public class AnswerController : Controller
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public AnswerController(IAnswerRepository answerRepository, UserManager<IdentityUser> userManager)
        {
            _answerRepository = answerRepository;
            _userManager = userManager;
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
            return RedirectToAction("Index", "Question");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int answerId, AnswerViewModel model)
        {
            if (answerId == 0)
            {
                return NotFound();
            }

            var answer = await _answerRepository.GetAnswerByIdAsync(answerId);
            if (answer == null)
            {
                return NotFound();
            }

            answer.Text = model.Text;
            await _answerRepository.UpdateAnswerAsync(answer);
            return RedirectToAction("Index", "Question");
        }
    }
}
