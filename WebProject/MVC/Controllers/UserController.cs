using Data.Models;
using Data.Interface;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserInterface _userInterface;
        private readonly RegistrationInterface _registrationInterface;
        public UserController(UserInterface userInterface, RegistrationInterface registrationInterface)
        {
            _userInterface = userInterface;
            _registrationInterface = registrationInterface;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user, string confirmPassword)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Email))
            {
                ModelState.AddModelError("", "Không được để trống");
                return View(user);
            }

            var existingUser = await _userInterface.GetByName(user.UserName);
            if (existingUser != null)
            {
                ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại");
                return View(user);
            }
            var email = await _userInterface.GetByEmail(user.Email);
            if (email != null)
            {
                ModelState.AddModelError("Email", "Email đã tồn tại");
                return View(user);
            }
            if (user.Password != confirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp");
                return View(user);
            }

            await _userInterface.Add(user);

            var registration = new Registration { UserID = user.UserID, Role = user.Role, Status = "Cho duyet" };


            await _registrationInterface.RegisterUserAsync(registration);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userInterface.GetAll();
            return View(user);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ev = await _userInterface.GetById(id);
            if (ev == null)
            {
                return NotFound();
            }
            return View(ev);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userInterface.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Tên đăng nhập và mật khẩu không được để trống");
                return View();
            }
            var user = await _userInterface.GetByName(username);
            if ( user.Password != password)
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác");
                return View();
            }
            
            if(user.Role == "Admin")
            {
                return RedirectToAction("Account", "Admin");
            }
            else if (user.Role == "Faculty")
            {
                return RedirectToAction("Account", "Faculty");
            }
            else if (user.Role == "Student")
            {
                return RedirectToAction("Account", "Student");
            }
            else
            {
                
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
