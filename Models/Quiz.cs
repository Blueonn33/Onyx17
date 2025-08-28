namespace Onyx17.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageMimeType { get; set; }

        public ICollection<QuizQuestion> QuizQuestions { get; set; }
        public ICollection<QuizResult> QuizResults { get; set; }
    }
}
