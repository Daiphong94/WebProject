using Data.Models;
using Data.Interface;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminInterface _adminInterface;
        private readonly RegistrationInterface _registrationInterface;

        public AdminController(AdminInterface adminInterface, RegistrationInterface registrationInterface)
        {
            _adminInterface = adminInterface;
            _registrationInterface = registrationInterface;
        }

        public async Task<IActionResult> Index()
        {
            var admins = await _adminInterface.GetAllAdmin();
            return View(admins);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Admin admin)
        {
            if (ModelState.IsValid)
            {
                await _adminInterface.AddAdmin(admin);
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
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

       
    }
}
