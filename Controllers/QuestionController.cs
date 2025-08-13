using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onyx17.Repositories.Interfaces;

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


    }
}
