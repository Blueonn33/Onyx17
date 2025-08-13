using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onyx17.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
