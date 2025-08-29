using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onyx17.Areas.Identity.Data;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;
using Onyx17.ViewModels;

namespace Onyx17.Controllers
{
    public class AnswerController : Controller
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly UserManager<User> _userManager;

        public AnswerController(IAnswerRepository answerRepository, UserManager<User> userManager)
        {
            _answerRepository = answerRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnswersByQuestionId(int questionId, string userId)
        {
            if(questionId == 0)
            {
                return NotFound();
            }
            if (userId == null)
            {
                return NotFound();
            }

            var answers = await _answerRepository.GetAllAnswersByQuestionAndUserIdAsync(questionId, userId);
            return View(answers);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnswer(int questionId, AnswerViewModel model)
        {
            string? userId = _userManager.GetUserId(User);
            User? user = await _userManager.GetUserAsync(User);

            if (questionId == 0)
            {
                return NotFound();
            }
            if (userId == null || user == null)
            {
                throw new Exception("User not found");
            }

            var answer = new Answer
            {
                QuestionId = questionId,
                Text = model.Text,
                CreationDate = DateTime.Now,
                UserId = userId,
                User = user
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

        [HttpDelete]
        public async Task<IActionResult> Delete(int answerId)
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

            await _answerRepository.DeleteAnswerAsync(answerId);
            return RedirectToAction("Index", "Question");
        }
    }
}
