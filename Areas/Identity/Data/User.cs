using Microsoft.AspNetCore.Identity;
using Onyx17.Models;

namespace Onyx17.Areas.Identity.Data
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public string? Description { get; set; }
        [PersonalData]
        public byte[]? ImageData { get; set; }
        [PersonalData]
        public string? ImageMimeType { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<QuizResult> QuizResults { get; set; }
    }
}
