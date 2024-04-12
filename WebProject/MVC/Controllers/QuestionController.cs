using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class QuestionController : Controller
    {
        private readonly QuestionInterface _questionInterface;
        private readonly CompetitionInterface _competitionInterface;
        public QuestionController(QuestionInterface questionInterface, CompetitionInterface competitionInterface)
        {
            _questionInterface = questionInterface;
            _competitionInterface = competitionInterface;
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
        public async Task<IActionResult> Create(Question model)
        {
            model.CompetitionID = model.CompetitionID;
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
            /* if (ModelState.IsValid)
             {

             }*/
            return View(model);
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
