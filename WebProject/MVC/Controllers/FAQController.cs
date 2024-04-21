using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class FAQController : Controller
    {
        private readonly FAQInterface _faqinterface;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FAQController(FAQInterface faqinterface, IWebHostEnvironment webHostEnvironment)
        {
            _faqinterface = faqinterface;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var faq = await _faqinterface.GetAllFAQ();
            return View(faq);
        }
        public async Task<IActionResult> IndexFaculty()
        {
            var faq = await _faqinterface.GetAllFAQ();
            return View(faq);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FAQ model, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img/photo", imageFile.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
                
                model.Photo = "/img/photo/" + imageFile.FileName;
            }
            if (imageFile == null)
            {
                ViewBag.ErrorMessage = "Tên người dùng, mật khẩu và email không được để trống.";
                return View(model);
            }
            await _faqinterface.AddFAQ(model);
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Edit(int id )
        {
            var faq = await _faqinterface.GetByIdFAQ(id);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FAQ model, IFormFile imageFile)
        {
            if (id != model.FAQID)
            {
                return BadRequest();
            }
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img/photo", imageFile.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
                model.Photo = "/img/photo/" + imageFile.FileName;
            }
            if (imageFile == null)
            {
                ViewBag.ErrorMessage = "Tên người dùng, mật khẩu và email không được để trống.";
                return View(model);
            }
            await _faqinterface.UpdateFAQ(model);
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Delete(int id)
        {
            var faq = await _faqinterface.GetByIdFAQ(id);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _faqinterface.DeleteFAQ(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

