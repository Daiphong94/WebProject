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
                    
                    return RedirectToAction("Create", "Student");
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

            return RedirectToAction("Account", "Student");
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
                return RedirectToAction("Create", "Student");
            }


            var myCompetitions = await _scInterface.GetCompetitionsById(student.StudentID);
            
            var studentCompetitions = myCompetitions.ToList();
            return View(studentCompetitions);
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

        public async Task<IActionResult> Delete(int studentId, int competitionId)
        {
            var sc = await _scInterface.GetStudentCompetitionById(studentId, competitionId);
            if (sc == null)
            {
                return NotFound();
            }
            return View(sc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int studentId, int competitionId)
        {
            var userIdString = HttpContext.Session.GetString("UserID");

            // Kiểm tra xem UserID có tồn tại và hợp lệ không
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                // Nếu không hợp lệ, chuyển hướng người dùng đến trang đăng nhập
                return RedirectToAction("Login", "User");
            }

            // Lấy thông tin sinh viên từ UserID
            var student = await _studentInterface.GetByUserId(userId);

            // Kiểm tra xem thông tin sinh viên có tồn tại không
            if (student == null)
            {
                // Nếu không tồn tại, chuyển hướng người dùng đến trang lỗi
                return RedirectToAction("Error", "SC");
            }

            // Lấy thông tin cuộc thi từ CompetitionID
            var competition = await _competitionInterface.GetById(competitionId);

            // Kiểm tra xem thông tin cuộc thi có tồn tại không
            if (competition == null)
            {
                // Nếu không tồn tại, chuyển hướng người dùng đến trang lỗi
                return RedirectToAction("Error", "SC");
            }

            // Gọi phương thức Delete từ _scInterface để xóa StudentCompetition
            await _scInterface.Delete(studentId, competitionId);

            // Sau khi xóa thành công, chuyển hướng người dùng đến trang Index hoặc trang khác phù hợp
            return RedirectToAction("");
        }
    }
}
