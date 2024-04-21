using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace MVC.Controllers
{
    
    public class StudentController : Controller
    {
        private protected StudentInterface _studentInterface;
        private protected UserInterface _userInterface;
        
        public StudentController(StudentInterface studentInterface,UserInterface userInterface)
        {
            _studentInterface = studentInterface;
            _userInterface = userInterface;
            
        }
        public async Task<IActionResult> Index()
        {
            var student = await _studentInterface.GetAllStudent();
            return View(student);
        }

        public async Task<IActionResult> IndexFaculty()
        {
            var student = await _studentInterface.GetAllStudent();
            return View(student);
        }
        public IActionResult Create()
        {
            ViewBag.UserID = HttpContext.Session.GetString("UserID");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student model)
        {
            var userIdString = HttpContext.Session.GetString("UserID");
            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int userId))
            {
                
                var email = HttpContext.Session.GetString("Email");
                var existingStudent = await _studentInterface.GetByEmail(email);
                if (existingStudent != null)
                {
                    // Email đã tồn tại, không thể tạo sinh viên mới
                    ModelState.AddModelError(string.Empty, "Email đã tồn tại trong hệ thống.");
                    return View(model); // Trả về view để hiển thị thông báo lỗi
                }

                model.UserID = userId;
                model.Email = email;

                model.AdmissionDate = DateTime.Now;
                
                await _studentInterface.AddStudent(model);

                return RedirectToAction("Account", "Student");
            }
            else
            {
               
                return RedirectToAction("Login", "User");
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentInterface.GetByIdStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student model)
        {
            if (id != model.StudentID)
            {
                return BadRequest();
            }
            await _studentInterface.UpdateStudent(model);
            return RedirectToAction(nameof(Index));
            
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.IsInRole("Admin"))
            {

            }
            var student = await _studentInterface.GetByIdStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentInterface.DeleteStudent(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Account()
        {
            var userid = HttpContext.Session.GetString("UserID");
            if (!string.IsNullOrEmpty(userid) && int.TryParse(userid, out int userId))
            {
                var userName = HttpContext.Session.GetString("UserName");
                var email = HttpContext.Session.GetString("Email");
                var role = HttpContext.Session.GetString("Role");

                var user = new User
                {
                    UserID = userId,
                    UserName = userName,
                    Email = email,
                    Role = role
                };
                ViewBag.InvalidLogin = true;
                return View(user);
            }
            else
            { return RedirectToAction("Login", "User"); }
        }        
    }
}
