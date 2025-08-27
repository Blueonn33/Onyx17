using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Onyx17.Data;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;

namespace Onyx17.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ApplicationDbContext _context;

        public AnswerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Answer>> GetAllAnswersByQuestionIdAsync(int questionId)
        {
            if(questionId == 0)
            {
                throw new ArgumentException("Въпросът не може да бъде с ID = 0.", nameof(questionId));
            }

            var answers = await _context.Answers
                .Where(a => a.QuestionId == questionId)
                .Include(a => a.User)
                .Include(a => a.Reactions)
                .ToListAsync();
            return answers;
        }

        public async Task<Answer?> GetAnswerByIdAsync(int answerId)
        {
            if(answerId == 0)
            {
                throw new ArgumentException("Отговорът не може да бъде с ID = 0.", nameof(answerId));
            }

            var answer = await _context.Answers.FindAsync(answerId);

            if (answer == null)
            {
                throw new KeyNotFoundException($"Отговор с ID {answerId} не е намерен.");
            }

            return answer;
        }

        public async Task CreateAnswerAsync(Answer answer)
        {
            if (answer == null)
            {
                throw new ArgumentNullException(nameof(answer), "Отговорът не може да бъде null.");
            }

            await _context.Answers.AddAsync(answer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAnswerAsync(Answer answer)
        {
            if (answer == null)
            {
                throw new ArgumentNullException(nameof(answer), "Отговорът не може да бъде null.");
            }

            _context.Answers.Update(answer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnswerAsync(int answerId)
        {
            if (answerId == 0)
            {
                throw new ArgumentException("Отговорът не може да бъде с ID = 0.", nameof(answerId));
            }

            var answer = await _context.Answers.FindAsync(answerId);

            if (answer == null)
            {
                throw new KeyNotFoundException($"Отговор с ID {answerId} не е намерен.");
            }

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
        }
    }
}
