using Onyx17.Models;

namespace Onyx17.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        public Task<IEnumerable<Question>> GetAllQuestionsAsync();
        public Task<Question?> GetQuestionByIdAsync(int questionId);
        public Task CreateQuestionAsync(Question question);
        public Task DeleteQuestionAsync(int questionId);
        public Task UpdateQuestionAsync(Question question);
    }
}
