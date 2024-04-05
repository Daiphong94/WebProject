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
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError("","UserName hoặc Password là bắt buộc nhập");
                return View(user);
            }

            var existingUser = await _userInterface.GetByName(user.UserName);
            if (existingUser != null)
            {
                ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại");
                return View(user);
            }
            if (user.Password != confirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp");
                return View(user);
            }

            await _userInterface.Add(user);

            var registration = new Registration { UserID = user.UserID, Role = user.Role, Status = "Cho duyet" };

            // Gọi phương thức RegisterUserAsync để thêm mới đăng ký
            await _registrationInterface.RegisterUserAsync(registration);
            return RedirectToAction(nameof(Index));
        }

      /*  public IActionResult UserAction()
        {
            
            var user = _userInterface.GetUser();

            if(user != null)
            {
                if(user.Role =="Admin")
                {
                    return View("ViewAdmin");
                }
                else if(user.Role == "Faculty")
                {
                    return View("ViewFaculty");
                }
                else if(user.Role == "Student")
                {
                    return View("ViewStudent");
                }
            }
            return NotFound();
        }*/

       
        public IActionResult Index()
        {
            return View();
        }
    }
}
