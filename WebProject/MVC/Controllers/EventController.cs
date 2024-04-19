using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class EventController : Controller
    {
        private readonly EventInterface _eventInterface;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EventController(EventInterface eventInterface, IWebHostEnvironment webHostEnvironment)
        {
            _eventInterface = eventInterface;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var ev = await _eventInterface.GetAll();
            return View(ev);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Events model, IFormFile imageFile)
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
            await _eventInterface.Add(model);
            return RedirectToAction(nameof(Index));
            
        }
        public async Task<IActionResult> Edit(int id)
        {
            var ev = await _eventInterface.GetById(id);
            if (ev == null)
            {
                return NotFound();
            }
            return View(ev);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Events model, IFormFile imageFile)
        {
            if (id != model.EventID)
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

            await _eventInterface.Update(model);
            return RedirectToAction(nameof(Index));
            
        }
        public async Task<IActionResult> Delete(int id)
        {
            var ev = await _eventInterface.GetById(id);
            if (ev == null)
            {
                return NotFound();
            }
            return View(ev);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eventInterface.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            var ev = await _eventInterface.GetById(id);
            if (ev == null)
            {
                return NotFound(); 
            }
            else
            {
                return View(ev);
            }
        }
    }
}
