using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Hosting;

namespace MVC.Controllers
{
    [Authorize(Roles = "Faculty")]
    public class CompetitionController : Controller
    {
        private readonly CompetitionInterface _competitionInterface;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompetitionController(CompetitionInterface competitionInterface, IWebHostEnvironment webHostEnvironment)
        {
            _competitionInterface = competitionInterface;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var competition = await _competitionInterface.GetAll();
            return View(competition);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Competition model, IFormFile imageFile)
        {
            if (string.IsNullOrEmpty(model.CompetitionName) || string.IsNullOrEmpty(model.Description))
            {
                
                ModelState.AddModelError("", "CompetitionName và Description không được để trống" );
                return View(model); 
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


            await _competitionInterface.Add(model);
            return RedirectToAction(nameof(Index));
        }
            
    
        public async Task<IActionResult> Edit(int id)
        {
            var competition = await _competitionInterface.GetById(id);
            if (competition == null)
            {
                return NotFound();
            }
            return View(competition);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Competition model)
        {
            if (id != model.CompetitionID)
            {
                return BadRequest();
            }

            await _competitionInterface.Update(model);
            return RedirectToAction(nameof(Index));
            /* if (ModelState.IsValid)
             {

             }*/
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var competition = await _competitionInterface.GetById(id);
            if (competition == null)
            {
                return NotFound();
            }
            return View(competition);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _competitionInterface.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var competition = await _competitionInterface.GetById(id);
            if (competition == null)
            {
                return NotFound();
            }
            return View(competition);
        }
    }
}
