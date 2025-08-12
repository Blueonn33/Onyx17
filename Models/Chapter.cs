using System.ComponentModel.DataAnnotations.Schema;

namespace Onyx17.Models
{
    public class Chapter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[]? PdfFileData { get; set; }
        public string? PdfMimeType { get; set; }
        public string? PdfFileName { get; set; }

        [ForeignKey(nameof(LanguageId))]
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
