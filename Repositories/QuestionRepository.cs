using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Onyx17.Data;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;

namespace Onyx17.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await _context.Questions
                .Include(q => q.User)
                .Include(q => q.Answers)
                    .ThenInclude(a => a.Reactions)
                .Include(q => q.Answers)
                    .ThenInclude(u => u.User)
                .ToListAsync();
        }

        public async Task<Question?> GetQuestionByIdAsync(int questionId)
        {
            if(questionId == 0)
            {
                throw new ArgumentException("Въпросът не може да бъде с ID = 0.", nameof(questionId));
            }

            var question = await _context.Questions.FindAsync(questionId);
            return question;
        }

        public async Task CreateQuestionAsync(Question question)
        {
            if(question == null)
            {
                throw new ArgumentNullException(nameof(question), "Въпросът не може да бъде null.");
            }

            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            if(questionId == 0)
            {
                throw new ArgumentException("Въпросът не може да бъде с ID = 0.", nameof(questionId));
            }

            var question = await _context.Questions
               .Include(q => q.Answers)
               .FirstOrDefaultAsync(q => q.Id == questionId);

            if (question == null)
            {
                throw new KeyNotFoundException($"Въпрос с Id {questionId} не е намерен.");
            }
                
            _context.Questions.Remove(question);
            _context.Answers.RemoveRange(question.Answers);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            if(question == null)
            {
                throw new ArgumentNullException(nameof(question), "Въпросът не може да бъде null.");
            }

            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
        }
    }
}
