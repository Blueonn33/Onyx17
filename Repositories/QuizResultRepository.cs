using Microsoft.EntityFrameworkCore;
using Onyx17.Data;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;
using Onyx17.ViewModels;

namespace Onyx17.Repositories
{
    public class QuizResultRepository : IQuizResultRepository
    {
        private readonly ApplicationDbContext _context;

        public QuizResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuizResult>> GetQuizResultsByQuizAndUserIdAsync(int quizId, string userId)
        {
            if(quizId == 0)
            {
                throw new ArgumentException("Тестът не може да бъде с Id = 0.", nameof(quizId));
            }
            if(userId == null)
            {
                throw new ArgumentNullException(nameof(userId), "Потребителят не може да бъде null.");
            }

            var quizResult = await _context.QuizResults
                .Where(qr => qr.QuizId == quizId && qr.UserId == userId).ToListAsync();

            return quizResult;
        }

        public async Task<QuizResult?> GetQuizResultByIdAsync(int quizResultId)
        {
            if(quizResultId == 0)
            {
                throw new ArgumentException("Резултатът не може да бъде с Id = 0.", nameof(quizResultId));
            }

            var quizResult = await _context.QuizResults.FindAsync(quizResultId);
            return quizResult;
        }

        public async Task CreateQuizResultAsync(QuizResult quizResult)
        {
            if(quizResult == null)
            {
                throw new ArgumentNullException(nameof(quizResult), "Резултатът не може да бъде null.");
            }

            await _context.QuizResults.AddAsync(quizResult);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuizResultAsync(QuizResult quizResult)
        {
            if(quizResult == null)
            {
                throw new ArgumentNullException(nameof(quizResult), "Резултатът не може да бъде null.");
            }

            var currentQuizResult = await _context.QuizResults.FindAsync(quizResult.Id);
            
            if(currentQuizResult == null)
            {
                throw new KeyNotFoundException($"Резултат с ID {quizResult.Id} не е намерен.");
            }

            if (currentQuizResult.Score != quizResult.Score)
            {
                currentQuizResult.Score = quizResult.Score;
                _context.QuizResults.Update(currentQuizResult);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteQuizResultAsync(int quizResultId)
        {
            if(quizResultId == 0)
            {
                throw new ArgumentException("Резултатът не може да бъде с Id = 0.", nameof(quizResultId));
            }

            var quizResult = await _context.QuizResults.FindAsync(quizResultId);
            _context.QuizResults.Remove(quizResult);
            await _context.SaveChangesAsync();
        }
    }
}
