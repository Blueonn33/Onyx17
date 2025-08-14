using Microsoft.EntityFrameworkCore;
using Onyx17.Data;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;

namespace Onyx17.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly ApplicationDbContext _context;

        public QuizRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Quiz>> GetAllQuizzesAsync()
        {
            var quizzes = await _context.Quizzes
                .Include(q => q.QuizQuestions)
                .ToListAsync();

            return quizzes;
        }

        public async Task<Quiz?> GetQuizByIdAsync(int quizId)
        {
            if(quizId == 0)
            {
                throw new ArgumentException("Тестът не може да бъде с ID = 0.", nameof(quizId));
            }

            var quiz = await _context.Quizzes.FindAsync(quizId);
            return quiz;
        }

        public async Task CreateQuizAsync(Quiz quiz)
        {
            if(quiz == null)
            {
                throw new ArgumentNullException(nameof(quiz), "Тестът не може да бъде null.");
            }

            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuizAsync(Quiz quiz)
        {
            if(quiz == null)
            {
                throw new ArgumentNullException(nameof(quiz), "Тестът не може да бъде null.");
            }

            _context.Quizzes.Update(quiz);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuizAsync(int quizId)
        {
            if(quizId == 0)
            {
                throw new ArgumentException("Тестът не може да бъде с ID = 0.", nameof(quizId));
            }

            var quiz = await _context.Quizzes.FindAsync(quizId);
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
        }
    }
}
