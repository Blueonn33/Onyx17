using Microsoft.EntityFrameworkCore;
using Onyx17.Data;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;

namespace Onyx17.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly ApplicationDbContext _context;
        public LanguageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
        {
            var languages = await _context.Languages.ToListAsync();
            return languages;
        }

        public async Task<Language?> GetLanguageByIdAsync(int id)
        {
            if(id == 0)
            {
                throw new ArgumentException("Езикът не може да бъде с ID = 0.", nameof(id));
            }

            var language = await _context.Languages.FindAsync(id);
            return language;
        }

        public async Task CreateLanguageAsync(Language language)
        {
            if (language == null)
            {
                throw new ArgumentNullException(nameof(language), "Езикът не може да бъде null.");
            }

            var languageExists = await _context.Languages.AnyAsync(l => l.Name == language.Name);

            if(languageExists == false)
            {
                await _context.Languages.AddAsync(language);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteLanguageAsync(int languageId)
        {
            if (languageId == 0)
            {
                throw new ArgumentException("Езикът не може да бъде с ID = 0.", nameof(languageId));
            }

            var language = await _context.Languages.FindAsync(languageId);
            
            if(language == null)
            {
                throw new KeyNotFoundException($"Език с Id {languageId} не е намерен.");
            }

            _context.Languages.Remove(language);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLanguageAsync(Language language)
        {
            if(language == null)
            {
                throw new ArgumentNullException(nameof(language), "Езикът не може да бъде null.");
            }

            _context.Languages.Update(language);
            await _context.SaveChangesAsync();
        }
    }
}
