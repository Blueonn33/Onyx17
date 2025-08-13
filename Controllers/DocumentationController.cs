using Microsoft.AspNetCore.Mvc;
using Onyx17.Models;
using Onyx17.Repositories.Interfaces;
using Onyx17.ViewModels;

namespace Onyx17.Controllers
{
    public class DocumentationController : Controller
    {
        private readonly ILanguageRepository _repository;

        public DocumentationController(ILanguageRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var languages = await _repository.GetAllLanguagesAsync();
            return View(languages);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LanguageViewModel model)
        {
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await model.ImageFile.CopyToAsync(ms);
                    model.ImageData = ms.ToArray();
                    model.ImageMimeType = model.ImageFile.ContentType;
                }
            }

            var language = new Language
            {
                Name = model.Name,
                ImageData = model.ImageData,
                ImageMimeType = model.ImageMimeType
            };

            await _repository.CreateLanguageAsync(language);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int languageId)
        {
            if (languageId == 0)
            {
                return NotFound();
            }

            var language = await _repository.GetLanguageByIdAsync(languageId);

            if (language == null)
            {
                return NotFound();
            }

            var languageVm = new LanguageViewModel
            {
                Name = language.Name,
                ImageData = language.ImageData,
                ImageMimeType = language.ImageMimeType
            };

            ViewBag.LanguageId = languageId;

            return View(languageVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LanguageViewModel model, int languageId)
        {
            if (languageId == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var language = await _repository.GetLanguageByIdAsync(languageId);

                if (language == null)
                {
                    return NotFound();
                }

                language.Name = model.Name;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await model.ImageFile.CopyToAsync(ms);
                        language.ImageData = ms.ToArray();
                        language.ImageMimeType = model.ImageFile.ContentType;
                    }
                }
                else
                {
                    language.ImageData = language.ImageData ?? model.ImageData;
                    language.ImageMimeType = language.ImageMimeType ?? model.ImageMimeType;
                }

                await _repository.UpdateLanguageAsync(language);
                return RedirectToAction("Index");
            }

            ViewBag.LanguageId = languageId;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetLanguageImage(int languageId)
        {
            var language = await _repository.GetLanguageByIdAsync(languageId);

            if(language == null || language.ImageData == null || language.ImageMimeType == null)
            {
                return NotFound();
            }

            return File(language.ImageData, language.ImageMimeType);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int languageId)
        {
            var language = await _repository.GetLanguageByIdAsync(languageId);

            if(language == null)
            {
                return NotFound();
            }

            await _repository.DeleteLanguageAsync(languageId);

            return Ok();
        }
    }
}
