namespace Onyx17.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageMimeType { get; set; }
        public ICollection<Chapter> Chapters { get; set; }
    }
}
