using Onyx17.Models;

namespace Onyx17.Repositories.Interfaces
{
    public interface IQuizRepository
    {
        public Task<IEnumerable<Quiz>> GetAllQuizzesAsync();
        public Task<Quiz?> GetQuizByIdAsync(int quizId);
        public Task CreateQuizAsync(Quiz quiz);
        public Task UpdateQuizAsync(Quiz quiz);
        public Task DeleteQuizAsync(int quizId);
    }
}
