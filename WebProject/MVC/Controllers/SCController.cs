using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class SCController : Controller
    {
        private readonly SCInterface _scInterface;
        private readonly StudentInterface _studentInterface;
        private readonly CompetitionInterface _competitionInterface;
        public SCController(SCInterface scInterface, StudentInterface studentInterface, CompetitionInterface competitionInterface)
        {
            _scInterface = scInterface;
            _studentInterface = studentInterface;
            _competitionInterface = competitionInterface;
        }

        public async Task<IActionResult> Index()
        {
            var ev = await _scInterface.GetAll();
            return View(ev);
        }

        [HttpGet]
        public async Task<IActionResult> JoinCompetition()
        {
            var competitions = await _competitionInterface.GetAll();
            ViewBag.Competitions = competitions;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> JoinCompetition(string name, string email)
        {
           var student = await _studentInterface.GetByEmail(email);
            if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
            {
                ModelState.AddModelError("email", "Vui lòng nhập một địa chỉ email hợp lệ.");
                var competitions = await _competitionInterface.GetAll();
                ViewBag.Competitions = competitions;
                return View();
            }
            
            if (student == null)
            {
                ViewBag.ErrorMessage = "Không tìm thấy sinh viên với email đã nhập.";
                var competitions = await _competitionInterface.GetAll();
                ViewBag.Competitions = competitions;
                return View();
            }
            var competition = await _competitionInterface.GetByName(name);
            

            var studentCompetition = new StudentCompetition
            {
                StudentID = student.StudentID,
                CompetitionID = competition.CompetitionID,
                JoinedDate = DateTime.Now
            };
            await _scInterface.Add(studentCompetition);
            return View("Success");
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
