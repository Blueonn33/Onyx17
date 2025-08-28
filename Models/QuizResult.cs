using Onyx17.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onyx17.Models
{
    public class QuizResult
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public DateTime TakenAt { get; set; } = DateTime.Now;
        [ForeignKey(nameof(QuizId))]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
