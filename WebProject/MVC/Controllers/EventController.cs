using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class EventController : Controller
    {
        private readonly EventInterface _eventInterface;
        public EventController(EventInterface eventInterface)
        {
            _eventInterface = eventInterface;
        }

        public async Task<IActionResult> Index()
        {
            var ev = await _eventInterface.GetAll();
            return View(ev);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Events model)
        {
            await _eventInterface.Add(model);
            return RedirectToAction(nameof(Index));
            /*if (ModelState.IsValid)
            {
               
            }*/
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var ev = await _eventInterface.GetById(id);
            if (ev == null)
            {
                return NotFound();
            }
            return View(ev);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Events model)
        {
            if (id != model.EventID)
            {
                return BadRequest();
            }

            await _eventInterface.Update(model);
            return RedirectToAction(nameof(Index));
            /* if (ModelState.IsValid)
             {

             }*/
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var ev = await _eventInterface.GetById(id);
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
            await _eventInterface.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
