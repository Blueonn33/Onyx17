using Onyx17.Models;

namespace Onyx17.Repositories.Interfaces
{
    public interface IReactionRepository
    {
        public Task<IEnumerable<Reaction>> GetAllReactionsByAnswerIdAsync(int answerId);
        public Task<Reaction?> GetReactionByIdAsync(int id);
        public Task CreateReactionAsync(Reaction reaction);
    }
}
