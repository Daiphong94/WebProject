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
        private readonly SCInterface _scInterface;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AnswerController(AnswerInterface answerInterface,
            UserInterface userInterface, StudentInterface studentInterface, 
            QuestionInterface questionInterface, CompetitionInterface competitionInterface, SCInterface scInterface, IWebHostEnvironment webHostEnvironment)
        {
            _answerInterface = answerInterface;
            _userInterface = userInterface;
            _studentInterface = studentInterface;
            _questionInterface = questionInterface;
            _competitionInterface = competitionInterface;
            _scInterface = scInterface;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var answers = await _answerInterface.GetAll();
            
            return View(answers);
        }
        public async Task<IActionResult> IndexFaculty()
        {
            var answers = await _answerInterface.GetAll();
            
            return View(answers);
        }
        public async Task<IActionResult> Create()
        {
            var model = new Answer();
            
            ViewBag.QuestionID = model.QuestionID;
            var question = await _questionInterface.GetById(model.QuestionID);
            ViewBag.Question = question;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Answer model, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img/photo", imageFile.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
                model.File = "/img/photo/" + imageFile.FileName;
            }
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
            ViewBag.QuestionID = model.QuestionID;
            var question = await _questionInterface.GetById(model.QuestionID);
            ViewBag.Question = question;
            return View(model);
        }   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MyAnswer( Answer model, IFormFile imageFile)
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
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img/photo", imageFile.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
                model.File = "/img/photo/" + imageFile.FileName;
            }
            model.StudentID = student.StudentID;

            await _answerInterface.Add(model);
            if (model.Question == null || model.Question.CompetitionID == 0)
            {
                return RedirectToAction("MyCompetitions", "SC");
            }
            var competitionId = model.Question.CompetitionID;
            await _scInterface.DeleteCompetition(competitionId, student.StudentID);
            return RedirectToAction("MyCompetitions", "SC");
        }

        public async Task<IActionResult> Details(int id)
        {
            var answer = await _answerInterface.GetById(id);
            if (answer == null)
            {
                return NotFound();
            }
            var question = await _questionInterface.GetById(answer.QuestionID);
            if (question == null)
            {
                return NotFound();
            }

            ViewBag.StudentID = answer.StudentID;
            ViewBag.Question = question;

            return View(answer);
        }
    }
}
