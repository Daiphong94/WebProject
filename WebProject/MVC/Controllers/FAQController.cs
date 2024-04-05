using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class FAQController : Controller
    {
        private readonly FAQInterface _faqinterface;
        public FAQController(FAQInterface faqinterface)
        {
            _faqinterface = faqinterface;
        }
        public async Task<IActionResult> Index()
        {
            var faq = await _faqinterface.GetAllFAQ();
            return View(faq);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FAQ model)
        {
            await _faqinterface.AddFAQ(model);
            return RedirectToAction(nameof(Index));
            /*if (ModelState.IsValid)
            {
               
            }*/
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var faq = await _faqinterface.GetByIdFAQ(id);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FAQ model)
        {
            if (id != model.FAQID)
            {
                return BadRequest();
            }

            await _faqinterface.UpdateFAQ(model);
            return RedirectToAction(nameof(Index));
            /* if (ModelState.IsValid)
             {

             }*/
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var faq = await _faqinterface.GetByIdFAQ(id);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _faqinterface.DeleteFAQ(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

