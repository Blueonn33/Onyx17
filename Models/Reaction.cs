using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onyx17.Models
{
    public class Reaction
    {
        public int Id { get; set; }
        public string Type { get; set; } // "Like" / "Dislike"

        [ForeignKey(nameof(AnswerId))]
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }

        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
