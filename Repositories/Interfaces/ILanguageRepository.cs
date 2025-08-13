using Onyx17.Models;

namespace Onyx17.Repositories.Interfaces
{
    public interface ILanguageRepository
    {
        public Task<IEnumerable<Language>> GetAllLanguagesAsync();
        public Task<Language?> GetLanguageByIdAsync(int languageId);
        public Task CreateLanguageAsync(Language language);
        public Task DeleteLanguageAsync(int languageId);

        public Task UpdateLanguageAsync(Language language);
    }
}
