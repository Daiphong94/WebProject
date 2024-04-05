using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class ExamController : Controller
    {
        private readonly ExamInterface _examInterface;
        public ExamController(ExamInterface examInterface)
        {
            _examInterface = examInterface;
        }

        public async Task<IActionResult> Index()
        {
            var exam = await _examInterface.GetAll();
            return View(exam);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exam model)
        {
            await _examInterface.Add(model);
            return RedirectToAction(nameof(Index));
            /*if (ModelState.IsValid)
            {
               
            }*/
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var exam = await _examInterface.GetById(id);
            if (exam == null)
            {
                return NotFound();
            }
            return View(exam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Exam model)
        {
            if (id != model.ExamID)
            {
                return BadRequest();
            }

            await _examInterface.Update(model);
            return RedirectToAction(nameof(Index));
            /* if (ModelState.IsValid)
             {

             }*/
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var exam = await _examInterface.GetById(id);
            if (exam == null)
            {
                return NotFound();
            }
            return View(exam);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _examInterface.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
