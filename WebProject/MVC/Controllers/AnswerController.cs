using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class AnswerController : Controller
    {
        private readonly AnswerInterface _answerInterface;
        public AnswerController(AnswerInterface answerInterface)
        {
            _answerInterface = answerInterface;
        }

        public async Task<IActionResult> Index()
        {
            var answer = await _answerInterface.GetAll();
            return View(answer);
        }
        public IActionResult Create()
        {
            var model = new Answer();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Answer model)
        {
            await _answerInterface.Add(model);
            return RedirectToAction(nameof(Index));
            /*if (ModelState.IsValid)
            {
               
            }*/
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var answer = await _answerInterface.GetById(id);
            if (answer == null)
            {
                return NotFound();
            }
            return View(answer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Answer model)
        {
            if (id != model.AnswerID)
            {
                return BadRequest();
            }

            await _answerInterface.Update(model);
            return RedirectToAction(nameof(Index));
            /* if (ModelState.IsValid)
             {

             }*/
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var answer = await _answerInterface.GetById(id);
            if (answer == null)
            {
                return NotFound();
            }
            return View(answer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _answerInterface.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
