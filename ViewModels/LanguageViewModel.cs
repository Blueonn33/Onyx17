namespace Onyx17.ViewModels
{
    public class LanguageViewModel
    {
        public string Name { get; set; }
        public IFormFile? ImageFile { get; set; }

        public byte[] ImageData { get; set; }    
        public string ImageMimeType { get; set; }
    }
}
