using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onyx17.Models
{
    public class QuizResult
    {
        public int Id { get; set; }

        [ForeignKey(nameof(QuizId))]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        
        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public DateTime CompletionDate { get; set; } = DateTime.Now;
    }
}
