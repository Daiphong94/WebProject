using Data.Models;
using Data.Interface;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminInterface _adminInterface;
        

        public AdminController(AdminInterface adminInterface)
        {
            _adminInterface = adminInterface;
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
    }
}
