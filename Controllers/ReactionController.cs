using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onyx17.Areas.Identity.Data;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;
using Onyx17.ViewModels;

namespace Onyx17.Controllers
{
    public class ReactionController : Controller
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IReactionRepository _reactionRepository;
        private readonly UserManager<User> _userManager;

        public ReactionController(IAnswerRepository answerRepository, UserManager<User> userManager, 
                IReactionRepository reactionRepository)
        {
            _answerRepository = answerRepository;
            _userManager = userManager;
            _reactionRepository = reactionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int answerId, ReactionViewModel model)
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
            return RedirectToAction("Index", "Question");
        }
    }
}
