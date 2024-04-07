using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student model)
        {
            var user = await _userInterface.GetByName(User.Identity.Name);
            if(user != null)
            {
                model.UserID = user.UserID;
                model.Email = user.Email;

                await _studentInterface.AddStudent(model);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
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
            /*if (ModelState.IsValid)
            {
               
            }*/
            return View(model);
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
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
