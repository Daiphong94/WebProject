using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class StudentController : Controller
    {
        private protected StudentInterface _studentInterface;
        
        public StudentController(StudentInterface studentInterface)
        {
            _studentInterface = studentInterface;
            
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
            await _studentInterface.AddStudent(model);
            return RedirectToAction(nameof(Index));
            /*if (ModelState.IsValid)
            {
               
            }*/
            return View(model);
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
    }
}
