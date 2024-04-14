using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class ExamController : Controller
    {
        private readonly ExamInterface _examInterface;
        private readonly StudentInterface _studentInterface;
        public ExamController(ExamInterface examInterface, StudentInterface studentInterface)
        {
            _examInterface = examInterface;
            _studentInterface = studentInterface;
        }

        public async Task<IActionResult> Index(string searchText)
        {
            var exams = await _examInterface.GetAll();
            if (!string.IsNullOrEmpty(searchText))
            {
                
                exams = exams.Where(e => e.Competition.CompetitionName.Contains(searchText));
            }
            var sortedExams = exams.OrderByDescending(e => e.Score);
            return View(sortedExams);
        }
        public IActionResult Create(int competitionId, int studentId)
        {
            ViewBag.CompetitionId = competitionId;
            ViewBag.StudentId = studentId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exam model, int studentId)
        {
            var userIdString = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return RedirectToAction("Login", "User");
            }

            model.StudentID = studentId;

            await _examInterface.Add(model);

            await UpdateRanks(model.CompetitionID);

            return RedirectToAction(nameof(Index));

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

        private async Task UpdateRanks(int competitionId)
        {
            var exams = await _examInterface.GetExamsByCompetitionId(competitionId);

            
            foreach (var exam in exams)
            {
                
                var rank = await _examInterface.CalculateRank(competitionId, exam.Score);
                exam.Rank = rank;
                await _examInterface.Update(exam);
            }
        }
    }
}
