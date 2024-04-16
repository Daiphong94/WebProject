using Data.Interface;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class RegistrationController : Controller
    { 
        private readonly RegistrationInterface _registrationInterface;
        private readonly UserInterface _userInterface;
        public RegistrationController(RegistrationInterface registrationInterface, UserInterface userInterface)
        {
            _registrationInterface = registrationInterface;
            _userInterface = userInterface;
        }
        public async Task<IActionResult> Index()
        {
            var regis = await _registrationInterface.GetAll();
            return View(regis);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var ev = await _registrationInterface.GetById(id);
            if (ev == null)
            {
                return NotFound();
            }
            return View(ev);
        }

        [HttpPost, ActionName("Delete")]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            await _registrationInterface.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRegis(int id)
        {
            
            await _registrationInterface.DeleteRegis(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
