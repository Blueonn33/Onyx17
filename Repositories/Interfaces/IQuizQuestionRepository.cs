using Onyx17.Models;

namespace Onyx17.Repositories.Interfaces
{
    public interface IQuizQuestionRepository
    {
        public Task<IEnumerable<QuizQuestion>> GetAllQuizQuestionsByQuizIdAsync(int quizId);
        public Task<QuizQuestion?> GetQuizQuestionByIdAsync(int quizQuestionId);
        public Task CreateQuizQuestionAsync(QuizQuestion quizQuestion);
        public Task UpdateQuizQuestionAsync(QuizQuestion quizQuestion);
        public Task DeleteQuizQuestionAsync(int quizQuestionId);
    }
}
