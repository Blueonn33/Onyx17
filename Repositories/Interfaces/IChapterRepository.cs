using Onyx17.Models;

namespace Onyx17.Repositories.Interfaces
{
    public interface IChapterRepository
    {
        public Task<IEnumerable<Chapter>> GetAllChaptersByLanguageIdAsync(int languageId);
        public Task<Chapter?> GetChapterByIdAsync(int chapterId);
        public Task CreateChapterAsync(Chapter chapter);
        public Task DeleteChapterAsync(int chapterId);
    }
}
