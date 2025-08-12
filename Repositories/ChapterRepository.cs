using Microsoft.EntityFrameworkCore;
using Onyx17.Data;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;

namespace Onyx17.Repositories
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly ApplicationDbContext _context;

        public ChapterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Chapter>> GetAllChaptersByLanguageIdAsync(int languageId)
        {
            if(languageId == 0)
            {
                throw new ArgumentException("Езикът не може да бъде с Id = 0.", nameof(languageId));
            }

            var chapters = await _context.Chapters
                .Where(c => c.LanguageId == languageId)
                .ToListAsync();

            return chapters;
        }

        public async Task<Chapter?> GetChapterByIdAsync(int chapterId)
        {
            if(chapterId == 0)
            {
                throw new ArgumentException("Част не може да бъде с Id = 0.", nameof(chapterId));
            }

            var chapter = await _context.Chapters.FindAsync(chapterId);
            return chapter;
        }

        public async Task CreateChapterAsync(Chapter chapter)
        {
            if(chapter == null)
            {
                throw new ArgumentNullException(nameof(chapter), "Частта не може да бъде null.");
            }

            var existingChapter = await _context.Chapters.
                AnyAsync(c => c.Name == chapter.Name && c.LanguageId == chapter.LanguageId);

            if(existingChapter == false)
            {
                _context.Chapters.Add(chapter);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteChapterAsync(int chapterId)
        {
            if(chapterId == 0)
            {
                throw new ArgumentException("Част не може да бъде с Id = 0.", nameof(chapterId));
            }

            var chapter = await _context.Chapters.FindAsync(chapterId);
            _context.Chapters.Remove(chapter);
            await _context.SaveChangesAsync();
        }
    }
}
