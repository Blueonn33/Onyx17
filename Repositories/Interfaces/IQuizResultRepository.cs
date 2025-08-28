using Onyx17.Models;

namespace Onyx17.Repositories.Interfaces
{
    public interface IQuizResultRepository
    {
        public Task<IEnumerable<QuizResult>> GetQuizResultsByQuizAndUserIdAsync(int quizId, string userId);
        public Task<QuizResult?> GetQuizResultByIdAsync(int quizResultId);
        public Task CreateQuizResultAsync(QuizResult quizResult);
        public Task UpdateQuizResultAsync(QuizResult quizResult);
        public Task DeleteQuizResultAsync(int quizResultId);
    }
}
