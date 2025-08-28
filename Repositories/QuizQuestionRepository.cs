using Microsoft.EntityFrameworkCore;
using Onyx17.Data;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;

namespace Onyx17.Repositories
{
    public class QuizQuestionRepository : IQuizQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public QuizQuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuizQuestion>> GetAllQuizQuestionsByQuizIdAsync(int quizId)
        {
            if(quizId == 0)
            {
                throw new ArgumentException("Тестът не може да бъде с ID = 0.", nameof(quizId)); 
            }

            var quizQuestions = await _context.QuizQuestions
                .Where(q => q.QuizId == quizId)
                .ToListAsync();

            return quizQuestions;
        }

        public async Task<QuizQuestion?> GetQuizQuestionByIdAsync(int quizQuestionId)
        {
            if (quizQuestionId == 0)
            {
                throw new ArgumentException("Въпросът не може да бъде с ID = 0.", nameof(quizQuestionId));
            }

            var quizQuestion = await _context.QuizQuestions.FindAsync(quizQuestionId);

            return quizQuestion;
        }

        public async Task CreateQuizQuestionAsync(QuizQuestion quizQuestion)
        {

            if (quizQuestion == null)
            {
                throw new ArgumentNullException(nameof(quizQuestion), "Въпросът не може да бъде null.");
            }

            await _context.QuizQuestions.AddAsync(quizQuestion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuizQuestionAsync(QuizQuestion quizQuestion)
        {
            if (quizQuestion == null)
            {
                throw new ArgumentNullException(nameof(quizQuestion), "Въпросът не може да бъде null.");
            }

            _context.QuizQuestions.Update(quizQuestion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuizQuestionAsync(int quizQuestionId)
        {
            if(quizQuestionId == 0)
            {
                throw new ArgumentException("Въпросът не може да бъде с ID = 0.", nameof(quizQuestionId));
            }

            var question = await _context.QuizQuestions.FindAsync(quizQuestionId);
            _context.QuizQuestions.Remove(question);
            await _context.SaveChangesAsync();
        }
    }
}
