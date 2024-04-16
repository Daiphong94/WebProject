using Data.Models;
using Data.Interface;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserInterface _userInterface;
        private readonly RegistrationInterface _registrationInterface;
        
    
        public UserController(UserInterface userInterface, RegistrationInterface registrationInterface )
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
                ViewBag.ErrorMessage = "Tên người dùng, mật khẩu và email không được để trống.";
                return View(user);
            }

            var existingUser = await _userInterface.GetByName(user.UserName);
            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "Tên người dùng đã tồn tại.";
                return View(user);
            }

            var email = await _userInterface.GetByEmail(user.Email);
            if (email != null)
            {
                ViewBag.ErrorMessage = "Email đã tồn tại.";
                return View(user);
            }

            if (user.Password != confirmPassword)
            {
                ViewBag.ErrorMessage = "Mật khẩu xác nhận không khớp.";
                return View(user);
            }

            await _userInterface.Add(user);

            var registration = new Registration { UserID = user.UserID, Role = user.Role, Status = "Pending" };

            await _registrationInterface.RegisterUserAsync(registration);
            return RedirectToAction("Index", "Home");
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
        //Login
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {

            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError("", "Username and Password are required.");
                return View(user);
            }

            var login = await _userInterface.NameAndPassword(user.UserName, user.Password);
            if (login != null)
            {
                HttpContext.Session.SetString("UserID", login.UserID.ToString());
                HttpContext.Session.SetString("UserName", login.UserName);
                HttpContext.Session.SetString("Email", login.Email);
                HttpContext.Session.SetString("Role", login.Role);

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, login.UserID.ToString()),
            new Claim(ClaimTypes.Name, login.UserName),
            new Claim(ClaimTypes.Email, login.Email),
            new Claim(ClaimTypes.Role, login.Role)
        };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                if (login.Role == "Admin")
                {
                    return RedirectToAction("Account", "Admin");
                }
                else if (login.Role == "Faculty")
                {
                    return RedirectToAction("Account", "Faculty");
                }
                else if (login.Role == "Student")
                {
                    return RedirectToAction("Account", "Student");
                }
            }

            
            ViewBag.Error = "Username or password is incorrect!";
            return View();
        }
        // Profile
        public IActionResult Profile()
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
        // Log Out
        public  IActionResult Logout()

        {

            HttpContext.Session.Remove("UserID");
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Role");

            return RedirectToAction("Index", "Home");
        }
    }
}
