using System.ComponentModel.DataAnnotations.Schema;

namespace Onyx17.Models
{
    public class QuizQuestion
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public string CorrectAnswer { get; set; }

        [ForeignKey(nameof(QuizId))]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}
