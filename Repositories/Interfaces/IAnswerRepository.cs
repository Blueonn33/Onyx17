using Onyx17.Models;

namespace Onyx17.Repositories.Interfaces
{
    public interface IAnswerRepository
    {
        public Task<IEnumerable<Answer>> GetAllAnswersByQuestionAndUserIdAsync(int questionId, string userId);
        public Task<Answer?> GetAnswerByIdAsync(int answerId);
        public Task CreateAnswerAsync(Answer answer);
        public Task UpdateAnswerAsync(Answer answer);
        public Task DeleteAnswerAsync(int answerId);
    }
}
