using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class ResponseController : Controller
    {
        private readonly ResponseInterface _ResponseInterface;
        public ResponseController(ResponseInterface responseInterface)
        {
            _ResponseInterface = responseInterface;
        }

        public async Task<IActionResult> Index()
        {
            var respone = await _ResponseInterface.GetAllResponse();
            return View(respone);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Response model)
        {
            await _ResponseInterface.AddResponse(model);
            return RedirectToAction(nameof(Index));
            /*if (ModelState.IsValid)
            {
               
            }*/
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _ResponseInterface.GetByIdResponse(id);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Response model)
        {
            if (id != model.ResponseID)
            {
                return BadRequest();
            }

            await _ResponseInterface.UpdateResponse(model);
            return RedirectToAction(nameof(Index));
            /* if (ModelState.IsValid)
             {

             }*/
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _ResponseInterface.GetByIdResponse(id);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ResponseInterface.DeleteResponse(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
