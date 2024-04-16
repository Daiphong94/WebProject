using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class QuestionController : Controller
    {
        private readonly QuestionInterface _questionInterface;
        private readonly CompetitionInterface _competitionInterface;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public QuestionController(QuestionInterface questionInterface, CompetitionInterface competitionInterface, IWebHostEnvironment webHostEnvironment)
        {
            _questionInterface = questionInterface;
            _competitionInterface = competitionInterface;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            
            var question = await _questionInterface.GetAll();
            return View(question);
        }
        public async Task<IActionResult> Create(int competitionId)
        {
            var competition = await _competitionInterface.GetById(competitionId);
            if (competition == null)
            {
                return NotFound();
            }

            ViewData["CompetitionName"] = competition.CompetitionName;
            var model = new Question 
            {
                CompetitionID = competitionId,
                
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Question model, IFormFile imageFile)
        {
            model.CompetitionID = model.CompetitionID;
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img/photo", imageFile.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
                model.File = "/img/photo/" + imageFile.FileName;
            }
            await _questionInterface.Add(model);
            return RedirectToAction(nameof(Index));
            
        }
        public async Task<IActionResult> Edit(int id)
        {
            var question = await _questionInterface.GetById(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Question model)
        {
            if (id != model.QuestionID)
            {
                return BadRequest();
            }

            await _questionInterface.Update(model);
            return RedirectToAction(nameof(Index));
            
        }
        public async Task<IActionResult> Delete(int id)
        {
            var question = await _questionInterface.GetById(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _questionInterface.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
