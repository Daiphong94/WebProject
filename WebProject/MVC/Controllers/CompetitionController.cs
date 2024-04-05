using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class CompetitionController : Controller
    {
        private readonly CompetitionInterface _competitionInterface;
        public CompetitionController(CompetitionInterface competitionInterface)
        {
            _competitionInterface = competitionInterface;
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
        public async Task<IActionResult> Create(Competition model)
        {
            await _competitionInterface.Add(model);
            return RedirectToAction(nameof(Index));
            /*if (ModelState.IsValid)
            {
               
            }*/
            return View(model);
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
    }
}
