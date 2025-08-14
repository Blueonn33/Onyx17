using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onyx17.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        //public ICollection<Answer> Answers { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}
