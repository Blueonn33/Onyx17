using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Onyx17.Data;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;

namespace Onyx17.Repositories
{
    public class ReactionRepository : IReactionRepository
    {
        private readonly ApplicationDbContext _context;

        public ReactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reaction>> GetAllReactionsByAnswerIdAsync(int answerId)
        {
            if (answerId == 0)
            {
                throw new ArgumentException("Отговорът не може да бъде с ID = 0.", nameof(answerId));
            }

            var reactions = await _context.Reactions
                .Where(r => r.AnswerId == answerId)
                .OrderByDescending(r => r.Type == "Like")
                .ToListAsync();
            return reactions;
        }

        public async Task<Reaction?> GetReactionByIdAsync(int reactionId)
        {
            if (reactionId == 0)
            {
                throw new ArgumentException("Реакцията не може да бъде с ID = 0.", nameof(reactionId));
            }

            var reaction = await _context.Reactions.FindAsync(reactionId);
            return reaction;
        }

        public async Task CreateReactionAsync(Reaction reaction)
        {
            if(reaction == null)
            {
                throw new ArgumentNullException(nameof(reaction), "Реакцията не може да бъде null.");
            }

            var existingReaction = await _context.Reactions
                .FirstOrDefaultAsync(r => r.AnswerId == reaction.AnswerId && r.UserId == reaction.UserId);

            if (existingReaction == null)
            {
                await _context.Reactions.AddAsync(reaction);
                await _context.SaveChangesAsync();
            }
        }
    }
}
