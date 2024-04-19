using Data.Models;
using Data.Interface;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserInterface _userInterface;
        private readonly AdminInterface _adminInterface;
        private readonly RegistrationInterface _registrationInterface;

        public AdminController(AdminInterface adminInterface, RegistrationInterface registrationInterface, UserInterface userInterface)
        {
            _adminInterface = adminInterface;
            _registrationInterface = registrationInterface;
            _userInterface = userInterface;
        }

        public async Task<IActionResult> Index()
        {
            var admins = await _adminInterface.GetAllAdmin();
            return View(admins);
        }
        
        public IActionResult Create()
        {
            ViewBag.UserID = HttpContext.Session.GetString("UserID");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Admin model)
        {
            var userIdString = HttpContext.Session.GetString("UserID");
            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int userId))
            {

                var email = HttpContext.Session.GetString("Email");


                model.UserID = userId;
                model.Email = email;
                model.JoiningDate = DateTime.Now;

                await _adminInterface.AddAdmin(model);

                return RedirectToAction("Index");
            }
            else
            {

                return RedirectToAction("Login", "User");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var admin = await _adminInterface.GetByIdAdmin(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Admin admin)
        {
            if (id != admin.AdminID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _adminInterface.UpdateAdmin(admin);
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var admin = await _adminInterface.GetByIdAdmin(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _adminInterface.DeleteAdmin(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ApproveRegistration(int id)
        {
            var result = await _registrationInterface.ApproveRegistrationAsync(id);
            if (result)
            {
                // Xử lý khi duyệt đăng ký thành công
                return RedirectToAction(nameof(PendingRegistrations));
            }
            else
            {
                // Xử lý khi duyệt đăng ký thất bại
                return RedirectToAction(nameof(PendingRegistrations));
            }
        }
        public async Task<IActionResult> RejectRegistration(int id)
        {
            var result = await _registrationInterface.RejectRegistrationAsync(id);
            if (result)
            {
                // Xử lý khi từ chối đăng ký thành công
                return RedirectToAction(nameof(PendingRegistrations));
            }
            else
            {
                // Xử lý khi từ chối đăng ký thất bại
                return RedirectToAction(nameof(PendingRegistrations));
            }
        }

        public async Task<IActionResult> PendingRegistrations()
        {
            var pendingRegistrations = await _registrationInterface.GetAll();
            return View(pendingRegistrations);
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
