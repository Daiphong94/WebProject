using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MVC.Controllers
{
    public class SCController : Controller
    {
        private readonly SCInterface _scInterface;
        private readonly StudentInterface _studentInterface;
        private readonly CompetitionInterface _competitionInterface;
        private readonly UserInterface _userInterface;
        public SCController(SCInterface scInterface, StudentInterface studentInterface, CompetitionInterface competitionInterface, UserInterface userInterface)
        {
            _scInterface = scInterface;
            _studentInterface = studentInterface;
            _competitionInterface = competitionInterface;
            _userInterface = userInterface;
        }

        public async Task<IActionResult> Index()
        {
            var ev = await _scInterface.GetAll();
            return View(ev);
        }

        [HttpGet]
        public async Task<IActionResult> JoinCompetition(int competitionId)
        {
            var userid = HttpContext.Session.GetString("UserID");
            if (!string.IsNullOrEmpty(userid) && int.TryParse(userid, out int userId))
            {
                var student = await _studentInterface.GetByUserId(userId);
                var competition = await _competitionInterface.GetById(competitionId);

                if (student != null && competition != null)
                {
                    ViewData["Student"] = student;
                    ViewData["Competition"] = competition;

                    return View();
                }
                else
                {
                    
                    return RedirectToAction("Error", "SC");
                }
            }
            else
            { return RedirectToAction("Login", "User"); }

            
        }
        [HttpPost]
        public async Task<ActionResult> JoinCompetition(StudentCompetition model)
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

            var competition = await _competitionInterface.GetById(model.CompetitionID);
            if (competition == null)
            {
                return RedirectToAction("Error", "SC");
            }

            var existingRegistration = await _scInterface.GetByStudentAndCompetition(student.StudentID, model.CompetitionID);
            if (existingRegistration != null)
            {
                
                return RedirectToAction("Error", "SC");
            }

            var scmodel = new StudentCompetition
            {
                StudentID = student.StudentID,
                CompetitionID = model.CompetitionID,
                JoinedDate = DateTime.Now,
            };

            await _scInterface.Add(scmodel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MyCompetitions()
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

            var myCompetitions = await _scInterface.GetCompetitionsById(student.StudentID);
            return View(myCompetitions);
        }

        public IActionResult Success()
        {
            return View("Success");
        }

        public IActionResult Error()
        {
            return View("Error");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
