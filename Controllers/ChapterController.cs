using Microsoft.AspNetCore.Mvc;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;
using Onyx17.ViewModels;

namespace Onyx17.Controllers
{
    public class ChapterController : Controller
    {
        private readonly IChapterRepository _repository;

        public ChapterController(IChapterRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int languageId)
        {
            if(languageId == 0)
            {
                return NotFound();
            }

            var chapters = await _repository.GetAllChaptersByLanguageIdAsync(languageId);

            ViewBag.LanguageId = languageId;

            if (chapters == null)
            {
                chapters = new List<Chapter>();
            }

            return View(chapters);
        }

        [HttpGet]
        public IActionResult Create(int languageId)
        {
            if(languageId == 0)
            {
                return NotFound();
            }

            ViewBag.LanguageId = languageId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChapterViewModel model, int languageId)
        {
            if(languageId == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                byte[]? pdfData = null;
                string? mimeType = null;
                string? fileName = null;

                if (model.PdfFile != null && model.PdfFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await model.PdfFile.CopyToAsync(ms);
                        pdfData = ms.ToArray();
                        mimeType = model.PdfFile.ContentType;
                        fileName = model.PdfFile.FileName;
                    }
                }

                var chapter = new Chapter
                {
                    Name = model.Name,
                    Description = model.Description,
                    PdfFileData = pdfData,
                    PdfMimeType = mimeType,
                    PdfFileName = fileName,
                    LanguageId = languageId
                };
                await _repository.CreateChapterAsync(chapter);
                return RedirectToAction(nameof(Index), new { languageId });
            }

            ViewBag.LanguageId = languageId;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetPdf(int chapterId)
        {
            if(chapterId == 0)
            {
                return NotFound();
            }

            var chapter = await _repository.GetChapterByIdAsync(chapterId);

            if(chapter == null || chapter.PdfFileData == null || chapter.PdfMimeType == null)
            {
                return NotFound();
            }

            ViewBag.ChapterId = chapterId;
            return File(chapter.PdfFileData, chapter.PdfMimeType);
        }

        [HttpGet]
        public async Task<IActionResult> PdfViewer(int chapterId)
        {
            if (chapterId == 0)
            {
                return NotFound();
            }

            var chapter = await _repository.GetChapterByIdAsync(chapterId);
            if (chapter == null)
            {
                return NotFound();
            }

            return View(chapter);
        }
    }
}
