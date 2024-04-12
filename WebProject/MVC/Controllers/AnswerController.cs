using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers
{
    public class AnswerController : Controller
    {
        private readonly UserInterface _userInterface;
        private readonly StudentInterface _studentInterface;
        private readonly QuestionInterface _questionInterface;
        private readonly AnswerInterface _answerInterface;
        private readonly CompetitionInterface _competitionInterface;
        public AnswerController(AnswerInterface answerInterface, UserInterface userInterface, StudentInterface studentInterface, QuestionInterface questionInterface, CompetitionInterface competitionInterface)
        {
            _answerInterface = answerInterface;
            _userInterface = userInterface;
            _studentInterface = studentInterface;
            _questionInterface = questionInterface;
            _competitionInterface = competitionInterface;
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

        public async Task<IActionResult> MyAnswer(int competitionId)
        {
            var userIdString = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return RedirectToAction("Login", "User");
            }
            var questionId = await _questionInterface.GetQuestionIdByCompetitionId(competitionId);
            var student = await _studentInterface.GetByUserId(userId);
            if (student == null)
            {
                
                return RedirectToAction("Error", "SC");
            }
            var model = new Answer
            {
                QuestionID = questionId,
                StudentID = student.StudentID,
            };

            return View(model);
        }   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MyAnswer( Answer model)
        {
             var userIdString = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return RedirectToAction("Login", "User");
            }

            var student = await _studentInterface.GetByUserId(userId);
            if (student == null)
            {
                
                return RedirectToAction("Error", "SC");
            }

            model.StudentID = student.StudentID;

            await _answerInterface.Add(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
